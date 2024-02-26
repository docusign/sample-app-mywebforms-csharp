using DocuSign.eSign.Client;
using DocuSign.MyWebForms.Services.Database;
using DocuSignWebFormsClient = DocuSign.WebForms.Client.DocuSignClient;
using WebFormsConfiguration = DocuSign.WebForms.Client.Configuration;

namespace DocuSign.MyWebForms.Services.Clients.Implementation;

public class DocuSignClientsFactory : IDocuSignClientsFactory
{
    private readonly IAccountRepository _accountRepository;

    public DocuSignClientsFactory(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public DocuSignWebFormsClient BuildDocuSignWebFormsApiClient()
    {
        var docuSignConfig = new WebFormsConfiguration(_accountRepository.WebFormsUri);
        docuSignConfig.AddDefaultHeader("Authorization", "Bearer " + _accountRepository.AccessToken);
        var apiClient = new DocuSignWebFormsClient(docuSignConfig);
        apiClient.SetBasePath(docuSignConfig.BasePath);
        return apiClient;
    }

    public DocuSignClient BuildDocuSignAuthClient(string authServer)
    {
        var client = new DocuSignClient();
        client.SetOAuthBasePath(authServer);
        return client;
    }

    public DocuSignClient BuildDocuSignBaseClient()
    {
        var client = new DocuSignClient(_accountRepository.BaseUri + "/restapi");
        client.Configuration.AddDefaultHeader("Authorization", "Bearer " + _accountRepository.AccessToken);

        return client;
    }
}