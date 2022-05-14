using ApiDigitalWare.Core.Interface;
using ApiDigitalWare.Core.Entities;
using ApiDigitalWare.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsApiDigitalWare.Class
{
    internal class TestCustomersClass: CustomerInterface
    {
        public List<TbCustomer> GetCustomers()
        {
            //var result = _db.TbCustomers.ToList();
            return new List<TbCustomer>();
        }
    }
}
