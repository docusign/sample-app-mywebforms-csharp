using DocuSign.eSign.Model;
using DocuSign.MyWebForms.Models.Documents;

namespace DocuSign.MyWebForms.Services.Documents.Implementation;

public class DocumentModelConverter : IDocumentModelConverter
{
    public DocumentModel Convert(EnvelopeDocument envelopeDocument)
        => new()
        {
            Id = envelopeDocument.DocumentId,
            Name = envelopeDocument.Name,
        };
}