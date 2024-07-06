namespace Inventorium.API.Models
{
    public class ProductReferenceModel
    {
        // the constructor

        // the properties
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }

        // the relations
        // product reference belongs to category
        // public int ProductCategoryId { get; set; }
        public ProductCategoryModel ProductCategory { get; set; }
    }
}
