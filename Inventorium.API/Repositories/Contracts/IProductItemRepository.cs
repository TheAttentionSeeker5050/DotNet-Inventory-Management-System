using Inventorium.API.Models;

namespace Inventorium.API.Repositories.Contracts
{
    public interface IProductItemRepository
    {
        Task<IEnumerable<ProductItemModel>> GetProductItems();
        Task<IEnumerable<ProductItemModel>> GetProductItemsByProductReference(int id);
        Task<ProductItemModel> GetProductItemById(int id);
        ProductItemModel CreateProductItem(ProductItemModel newProductItem, int productReferenceId);
        void UpdateProductReferenceOnProductItem(int productItemId, int productReferenceId);
        void DeleteProductItemById(int productItemId);
        void UpdateProductItemById(int productItemId, ProductItemModel newProductItem);
    }
}
