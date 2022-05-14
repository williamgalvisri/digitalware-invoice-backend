using ApiDigitalWare.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiDigitalWare.Infrastructure.Models
{
    public partial class db_system_digitalwareContext : DbContext
    {
        public db_system_digitalwareContext()
        {
        }

        public db_system_digitalwareContext(DbContextOptions<db_system_digitalwareContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TbCustomer> TbCustomers { get; set; } = null!;
        public virtual DbSet<TbInventory> TbInventories { get; set; } = null!;


        public virtual DbSet<TbInvoice> TbInvoices { get; set; } = null!;
        public virtual DbSet<TbInvoiceDetail> TbInvoiceDetails { get; set; } = null!;
        public virtual DbSet<TbPriceList> TbPriceLists { get; set; } = null!;
        public virtual DbSet<TbPriceListDetail> TbPriceListDetails { get; set; } = null!;
        public virtual DbSet<TbProduct> TbProducts { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TbCustomer>(entity =>
            {
                entity.ToTable("tb_customers");

                entity.Property(e => e.Id)
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.Birthday)
                    .HasColumnType("datetime")
                    .HasColumnName("birthday");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.Dni)
                    .HasMaxLength(11)
                    .HasColumnName("dni")
                    .IsFixedLength();

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .HasColumnName("last_name");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.TypeDni)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("type_dni")
                    .IsFixedLength();

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<TbInventory>(entity =>
            {
                entity.ToTable("tb_inventory");

                entity.Property(e => e.Id)
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Count)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("count");

                entity.Property(e => e.IdProductFk)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("id_product_fk");

                entity.HasOne(d => d.IdProductFkNavigation)
                    .WithMany(p => p.TbInventories)
                    .HasForeignKey(d => d.IdProductFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tb_products_tb_inventory");
            });

            modelBuilder.Entity<TbInvoice>(entity =>
            {
                entity.ToTable("tb_invoices");

                entity.Property(e => e.Id)
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.Consecutive).HasColumnName("consecutive");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.DatePurchase)
                    .HasColumnType("datetime")
                    .HasColumnName("date_purchase");

                entity.Property(e => e.IdCustomerFk)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("id_customer_fk");

                entity.Property(e => e.IdPriceListFk)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("id_price_list_fk");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.IdCustomerFkNavigation)
                    .WithMany(p => p.TbInvoices)
                    .HasForeignKey(d => d.IdCustomerFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tb_invoices_tb_customers");

                entity.HasOne(d => d.IdPriceListFkNavigation)
                    .WithMany(p => p.TbInvoices)
                    .HasForeignKey(d => d.IdPriceListFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tb_invoices_tb_price_list");
            });

            modelBuilder.Entity<TbInvoiceDetail>(entity =>
            {
                entity.ToTable("tb_invoice_detail");

                entity.Property(e => e.Id)
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Count)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("count");

                entity.Property(e => e.IdInvoiceFk)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("id_invoice_fk");

                entity.Property(e => e.IdProductFk)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("id_product_fk");

                entity.HasOne(d => d.IdInvoiceFkNavigation)
                    .WithMany(p => p.TbInvoiceDetails)
                    .HasForeignKey(d => d.IdInvoiceFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tb_invoice_detail_tb_invoices");

                entity.HasOne(d => d.IdProductFkNavigation)
                    .WithMany(p => p.TbInvoiceDetails)
                    .HasForeignKey(d => d.IdProductFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tb_invoice_detail_tb_products");
            });

            modelBuilder.Entity<TbPriceList>(entity =>
            {
                entity.ToTable("tb_price_list");

                entity.Property(e => e.Id)
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.Description)
                    .HasMaxLength(150)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.UpdateAt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_at");
            });

            modelBuilder.Entity<TbPriceListDetail>(entity =>
            {
                entity.ToTable("tb_price_list_detail");

                entity.Property(e => e.Id)
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.IdPriceListFk)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("id_price_list_fk");

                entity.Property(e => e.IdProductFk)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("id_product_fk");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("price");

                entity.HasOne(d => d.IdPriceListFkNavigation)
                    .WithMany(p => p.TbPriceListDetails)
                    .HasForeignKey(d => d.IdPriceListFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tb_price_list_detail_tb_price_list");

                entity.HasOne(d => d.IdProductFkNavigation)
                    .WithMany(p => p.TbPriceListDetails)
                    .HasForeignKey(d => d.IdProductFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tb_price_list_detail_tb_products");
            });

            modelBuilder.Entity<TbProduct>(entity =>
            {
                entity.ToTable("tb_products");

                entity.Property(e => e.Id)
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.Color)
                    .HasMaxLength(15)
                    .HasColumnName("color")
                    .IsFixedLength();

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("date")
                    .HasColumnName("created_at");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.UpdateAt)
                    .HasColumnType("date")
                    .HasColumnName("update_at");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
