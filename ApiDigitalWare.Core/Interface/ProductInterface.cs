
using ApiDigitalWare.Core.Entities;

namespace ApiDigitalWare.Core.Interface
{
    public interface ProductInterface
    {
        List<ProductWithPrice> GetProductsWithPrice();
    }
}
