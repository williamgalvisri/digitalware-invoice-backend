namespace ApiDigitalWare.Core.Entities
{
    public partial class TbInventory
    {
        public decimal Id { get; set; }
        public decimal Count { get; set; }
        public decimal IdProductFk { get; set; }

        public virtual TbProduct IdProductFkNavigation { get; set; } = null!;
    }
}
