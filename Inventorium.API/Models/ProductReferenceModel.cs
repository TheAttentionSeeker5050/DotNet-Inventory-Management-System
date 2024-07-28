using System.ComponentModel.DataAnnotations;

namespace Inventorium.API.Models
{
    public class ProductReferenceModel
    {
        // the constructor

        // the properties
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [MinLength(1)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(5000)]
        public string Description { get; set; } = string.Empty ;

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price should be a positive number")]
        public double Price { get; set; } = 0;

        [Range(0,100, ErrorMessage = "Discount is a percentage, so it should be anywhere from 0 to 100")]
        public double Discount { get; set; }

        // the relations
        // product reference belongs to category
        public int ProductCategoryId { get; set; }

        public ProductCategoryModel? ProductCategory { get; set; }

        // product reference has many items
        public ICollection<ProductItemModel>? ProductItems { get; } = 
            new List<ProductItemModel>(); // collection of product items associated with this product reference


    }
}
