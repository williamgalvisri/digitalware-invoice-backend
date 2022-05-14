using System;
using System.Collections.Generic;

namespace ApiDigitalWare.Core.Entities
{
    public partial class TbPriceListDetail
    {
        public decimal Id { get; set; }
        public decimal Price { get; set; }
        public decimal IdPriceListFk { get; set; }
        public decimal IdProductFk { get; set; }

        public virtual TbPriceList IdPriceListFkNavigation { get; set; } = null!;
        public virtual TbProduct IdProductFkNavigation { get; set; } = null!;
    }
}
