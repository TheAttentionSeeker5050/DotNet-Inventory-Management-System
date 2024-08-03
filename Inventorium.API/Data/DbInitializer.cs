using Inventorium.Models;

namespace Inventorium.API.Data
{
    public static class DbInitializer
    {
        public async static void Initialize(InventoriumDbContext context)
        {
            // This method initialized the database with seed data if there isn't any
            // And it is triggered with IHost Extension method

            // if there are any values on this, return
            if (context.ProductItems.Any() && context.ProductItems.Any() && context.ProductReferences.Any())
            {
                return;
            }

            var categoryBed = new ProductCategoryModel { Name = "Bed" };
            var categoryBath = new ProductCategoryModel { Name = "Bath" };
            var categoryBeyond = new ProductCategoryModel { Name = "Beyond" };

            var referencePillow = new ProductReferenceModel { Name = "Pillow", Price = 40.99, ProductCategory = categoryBed };
            var referenceSheet = new ProductReferenceModel { Name = "Sheets, King Size", Price = 45.99, ProductCategory = categoryBed };

            var referenceTowel = new ProductReferenceModel { Name = "Towel, Medium", Price = 15.99, ProductCategory = categoryBath };
            var referenceLotion = new ProductReferenceModel { Name = "Lotion, Large", Price = 10.99, ProductCategory = categoryBath };
            var referenceSoap = new ProductReferenceModel { Name = "Soap, Pack of 6", Price = 16.99, ProductCategory = categoryBath };


            var itemPillow1 = new ProductItemModel { Name = "Pillow Ref 6955454", ProductReference = referencePillow };
            var itemPillow2 = new ProductItemModel { Name = "Pillow Ref 4242454", ProductReference = referencePillow };
            var itemPillow3 = new ProductItemModel { Name = "Pillow Ref 5165166", ProductReference = referencePillow };

            var itemSheet1 = new ProductItemModel { Name = "Sheets, King Size Ref 635654", ProductReference = referenceSheet };
            var itemSheet2 = new ProductItemModel { Name = "Sheets, King Size Ref 454545", ProductReference = referenceSheet };
            var itemSheet3 = new ProductItemModel { Name = "Sheets, King Size Ref 367367", ProductReference = referenceSheet };

            var itemTowel1 = new ProductItemModel { Name = "Towel, Medium Ref 569556", ProductReference = referenceTowel };
            var itemTowel2 = new ProductItemModel { Name = "Towel, Medium Ref 453685", ProductReference = referenceTowel };
            var itemTowel3 = new ProductItemModel { Name = "Towel, Medium Ref 456464", ProductReference = referenceTowel };
            var itemTowel4 = new ProductItemModel { Name = "Towel, Medium Ref 454554", ProductReference = referenceTowel };
            var itemTowel5 = new ProductItemModel { Name = "Towel, Medium Ref 453453", ProductReference = referenceTowel };

            var itemLotion1 = new ProductItemModel { Name = "Lotion, Large Ref 458645", ProductReference = referenceLotion };

            // Now add these to the database on fresh start
            // first add the categories
            // make a list of categories
            await context.ProductCategories.AddRangeAsync(
                new List<ProductCategoryModel>
                {
                    categoryBed,
                    categoryBath,
                    categoryBeyond,
                }
            );

            // add the references
            await context.ProductReferences.AddRangeAsync(
                new List<ProductReferenceModel>
                { 
                    referenceLotion,
                    referenceTowel,
                    referenceSheet,
                    referenceSoap,
                    referenceTowel
                }
            );

            // now add the items
            await context.ProductItems.AddRangeAsync(
                new List<ProductItemModel>
                {
                    itemPillow1, itemPillow2, itemPillow3, itemSheet1, itemSheet2,
                    itemSheet3, itemTowel1, itemTowel2, itemTowel3, itemTowel4,
                    itemTowel5, itemLotion1
                }
            );

            context.SaveChanges();

        }
    }
}
