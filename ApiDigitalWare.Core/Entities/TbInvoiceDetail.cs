using System;
using System.Collections.Generic;

namespace ApiDigitalWare.Core.Entities
{
    public partial class TbInvoiceDetail
    {
        public decimal Id { get; set; }
        public decimal Count { get; set; }
        public decimal IdProductFk { get; set; }
        public decimal IdInvoiceFk { get; set; }

        public virtual TbInvoice IdInvoiceFkNavigation { get; set; } = null!;
        public virtual TbProduct IdProductFkNavigation { get; set; } = null!;
    }
}
