using System;
using System.Collections.Generic;

namespace ApiDigitalWare.Core.Entities
{
    public partial class TbPriceList
    {
        public TbPriceList()
        {
            TbInvoices = new HashSet<TbInvoice>();
            TbPriceListDetails = new HashSet<TbPriceListDetail>();
        }

        public decimal Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }

        public virtual ICollection<TbInvoice> TbInvoices { get; set; }
        public virtual ICollection<TbPriceListDetail> TbPriceListDetails { get; set; }
    }
}
