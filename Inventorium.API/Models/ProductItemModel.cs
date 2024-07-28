using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventorium.API.Models
{
    public class ProductItemModel
    {
        // the constructor

        // the properties
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [MinLength(1)]
        public string Name { get; set; } = string.Empty;

        // the relations
        // product item belongs to product reference
        public int ProductReferenceId { get; set; }
        public ProductReferenceModel? ProductReference { get; set; }
    }
}
