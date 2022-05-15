
using ApiDigitalWare.Core.Entities;

namespace ApiDigitalWare.Core.Interface
{
    public interface InvoiceInterface
    {
        List<InvoicesWithInformationCustomersEntity> GetInovices();

        void CreateInvoice(InvoicePayloadEntity payload);

        void UpdateInvoice(decimal id, InvoicePayloadEntity payload);

        void DeleteInvoice(decimal id);

        CompleteInvoiceDataEntity GetInvoiceById(decimal id);

        Guid GenerateConsecutive();

        CompleteInvoiceDataEntity GetInvoiceByConsecutive(Guid consecutive);
    }
}
