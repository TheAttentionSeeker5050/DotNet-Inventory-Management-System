using Inventorium.Dtos.Dtos;

namespace Inventorium.Web.Services.Interface
{
    public interface IItemService
    {
        Task<IEnumerable<ProductItemDto>> GetItemsAsync();
        Task<ProductItemDto> GetItemByIdAsync(int id);
        Task<IEnumerable<ProductItemDto>> GetItemsBySearchParamAsync(string query);
    }
}
