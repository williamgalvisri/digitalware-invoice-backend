using System;
using System.Collections.Generic;

namespace ApiDigitalWare.Core.Entities
{
    public partial class TbInvoice
    {
        public TbInvoice()
        {
            TbInvoiceDetails = new HashSet<TbInvoiceDetail>();
        }

        public decimal Id { get; set; }
        public Guid Consecutive { get; set; }
        public DateTime DatePurchase { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public decimal IdCustomerFk { get; set; }
        public decimal IdPriceListFk { get; set; }
        public bool? Active { get; set; }

        public virtual TbCustomer IdCustomerFkNavigation { get; set; } = null!;
        public virtual TbPriceList IdPriceListFkNavigation { get; set; } = null!;
        public virtual ICollection<TbInvoiceDetail> TbInvoiceDetails { get; set; }
    }
}
