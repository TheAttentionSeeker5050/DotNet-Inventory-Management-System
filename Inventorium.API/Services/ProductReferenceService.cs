using Inventorium.API.Data;
using Inventorium.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Inventorium.API.Services
{
    public class ProductReferenceService
    {

        // add the readonly property context
        private readonly InventoriumDbContext _context;

        public ProductReferenceService(InventoriumDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ProductReferenceModel> GetProductReferences() // This can be null list if the product categories are empty
        {
            return _context.ProductReferences
                .AsNoTracking() // This is to mark this query as read only to avoid any unwanted changes when 
                                // the entry Linq relations change in some other row or table
                .Include(p => p.ProductItems)
                .Include(p => p.ProductCategory)
                .ToList();
        }

        public ProductReferenceModel? GetProductReferenceById(int id)
        {
            return _context.ProductReferences
                .Include(p => p.ProductItems)
                .Include(p => p.ProductCategory)
                .AsNoTracking()
                .SingleOrDefault(p => p.Id == id);
        }

        public ProductReferenceModel CreateProductReference(ProductReferenceModel newProductReference, int productCategoryId)
        {
            var productCategory = _context.ProductCategories.Find(productCategoryId);

            if (productCategory == null)
            {
                throw new InvalidOperationException("Product category was not found or doesn't exist");
            }

            newProductReference.ProductCategory = productCategory;

            _context.ProductReferences.Add(newProductReference);
            _context.SaveChanges();

            return newProductReference;
        }

        public void UpdateProductCategoryOnProductReference(int productReferenceId, int productCategoryId)
        {
            var productReference = _context.ProductReferences.Find(productReferenceId);
            var productCategory = _context.ProductCategories.Find(productCategoryId);

            if (productCategory is null || productReference is null)
            {
                throw new InvalidOperationException("Product category or product reference was not found or doesn't exist");
            }

            productReference.ProductCategory = productCategory;

            _context.SaveChanges();
        }

        public void DeleteProductReferenceById(int productReferenceId)
        {
            var productReferenceToDelete = _context.ProductReferences.Find(productReferenceId);

            if (productReferenceToDelete is null)
            {
                throw new InvalidOperationException("Product reference was not found or doesn't exist");
            } else
            {
                _context.ProductReferences.Remove(productReferenceToDelete);
                _context.SaveChanges();
            }
        }

        public void UpdateProductReferenceById(int productReferenceId, ProductReferenceModel newProductReference)
        {
            var productReferenceToUpdate = _context.ProductReferences.Find(productReferenceId);

            if (productReferenceToUpdate is null)
            {
                throw new InvalidOperationException("Product category was not found or doesn't exist");
            }
            else
            {
                productReferenceToUpdate.Name = newProductReference.Name ?? productReferenceToUpdate.Name;
                productReferenceToUpdate.Description = newProductReference.Description ?? productReferenceToUpdate.Description;
                
                productReferenceToUpdate.Price = newProductReference.Price;
                productReferenceToUpdate.Discount = newProductReference.Discount;

                if (productReferenceToUpdate.ProductCategory.Id != newProductReference.ProductCategory.Id)
                {
                    productReferenceToUpdate.ProductCategory = newProductReference.ProductCategory;
                }
                
                _context.ProductReferences.Update(productReferenceToUpdate);
            }
        }
    }
}
