using System.ComponentModel.DataAnnotations;

namespace Inventorium.Models
{
    public class ProductCategoryModel
    {
        // the constructor

        // the properties
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [MaxLength(1500)]
        public string Description { get; set; } = string.Empty ;

        public ICollection<ProductReferenceModel>? ProductReferences { get; } = 
            new List<ProductReferenceModel>(); // collection of product references associated with this product category
    }
}
