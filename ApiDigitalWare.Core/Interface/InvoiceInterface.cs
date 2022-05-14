
using ApiDigitalWare.Core.Entities;

namespace ApiDigitalWare.Core.Interface
{
    public interface InvoiceInterface
    {
        List<TbInvoice> GetInovices();


        void CreateInvoice(TbInvoice header, List<TbInvoiceDetail> detail);

        void UpdateInvoice(decimal id, TbInvoice header, List<TbInvoiceDetail> detail);

        void DeleteInvoice(decimal id);

        CompleteInvoiceDataEntity GetInvoiceById(decimal id);

        Guid GenerateConsecutive();
    }
}
