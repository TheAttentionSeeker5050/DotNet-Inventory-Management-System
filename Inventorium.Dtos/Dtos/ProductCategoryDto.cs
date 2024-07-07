using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventorium.Dtos.Dtos
{
    public class ProductCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<ProductReferenceForProductCatgegoryDto>? ProductReferences { get; set; }

    }
    public class ProductReferenceForProductCatgegoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public double Price { get; set; }
        public double Discount { get; set; }
    }
}
