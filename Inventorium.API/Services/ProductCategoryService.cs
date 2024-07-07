using Microsoft.EntityFrameworkCore;
using Inventorium.API.Data;
using Inventorium.API.Models;


namespace Inventorium.API.Services
{
    public class ProductCategoryService
    {
        // add the readonly property context
        private readonly InventoriumDbContext _context;

        // the constructor 
        public ProductCategoryService(InventoriumDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ProductCategoryModel> GetProductCategories() // This can be null list if the product categories are empty
        {
            return _context.ProductCategories
                /*.AsNoTracking() // This is to mark this query as read only to avoid any unwanted changes when 
                // the entry Linq relations change in some other row or table*/
                .Include(c => c.ProductReferences)
                .ToList();
        }

        public ProductCategoryModel? GetProductCategoryById(int id)
        {
            return _context.ProductCategories
                .Include(c => c.ProductReferences)
                .AsNoTracking()
                .SingleOrDefault(p => p.Id == id);
        }

        public ProductCategoryModel CreateProductCategory(ProductCategoryModel newProductCategory)
        {
            _context.ProductCategories.Add(newProductCategory);
            _context.SaveChanges();

            return newProductCategory;
        }

        public void DeleteProductCategoryById(int categoryId)
        {
            var productCategoryToDelete = _context.ProductCategories.Find(categoryId);

            if (productCategoryToDelete is null)
            {
                throw new InvalidOperationException("Product category was not found or doesn't exist");

            }
            else
            {
                _context.ProductCategories.Remove(productCategoryToDelete);
                _context.SaveChanges();
            }
        }

        public void UpdateProductCategoryById(int categoryId, ProductCategoryModel newProductCategory)
        {
            var productCategoryToUpdate = _context.ProductCategories.Find(categoryId);

            if (productCategoryToUpdate is null)
            { 
                throw new InvalidOperationException("Product category was not found or doesn't exist");
            }
            else
            {
                productCategoryToUpdate.Name = newProductCategory.Name ?? productCategoryToUpdate.Name;
                productCategoryToUpdate.Description = newProductCategory.Description ?? productCategoryToUpdate.Description;
                _context.ProductCategories.Update(productCategoryToUpdate);
            }
        }
    }
}
