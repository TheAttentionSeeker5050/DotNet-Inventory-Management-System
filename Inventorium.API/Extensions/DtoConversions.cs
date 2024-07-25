using Inventorium.API.Models;
using Inventorium.Dtos.Dtos;


namespace Inventorium.API.Extensions
{
    public static class DtoConversions
    {
        // This will transform data we get from tables into something that our blazor app can use efficiently
        // with just the data that they will make use on and avoiding repeated elements as much as possible

        // Dto for displaying a single product category
        public static ProductCategoryDto ConvertToDto(this ProductCategoryModel productCategory, IEnumerable<ProductReferenceModel> productReferences)
        {
            var productReferenceDto = (from productReference in productReferences
                                       where productReference.ProductCategory == productCategory
                                       select new ProductReferenceForProductCategoryDto()
                                       {
                                           Id = productReference.Id,
                                           Name = productReference.Name,
                                           Description = productReference.Description,
                                           Price = productReference.Price,
                                           Discount = productReference.Discount,
                                           DiscountedPrice = productReference.Discount > 0 && 
                                           productReference.Discount < 100 ?  
                                           productReference.Price * 
                                           (100 - productReference.Discount)/100 
                                           : null
                                       });

            return new ProductCategoryDto()
            {
                Id = productCategory.Id,
                Name = productCategory.Name,
                Description = productCategory.Description,
                ProductReferences = productReferenceDto.ToList()
            };

        }

        // Dto for displaying a list of product categories
        public static IEnumerable<ProductCategoryDto> ConvertToDto(this IEnumerable<ProductCategoryModel> productCategories)
        {
            return (from productCategory in productCategories
                    select new ProductCategoryDto()
                    {
                        Id = productCategory.Id,
                        Name = productCategory.Name,
                        Description = productCategory.Description
                    }).ToList();

        }

        // Dto for displaying a single product reference
        public static ProductReferenceDto ConvertToDto(this ProductReferenceModel productReference, ProductCategoryModel productCategory, List<ProductItemModel> productItems)
        {
            var productItemsDto = new List<ProductItemForProductReferenceDto>();

            productItems.ForEach(productItem =>
            {
                productItemsDto.Add(new ProductItemForProductReferenceDto()
                {
                    Id = productItem.Id,
                    Name = productItem.Name,
                });

            });

            return new ProductReferenceDto()
            {
                Id = productReference.Id,
                Name = productReference.Name,
                Description = productReference.Description,
                Price = productReference.Price,
                Discount = productReference.Discount,
                DiscountedPrice = productReference.Discount > 0 &&
                                           productReference.Discount < 100 ?
                                           productReference.Price *
                                           (100 - productReference.Discount) / 100
                                           : null,

                ProductCategoryId = productCategory.Id,
                ProductCategoryName = productCategory.Name,
                ProductCategoryDescription = productCategory.Description,

                ProductItems = productItemsDto
            };
        }

        // Dto for displaying a list of product references
        public static IEnumerable<ProductReferenceDto> ConvertToDto(this IEnumerable<ProductReferenceModel> productReferences, IEnumerable<ProductCategoryModel> productCategories)
        {
            return (from productReference in productReferences
                    join productCategory in productCategories
                    on productReference.ProductCategory.Id equals productCategory.Id
                    select new ProductReferenceDto()
                    {
                        Id = productReference.Id,
                        Name = productReference.Name,
                        Description = productReference.Description,
                        Price = productReference.Price,
                        Discount = productReference.Discount,

                        // The product category properties
                        ProductCategoryId = productCategory.Id,
                        ProductCategoryName = productCategory.Name,
                        ProductCategoryDescription = productCategory.Description
                    }
                ).ToList();
        }

        // Dto for displaying a single product item
        public static ProductItemDto ConvertToDto(this ProductItemModel productItem, ProductReferenceModel productReference)
        {
            return new ProductItemDto()
            {
                Id = productItem.Id,
                Name = productItem.Name,
                ProductReferenceId = productReference.Id,
                ProductReferenceName = productReference.Name,
                Price = productReference.Price,
                Discount = productReference.Discount,
                DiscountedPrice = productReference.Discount > 0 &&
                                           productReference.Discount < 100 ?
                                           productReference.Price *
                                           (100 - productReference.Discount) / 100
                                           : null,
            };
        }


        // Dto for displaying a list product items
        public static IEnumerable<ProductItemDto> ConvertToDto(this IEnumerable<ProductItemModel> productItems, IEnumerable<ProductReferenceModel> productReferences)
        {
            return (from productItem in productItems
                    join productReference in productReferences
                    on productItem.ProductReference.Id equals productReference.Id
                    select new ProductItemDto()
                    {
                        Id = productItem.Id,
                        Name = productItem.Name,
                        ProductReferenceId = productReference.Id,
                        ProductReferenceName = productReference.Name,
                        Price = productReference.Price,
                        Discount = productReference.Discount
                    });
        }

        // Dto for displaying a list of search results
        public static IEnumerable<SearchOptionDto> ConvertToSearchDto(this IEnumerable<ProductCategoryModel> productCategories)
        {
            return (from productCategory in productCategories

                    select new SearchOptionDto()
                    {
                        OptionDisplayCategoryLabel = "Product Categories",
                        OptionResourceParent = "ProductCategories",
                        OptionResourceFrontendParent = "product-categories",
                        OptionDisplayValue = productCategory.Name,
                        OptionDisplayId = productCategory.Id,
                    });
        }


        public static IEnumerable<SearchOptionDto> ConvertToSearchDto(this IEnumerable<ProductItemModel> productItems)
        {
            return (from productItem in productItems

                    select new SearchOptionDto()
                    {

                        OptionDisplayCategoryLabel = "Product Items",
                        OptionResourceParent = "ProductItems",
                        OptionResourceFrontendParent = "product-items",
                        OptionDisplayValue = productItem.Name,
                        OptionDisplayId = productItem.Id,
                    });
        }

        public static IEnumerable<SearchOptionDto> ConvertToSearchDto(this IEnumerable<ProductReferenceModel> productReferences)
        {
            return (from productReference in productReferences

                    select new SearchOptionDto()
                    {

                        OptionDisplayCategoryLabel = "Product References",
                        OptionResourceParent = "ProductReferences",
                        OptionResourceFrontendParent = "product-references",
                        OptionDisplayValue = productReference.Name,
                        OptionDisplayId = productReference.Id,
                    });
        }

    }
}
