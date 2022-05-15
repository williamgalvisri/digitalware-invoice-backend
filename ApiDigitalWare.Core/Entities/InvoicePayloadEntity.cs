using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDigitalWare.Core.Entities
{
    public class InvoicePayloadEntity
    {
        public decimal IdCustomer { get; set; }
        public Guid Consecutive { get; set; }
        public DateTime DatePurchase { get; set; }
        public List<ProductInvoiceDetailEntity>? Products { get; set; }
    }
}
