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

        public List<InvoicesWithInformationCustomersEntity> GetInovices()
        {
            return QueryInvoicesWithInformationCustomers();
        }

        public void  CreateInvoice(InvoicePayloadEntity payload)
        {
            try
            {
                var consecutive = _db.TbInvoices.Where(p => p.Consecutive == payload.Consecutive).FirstOrDefault();

                if (consecutive != null)
                {
                    throw new Exception("Already exist that consecutive");
                }
                var priceList = _db.TbPriceLists.First();
                List<TbInvoiceDetail> products = new List<TbInvoiceDetail>();
                for (int i = 0; i < payload.Products.Count; i++)
                {
                    products.Add(new TbInvoiceDetail {
                        IdProductFk = payload.Products[i].Id,
                        Count = payload.Products[i].Count
                    });
                }

                var invoice = new TbInvoice
                {
                    Consecutive = payload.Consecutive,
                    IdCustomerFk = payload.IdCustomer,
                    DatePurchase = payload.DatePurchase,
                    IdPriceListFk = priceList.Id,
                    TbInvoiceDetails = products,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Active = true
                };
                _db.Add(invoice);
                _db.SaveChangesAsync();
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
        public void UpdateInvoice(decimal id, InvoicePayloadEntity payload)
        {
            try
            {
                var invoice = _db.TbInvoices.Where(t => t.Id == id).FirstOrDefault();
                invoice.IdCustomerFk = payload.IdCustomer;
                 
                // Remove all products
                var productsStorage = _db.TbInvoiceDetails.Where(t => t.IdInvoiceFk == id).ToList();
                _db.TbInvoiceDetails.RemoveRange(productsStorage);
                if (invoice != null)
                {
                    List<TbInvoiceDetail> products = new List<TbInvoiceDetail>();
                    for (int i = 0; i < payload.Products.Count; i++)
                    {
                        products.Add(new TbInvoiceDetail
                        {
                            IdProductFk = payload.Products[i].Id,
                            Count = payload.Products[i].Count,
                            IdInvoiceFk = id
                        });
                    }
                    // Add new products
                    if (products.Count > 0)
                    {
                        _db.TbInvoiceDetails.AddRange(products);
                    }
                }
                _db.SaveChangesAsync();
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
            var queryProduct = QueryProductDetailWithPrice(id);

            // Get invoice complete
            var queryCompleteInvoice = QueryCompleteInvoice(id, queryProduct);


            if (invoice != null)
            {
                return queryCompleteInvoice;
            } else
            {
                throw new Exception("This invoice not exists");
            }
        }

        public CompleteInvoiceDataEntity GetInvoiceByConsecutive(Guid consecutive)
        {
            var invoice = _db.TbInvoices.Where(t => t.Consecutive == consecutive).First();

            // Get list products with price
            var queryProduct = QueryProductDetailWithPrice(invoice.Id);

            // Get invoice complete
            var queryCompleteInvoice = QueryCompleteInvoice(invoice.Id, queryProduct);


            if (invoice != null)
            {
                return queryCompleteInvoice;
            }
            else
            {
                throw new Exception("This invoice not exists");
            }
        }

        private List<ProductPriceEntity> QueryProductDetailWithPrice(decimal id)
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

        private CompleteInvoiceDataEntity QueryCompleteInvoice(decimal id, List<ProductPriceEntity> queryProduct)
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
                    DatePurchase = a.DatePurchase,
                    Consecutive = a.Consecutive,
                    Products = queryProduct
                }
              ).FirstOrDefault();
            if (response == null)
            {
                response = (from a in _db.TbInvoices
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
                                DatePurchase = a.DatePurchase,
                                Products = new List<ProductPriceEntity>()
                            }).First();
            }
            return response;
        }

        private List<InvoicesWithInformationCustomersEntity> QueryInvoicesWithInformationCustomers()
        {
            var invoices = (from a in _db.TbInvoices
                            join b in _db.TbCustomers on a.IdCustomerFk equals b.Id
                            select new InvoicesWithInformationCustomersEntity
                            {
                                Id = a.Id,
                                Name = b.Name,
                                LastName = b.LastName,
                                Dni = b.Dni,
                                Consecutive = a.Consecutive,
                                DatePurchase = a.DatePurchase
                            }).ToList();
            return invoices;
        }



    }


}
