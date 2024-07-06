using System.ComponentModel.DataAnnotations;

namespace Inventorium.API.Models
{
    public class ProductCategoryModel
    {
        // the constructor

        // the properties
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [MaxLength(1500)]
        public string Description { get; set; }

        public ICollection<ProductReferenceModel> ProductReferences { get; } = 
            new List<ProductReferenceModel>(); // collection of product references associated with this product category
    }
}
