using DocuSign.eSign.Client;
using DocuSignWebFormsClient = DocuSign.WebForms.Client.DocuSignClient;

namespace DocuSign.MyWebForms.Services.Clients;

public interface IDocuSignClientsFactory
{
    DocuSignWebFormsClient BuildDocuSignWebFormsApiClient();

    DocuSignClient BuildDocuSignAuthClient(string authServer);

    DocuSignClient BuildDocuSignBaseClient();
}