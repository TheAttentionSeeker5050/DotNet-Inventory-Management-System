using Inventorium.Models;
using Inventorium.Dtos;
using System.Net.Http;

namespace Inventorium.Web.Services.Interface
{
    public interface ICategoryService
    {
        Task<IEnumerable<ProductCategoryDto>> GetCategoriesAsync();
        Task<ProductCategoryDto> GetCategoryByIdAsync(int id);
        Task<IEnumerable<ProductCategoryDto>> GetCategoriesBySearchParamAsync(string query);

        Task<ProductCategoryDto> CreateProductCategory(ProductCategoryModel productCategoryModel);
    }
}
