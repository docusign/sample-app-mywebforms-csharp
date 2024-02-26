using DocuSign.MyWebForms.Models.Form;
using DocuSign.MyWebForms.Services.Clients;
using DocuSign.MyWebForms.Services.Configuration;
using DocuSign.WebForms.Api;
using DocuSign.WebForms.Client;
using DocuSign.WebForms.Model;
using System;
using System.Linq;

namespace DocuSign.MyWebForms.Services.Form.Implementation;

public class FormService : IFormService
{
    private readonly IAppConfiguration _appConfiguration;
    private readonly IDocuSignClientsFactory _docuSignClientsFactory;

    public FormService(
        IAppConfiguration appConfiguration,
        IDocuSignClientsFactory docuSignClientsFactory)
    {
        _appConfiguration = appConfiguration;
        _docuSignClientsFactory = docuSignClientsFactory;
    }

    public EmbedWebFormOutput EmbedForm(LoanType loanType)
    {
        var apiClient = _docuSignClientsFactory.BuildDocuSignWebFormsApiClient();
        var forms = GetForms(apiClient, _appConfiguration.DocuSign.AccountId);
        var formId = forms.Items.First(x => x.FormProperties.Name == GetTemplateName(loanType)).Id;
        var form = CreateInstance(apiClient, _appConfiguration.DocuSign.AccountId, formId);

        return new EmbedWebFormOutput
        {
            Id = form.Id,
            FormId = form.FormId,
            InstanceToken = form.InstanceToken,
            IntegrationKey = _appConfiguration.DocuSign.UserId,
            Url = form.FormUrl
        };
    }

    private string GetTemplateName(LoanType loanType) =>
        loanType switch
        {
            LoanType.Personal => _appConfiguration.DocuSign.PersonalLoanTemplateName,
            LoanType.Auto => _appConfiguration.DocuSign.AutoLoanTemplateName,
            LoanType.Sailboat => _appConfiguration.DocuSign.SailboatLoanTemplateName,
            _ => _appConfiguration.DocuSign.PersonalLoanTemplateName
        };

    private static WebFormSummaryList GetForms(DocuSignClient docuSignClient, string accountId)
    {
        var formManagementApi = new FormManagementApi(docuSignClient);
        return formManagementApi.ListForms(accountId);
    }

    private WebFormInstance CreateInstance(
        DocuSignClient docuSignClient,
        string accountId,
        string formId)
    {
        var formManagementApi = new FormInstanceManagementApi(docuSignClient);
        var options = new CreateInstanceRequestBody()
        {
            ClientUserId = _appConfiguration.DocuSign.UserId,
            ExpirationOffset = 3600
        };

        return formManagementApi.CreateInstance(accountId, formId, options);
    }
}