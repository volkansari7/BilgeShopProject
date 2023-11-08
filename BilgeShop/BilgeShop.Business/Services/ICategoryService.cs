using BilgeShop.Business.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeShop.Business.Services
{
    public interface ICategoryService
    {
        bool AddCategory(AddCategotyDto addCategoryDto);
        List<ListCategoryDto> GetCategories();
        UpdateCategoryDto GetCategory(int id);
        void UpdateCategory(UpdateCategoryDto updateCategoryDto);
        void DeleteCategory(int id);
    }
}
