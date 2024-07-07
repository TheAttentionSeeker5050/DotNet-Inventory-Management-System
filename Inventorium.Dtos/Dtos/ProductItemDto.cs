﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventorium.Dtos.Dtos
{
    public class ProductItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int ProductReferenceId { get; set; }
        public string ProductReferenceName { get; set; }

        public double Price { get; set; }
        public double Discount { get; set; }
    }
}
