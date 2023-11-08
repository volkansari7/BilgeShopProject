using BilgeShop.Business.Dto;
using BilgeShop.Business.Services;
using BilgeShop.Business.Types;
using BilgeShop.Data.Entities;
using BilgeShop.Data.Enums;
using BilgeShop.Data.Repository;
using Microsoft.AspNetCore.DataProtection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeShop.Business.Managers
{
    public class UserManager : IUserService
    {
        private readonly IRepository<UserEntity> _userRepository;
        private readonly IDataProtector _dataProtector;
        public UserManager(IRepository<UserEntity> userRepository, IDataProtectionProvider dataProtectionProvider)
        {
            _userRepository = userRepository;
            _dataProtector = dataProtectionProvider.CreateProtector("security");
        }
        // Dependency Injection
        public ServiceMessage AddUser(AddUserDto addUserDto)
        {
            var hasMail = _userRepository.GetAll(x => x.Email.ToLower() == addUserDto.Email.ToLower()).ToList();
            // Eklenilmek istenen mail adresi ile veritabanında zaten bulunan verilerin tamamını çekiyorum. (Null gelirse, eklenebilir. - null değilse zten o kullanıcı adı mevcut.)

            if (hasMail.Any()) // hasMail is not null / hasMail.Count != 0
            {
                // TOO : Eklenemez Uyarı Ver.
                return new ServiceMessage()
                {
                    IsSucceed = false,
                    Message = "Bu eposta hesabiyla bir kullanici zaten mevcut."
                };
            }

            var entity = new UserEntity()
            {
                Email = addUserDto.Email,
                FirstName = addUserDto.FirstName,
                LastName = addUserDto.LastName,
                Password = _dataProtector.Protect(addUserDto.Password),
                UserType = UserTypeEnum.User
            };

            _userRepository.Add(entity);

            return new ServiceMessage
            {
                IsSucceed = true
            };

        }

        public UserInfoDto LoginUser(LoginDto loginDto)
        {
            var userEntity = _userRepository.Get(x => x.Email == loginDto.Email);

            if (userEntity is null)
            {
                return null;

                // Eğer form üzerinde gösterilen email adresi ile eşleşen bir veri tabloda yoksa, oturum açılmayacağı için geriye veri dönülmüyor.
            }

            var rawPassword = _dataProtector.Unprotect(userEntity.Password); // Şifreyi açtım.

            if (rawPassword == loginDto.Password)
            {
                return new UserInfoDto()
                {
                    Id = userEntity.Id,
                    Email = userEntity.Email,
                    FirstName = userEntity.FirstName,
                    LastName = userEntity.LastName,
                    UserType = userEntity.UserType,
                };
            }
            else
            {
                return null;
            }
        }
    }
}
