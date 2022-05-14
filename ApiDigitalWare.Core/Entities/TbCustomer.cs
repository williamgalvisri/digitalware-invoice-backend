namespace ApiDigitalWare.Core.Entities
{
    public partial class TbCustomer
    {
        public TbCustomer()
        {
            TbInvoices = new HashSet<TbInvoice>();
        }

        public decimal Id { get; set; }
        public string Dni { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? LastName { get; set; }
        public string TypeDni { get; set; } = null!;
        public bool? Active { get; set; }
        public DateTime? Birthday { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<TbInvoice> TbInvoices { get; set; }
    }
}
