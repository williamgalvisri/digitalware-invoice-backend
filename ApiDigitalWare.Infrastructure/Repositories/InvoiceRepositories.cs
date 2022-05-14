using ApiDigitalWare.Infrastructure.Models;
using ApiDigitalWare.Core.Interface;
using ApiDigitalWare.Core.Entities;

namespace ApiDigitalWare.Infrastructure.Repositories
{
    public class InvoiceRepositories: InvoiceInterface
    {
        private db_system_digitalwareContext _db;

        private InventorieRepositories _inventoriRepositories;
        public InvoiceRepositories(db_system_digitalwareContext db, InventorieRepositories inventorieRepositories)
        {
            _db = db;
            _inventoriRepositories = inventorieRepositories;
        }

        public List<TbInvoice> GetInovices()
        {
            var response = _db.TbInvoices.ToList();
            return response;
        }

        public void CreateInvoice(TbInvoice header, List<TbInvoiceDetail> detail)
        {
            try
            {
                _db.TbInvoices.Add(header);
                for (int i = 0; i < detail.Count; i++)
                {
                    _db.TbInvoiceDetails.Add(detail[i]);
                }
                _db.SaveChanges();
            } catch(Exception)
            {
                throw;
            }
        }

        public Guid GenerateConsecutive()
        {
            return Guid.NewGuid();
        }

        /// <summary>
        /// Update Invoice if the details was modified
        /// </summary>
        /// <param name="id"></param>
        /// <param name="header"></param>
        /// <param name="detail"></param>
        public void UpdateInvoice(decimal id, TbInvoice header, List<TbInvoiceDetail> detail)
        {
            try
            {
                var invoice = _db.TbInvoices.Where(t => t.Id == id).First();
                invoice = header;
                var invoiceDetail = _db.TbInvoiceDetails.Where(t => t.IdInvoiceFk == id).ToList();
                // We need discount in stock inventory
                DiscountStockProductInvoiceDetail(invoiceDetail);

                // Update each deatil invoice
                for (int i = 0; i < detail.Count; i++)
                {
                    var invoiceFound = invoiceDetail.Find(t => t.Id == detail[i].Id);
                    invoiceFound = detail[i];
                }
                _db.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// for each product on detail update stock
        /// </summary>
        /// <param name="detail"></param>
        private void DiscountStockProductInvoiceDetail(List<TbInvoiceDetail> detail)
        {
            for (int i = 0; i < detail.Count; i++ )
            {
                var productOnDetail = detail[i];
                _inventoriRepositories.UpdateStockProduct(productOnDetail.IdProductFk, productOnDetail.Count);
            }
        }

        /// <summary>
        /// InActivate Invoice
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="Exception"></exception>
        public void DeleteInvoice(decimal id)
        {
            var invoice = _db.TbInvoices.Where(t => t.Id == id).First();
            if (invoice != null)
            {
                invoice.Active = false;
                _db.SaveChanges();
            } else
            {
                throw new Exception("This invoice not exists");
            }
        }

        /// <summary>
        /// Get all invoice with his deatail
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public CompleteInvoiceDataEntity GetInvoiceById(decimal id)
        {
            var invoice = _db.TbInvoices.Where(t => t.Id == id).First();

            // Get list products with price
            var queryProduct = GetProductDetailWithPrice(id);

            // Get invoice complete
            var queryCompleteInvoice = GetCompleteInvoice(id, queryProduct);


            if (invoice != null)
            {
                return queryCompleteInvoice;
            } else
            {
                throw new Exception("This invoice not exists");
            }
        }

        private List<ProductPriceEntity> GetProductDetailWithPrice(decimal id)
        {
            var response = (
                        from p in _db.TbProducts
                        join l in _db.TbInvoiceDetails on p.Id equals l.IdProductFk
                        join s in _db.TbPriceListDetails on l.IdProductFk equals s.IdProductFk
                        where l.IdInvoiceFk == id
                        select new ProductPriceEntity
                        {
                            Id = p.Id,
                            Name = p.Name,
                            Count = l.Count,
                            Price = s.Price
                        }
                    ).ToList();

            return response;
        }

        private CompleteInvoiceDataEntity GetCompleteInvoice(decimal id, List<ProductPriceEntity> queryProduct)
        {
            var response = (
                from a in _db.TbInvoices
                join b in _db.TbInvoiceDetails on a.Id equals b.IdInvoiceFk
                join c in _db.TbCustomers on a.IdCustomerFk equals c.Id
                where a.Id == id
                select new CompleteInvoiceDataEntity
                {
                    Id = a.Id,
                    IdCustomerFk = a.IdCustomerFk,
                    Dni = c.Dni,
                    Name = c.Name,
                    LastName = c.LastName,
                    Consecutive = a.Consecutive,
                    Products = queryProduct
                }
              ).First();
            return response;
        }

    }


}
