using DocuSign.eSign.Api;
using DocuSign.MyWebForms.Services.Clients;
using DocuSign.MyWebForms.Services.Database;
using System.IO;
using System.Threading.Tasks;

namespace DocuSign.MyWebForms.Services.Documents.Implementation;

public class DocumentService : IDocumentService
{
    private readonly IDocuSignClientsFactory _docuSignClientsFactory;
    private readonly IAccountRepository _accountRepository;

    public DocumentService(
        IDocuSignClientsFactory docuSignClientsFactory, 
        IAccountRepository accountRepository)
    {
        _docuSignClientsFactory = docuSignClientsFactory;
        _accountRepository = accountRepository;
    }

    public async Task<Stream> GetDocument(
        string envelopeId, 
        string documentId)
    {
        var docuSignClient = _docuSignClientsFactory.BuildDocuSignBaseClient();

        var envelopesApi = new EnvelopesApi(docuSignClient);

        var documentStream = await envelopesApi.GetDocumentAsync(_accountRepository.AccountId, envelopeId, documentId);

        return documentStream;
    }
}