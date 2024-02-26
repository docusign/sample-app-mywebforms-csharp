using DocuSign.eSign.Model;
using DocuSign.MyWebForms.Models.Documents;

namespace DocuSign.MyWebForms.Services.Documents;

public interface IDocumentModelConverter
{
    DocumentModel Convert(EnvelopeDocument envelopeDocument);
}