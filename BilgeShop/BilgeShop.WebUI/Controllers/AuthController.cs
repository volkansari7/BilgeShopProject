using BilgeShop.Business.Dto;
using BilgeShop.Business.Services;
using BilgeShop.WebUI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BilgeShop.WebUI.Controllers
{
    public class AuthController : Controller
    {

        private readonly IUserService _userService;
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("KayıtOl")]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [Route("KayıtOl")]
        public IActionResult Register(RegisterViewModel formData)
        {
            if (!ModelState.IsValid)
            {
                return View(formData);
            }

            var addUserDto = new AddUserDto()
            {
                Email = formData.Email.Trim(),
                FirstName = formData.FirstName.Trim(),
                LastName = formData.LastName.Trim(),
                Password = formData.Password.Trim(),
            };

            var result = _userService.AddUser(addUserDto);

            if (result.IsSucceed)
            {
                // İşlem başarılı anasayfaya yönlendir..
                return RedirectToAction("Index", "Home");
            }else
            {
                // İşlem başarısızsa Form View'e geri dön.
                ViewBag.Error = result.Message;
                return View(formData);
            }

            // Kayıt işlemi tamamlandıktan sonra ana sayfaya gönderelim.
            return RedirectToAction("Index","Home");
        }

        public async Task<IActionResult> Login(LoginViewModel formData)
        {

            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }

            var loginDto = new LoginDto()
            {
                Email = formData.Email,
                Password = formData.Password
            };

            var userInfo = _userService.LoginUser(loginDto);

            if(userInfo is null)
            {
                return RedirectToAction("Index", "Home");
            }

            // Buraya kadar gelebildiyse kodlar, demek ki kişinin formdan gönderdiği email ve şifre ile DB üzerindeki kayıt eşleşmiş. Gerekli bilgileri alıp veri tabanından buraya kadar getirmişiz  (UserInfo içerinde)

            // Oturumda tutacağım her veri -> Claim
            // Bütün verilerin listesi -> List<Claim>

            var claims = new List<Claim>();

            claims.Add(new Claim("id", userInfo.Id.ToString()));
            claims.Add(new Claim("email", userInfo.Email));
            claims.Add(new Claim("firstname", userInfo.FirstName));
            claims.Add(new Claim("lastname", userInfo.LastName));
            claims.Add(new Claim("usertype", userInfo.UserType.ToString()));

            claims.Add(new Claim(ClaimTypes.Role, userInfo.UserType.ToString()));

            var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // Claims listesindeki verilerle bir oturum açıldığı için yukarıdaki identity nesnesini tanımladım.

            var autProperties = new AuthenticationProperties
            {
                AllowRefresh = true, // yenilenebilir oturum. false yapıldığında sayfa yenilendiğinde tekrar giriş yapılır.
                ExpiresUtc = new DateTimeOffset(DateTime.Now.AddHours(48)) // oturum açıldıktan 48 saat sonra hesap düşecek 
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity), autProperties);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Index","Home");
        }
    }
}
