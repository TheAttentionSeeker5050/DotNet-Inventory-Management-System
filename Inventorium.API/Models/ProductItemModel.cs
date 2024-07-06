namespace Inventorium.API.Models
{
    public class ProductItemModel
    {
        // the constructor

        // the properties
        public int Id { get; set; }
        public string Name { get; set; }

        // the relations
        // product item belongs to product reference
        // public int ProductReferenceId { get; set; }
        public ProductReferenceModel ProductReference { get; set; }
    }
}
