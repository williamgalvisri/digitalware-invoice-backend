using ApiDigitalWare.Infrastructure.Models;
using ApiDigitalWare.Core.Interface;
using ApiDigitalWare.Core.Entities;

namespace ApiDigitalWare.Infrastructure.Repositories
{
    public class CustomerRepositories: CustomerInterface
    {
        private db_system_digitalwareContext _db;
        public CustomerRepositories(db_system_digitalwareContext db)
        {
            _db = db;
        }

        public List<TbCustomer> GetCustomers()
        {
            var listCustomer = _db.TbCustomers.ToList();
            return listCustomer;
        }

    }
}
