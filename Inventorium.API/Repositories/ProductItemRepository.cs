using Inventorium.API.Data;
using Inventorium.API.Models;
using Inventorium.API.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Inventorium.API.Repositories
{
    public class ProductItemRepository : IProductItemRepository
    {

        // add the readonly property context
        private readonly InventoriumDbContext _context;

        public ProductItemRepository(InventoriumDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductItemModel>> GetProductItems() // This can be null list if the product categories are empty
        {
            return await _context.ProductItems.ToListAsync();
                
        }

        public async Task<IEnumerable<ProductItemModel>> GetProductItemsByProductReference(int id) // This can be null list if the product categories are empty
        {
            return await _context.ProductItems.Where(productItem => productItem.ProductReferenceId == id).ToListAsync();

        }

        public async Task<ProductItemModel> GetProductItemById(int id)
        {
            return await _context.ProductItems.FindAsync(id);
                /*.Include(p => p.ProductReference)
                .AsNoTracking()
                .SingleOrDefault(p => p.Id == id);*/
        }

        public ProductItemModel CreateProductItem(ProductItemModel newProductItem, int productReferenceId)
        {
            var productReference = _context.ProductReferences.Find(productReferenceId);

            if (productReference == null)
            {
                throw new InvalidOperationException("Product reference not found");
            }

            newProductItem.ProductReference = productReference;

            _context.ProductItems.Add(newProductItem);
            _context.SaveChanges();

            return newProductItem;
        }

        public void UpdateProductReferenceOnProductItem(int productItemId, int productReferenceId)
        {
            var productItem= _context.ProductItems.Find(productItemId);
            var productReference = _context.ProductReferences.Find(productReferenceId);

            if (productItem is null || productReference is null)
            {
                throw new InvalidOperationException("Product item or product reference does not exist");
            }

            productItem.ProductReference = productReference;

            _context.SaveChanges();
        }

        public void DeleteProductItemById(int productItemId)
        {
            var productItemToDelete = _context.ProductItems.Find(productItemId);

            if (productItemToDelete is null)
            {
                throw new InvalidOperationException("Product item was not found or doesn't exist");
            }
            else
            {
                _context.ProductItems.Remove(productItemToDelete);
                _context.SaveChanges();
            }
        }

        public void UpdateProductItemById(int productItemId, ProductItemModel newProductItem)
        {
            var productItemToUpdate = _context.ProductItems.Find(productItemId);

            if (productItemToUpdate is null)
            {
                throw new InvalidOperationException("Product item was not found or doesn't exist");
            }
            else
            {
                productItemToUpdate.Name = newProductItem.Name ?? productItemToUpdate.Name;

                if (productItemToUpdate.ProductReference.Id != newProductItem.ProductReference.Id)
                {
                    productItemToUpdate.ProductReference = newProductItem.ProductReference;
                }

                _context.ProductItems.Update(productItemToUpdate);
            }
        }

    }
}
