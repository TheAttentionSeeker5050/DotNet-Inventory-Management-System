using Inventorium.API.Models;

namespace Inventorium.API.Repositories.Contracts
{
    public interface IProductReferenceRepository
    {
        Task<IEnumerable<ProductReferenceModel>> GetProductReferences();
        Task<ProductReferenceModel> GetProductReferenceById(int id);
        ProductReferenceModel CreateProductReference(ProductReferenceModel newProductReference, int productCategoryId);
        void UpdateProductCategoryOnProductReference(int productReferenceId, int productCategoryId);
        void DeleteProductReferenceById(int productReferenceId);
        void UpdateProductReferenceById(int productReferenceId, ProductReferenceModel newProductReference);
    }
}
