using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDigitalWare.Core.Entities
{
    public class ProductWithPrice
    {
        public decimal Id { get; set; }
        public decimal Price { get; set; }
        public string? Name { get; set; }

    }
}
