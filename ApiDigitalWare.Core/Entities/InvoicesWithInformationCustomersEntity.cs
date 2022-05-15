using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDigitalWare.Core.Entities
{
    public class InvoicesWithInformationCustomersEntity
    {
        public decimal Id { get; set; }
        public string Name { get; set; }
        public string? LastName { get; set; }
        public string Dni { get; set; }
        public Guid Consecutive { get; set; }
        public DateTime DatePurchase { get; set; }
    }
}
