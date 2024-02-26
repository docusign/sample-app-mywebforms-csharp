using System.IO;
using System.Threading.Tasks;

namespace DocuSign.MyWebForms.Services.Documents;

public interface IDocumentService
{
    Task<Stream> GetDocument(
        string envelopeId,
        string documentId);
}