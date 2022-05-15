using ApiDigitalWare.Infrastructure.Models;
using ApiDigitalWare.Core.Interface;
using ApiDigitalWare.Core.Entities;

namespace ApiDigitalWare.Infrastructure.Repositories
{
    public class ProductRepositories: ProductInterface
    {
        private db_system_digitalwareContext _db;
        public ProductRepositories(db_system_digitalwareContext db)
        {
            _db = db;
        }

        public List<ProductWithPrice> GetProductsWithPrice()
        {
            var listProductWithPrice = QueryProductWithPrice().ToList();
            return listProductWithPrice;
        }

        private List<ProductWithPrice> QueryProductWithPrice()
        {
            var invoices = (from a in _db.TbProducts
                            join b in _db.TbPriceListDetails on a.Id equals b.IdProductFk
                            select new ProductWithPrice
                            {
                                Id = a.Id,
                                Name = a.Name,
                                Price = b.Price,
                            }).ToList();
            return invoices;
        }

    }
}
