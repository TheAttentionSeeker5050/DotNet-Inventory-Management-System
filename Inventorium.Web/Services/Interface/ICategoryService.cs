using Inventorium.Dtos.Dtos;

namespace Inventorium.Web.Services.Interface
{
    public interface ICategoryService
    {
        Task<IEnumerable<ProductCategoryDto>> GetCategoriesAsync();
        Task<ProductCategoryDto> GetCategoryByIdAsync(int id);
        Task<IEnumerable<ProductCategoryDto>> GetCategoriesBySearchParamAsync(string query);
    }
}
