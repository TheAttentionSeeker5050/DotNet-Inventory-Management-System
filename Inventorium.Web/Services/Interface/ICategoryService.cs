using Inventorium.Dtos.Dtos;

namespace Inventorium.Web.Services.Interface
{
    public interface ICategoryService
    {
        Task<IEnumerable<ProductCategoryDto>> GetCategoriesAsync();
    }
}
