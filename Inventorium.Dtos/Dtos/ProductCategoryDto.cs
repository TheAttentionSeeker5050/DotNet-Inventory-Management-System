namespace Inventorium.Dtos.Dtos
{
    public class ProductCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<ProductReferenceForProductCategoryDto>? ProductReferences { get; set; }

    }

    public class ProductReferenceForProductCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public double Price { get; set; }
        public double Discount { get; set; }
    }
}
