using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDigitalWare.Core.Entities
{
    public class ProductPriceEntity
    {
        public decimal Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public decimal Count { get; set; }

    }
}
