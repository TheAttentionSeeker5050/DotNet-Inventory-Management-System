namespace Inventorium.Dtos.Dtos
{
    public class ProductReferenceDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public double Price { get; set; } = 0;
        public double? DiscountedPrice { get; set; } = 0;
        public double Discount { get; set; } = 0;

        // now the relations of the product references
        public int ProductCategoryId { get; set; }
        public string ProductCategoryName { get; set; }
        public string ProductCategoryDescription { get; set; }

        public List<ProductItemForProductReferenceDto>? ProductItems { get; set; }
    }

    public class ProductItemForProductReferenceDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

}
