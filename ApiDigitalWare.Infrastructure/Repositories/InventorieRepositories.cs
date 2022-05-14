

using ApiDigitalWare.Infrastructure.Models;

namespace ApiDigitalWare.Infrastructure.Repositories
{
    public class InventorieRepositories
    {
        private db_system_digitalwareContext _db;
        public InventorieRepositories(db_system_digitalwareContext db)
        {
            _db = db;
        }

        public void UpdateStockProduct(decimal productId, decimal count)
        {
            var productStock = _db.TbInventories.Where(i => i.IdProductFk == productId).First();
            productStock.Count = productStock.Count - count;
            _db.SaveChanges();
        }
    }
}
