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
        public string Name { get; set; } = string.Empty;

        [MaxLength(5000)]
        public string Description { get; set; } = string.Empty ;

        [Required]
        public double Price { get; set; } = 0;
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
