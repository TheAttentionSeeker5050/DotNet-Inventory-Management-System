using Microsoft.EntityFrameworkCore;
using Inventorium.API.Models;



namespace Inventorium.API.Data
{
    public class InventoriumDbContext : DbContext
    {
        // the constructor with db context options
        public InventoriumDbContext(DbContextOptions<InventoriumDbContext> options) : base(options) { }


        // add db sets of our models to inject into program.cs throught this context class
        public DbSet<ProductCategoryModel> ProductCategories => Set<ProductCategoryModel>();
        public DbSet<ProductReferenceModel> ProductReferences => Set<ProductReferenceModel>();
        public DbSet<ProductItemModel> ProductItems => Set<ProductItemModel>();


    }
}
