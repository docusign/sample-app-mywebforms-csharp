using DocuSign.eSign.Model;
using DocuSign.MyWebForms.Models.Form;
using DocuSign.MyWebForms.Models.Statuses;

namespace DocuSign.MyWebForms.Services.Statuses;

public interface IEnvelopeStatusModelConverter
{
    EnvelopeStatusModel Convert(Envelope envelope);
}