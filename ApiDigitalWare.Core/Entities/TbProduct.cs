using System;
using System.Collections.Generic;

namespace ApiDigitalWare.Core.Entities
{
    public partial class TbProduct
    {
        public TbProduct()
        {
            TbInventories = new HashSet<TbInventory>();
            TbInvoiceDetails = new HashSet<TbInvoiceDetail>();
            TbPriceListDetails = new HashSet<TbPriceListDetail>();
        }

        public decimal Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Color { get; set; } = null!;
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }

        public virtual ICollection<TbInventory> TbInventories { get; set; }
        public virtual ICollection<TbInvoiceDetail> TbInvoiceDetails { get; set; }
        public virtual ICollection<TbPriceListDetail> TbPriceListDetails { get; set; }
    }
}
