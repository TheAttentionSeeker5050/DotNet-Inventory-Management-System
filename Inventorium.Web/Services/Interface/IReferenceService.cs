using Inventorium.Models;
using Inventorium.Dtos;

namespace Inventorium.Web.Services.Interface
{
    public interface IReferenceService
    {
        Task<IEnumerable<ProductReferenceDto>> GetReferencesAsync();
        Task<ProductReferenceDto> GetReferenceByIdAsync(int id);
        Task<IEnumerable<ProductReferenceDto>> GetReferencesBySearchParamAsync(string query);
        // Create new product reference
        Task<ProductReferenceDto> CreateProductReference(ProductReferenceModel productReferenceModel);
    }
}
