using Inventorium.API.Data;
using Inventorium.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Inventorium.API.Services
{
    public class ProductItemService
    {

        // add the readonly property context
        private readonly InventoriumDbContext _context;

        public ProductItemService(InventoriumDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ProductItemModel> GetProductItems() // This can be null list if the product categories are empty
        {
            return _context.ProductItems
                .AsNoTracking() // This is to mark this query as read only to avoid any unwanted changes when 
                                // the entry Linq relations change in some other row or table
                .ToList();
        }

        public ProductItemModel? GetProductItemById(int id)
        {
            return _context.ProductItems
                .Include(p => p.ProductReference)
                .AsNoTracking()
                .SingleOrDefault(p => p.Id == id);
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
