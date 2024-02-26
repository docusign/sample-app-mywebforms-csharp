using DocuSign.eSign.Api;
using DocuSign.eSign.Model;
using DocuSign.MyWebForms.Models.Form;
using DocuSign.MyWebForms.Models.Statuses;
using DocuSign.MyWebForms.Services.Clients;
using DocuSign.MyWebForms.Services.Database;
using DocuSign.WebForms.Api;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocuSign.MyWebForms.Services.Statuses.Implementation;

public class StatusService : IStatusService
{
    private readonly IEnvelopeStatusRepository _envelopeStatusRepository;
    private readonly IEnvelopeStatusModelConverter _envelopeStatusModelConverter;
    private readonly IDocuSignClientsFactory _docuSignClientsFactory;
    private readonly IAccountRepository _accountRepository;

    public StatusService(
        IEnvelopeStatusRepository envelopeStatusRepository,
        IEnvelopeStatusModelConverter envelopeStatusModelConverter,
        IDocuSignClientsFactory docuSignClientsFactory,
        IAccountRepository accountRepository)
    {
        _envelopeStatusRepository = envelopeStatusRepository;
        _envelopeStatusModelConverter = envelopeStatusModelConverter;
        _docuSignClientsFactory = docuSignClientsFactory;
        _accountRepository = accountRepository;
    }

    public List<EnvelopeStatusModel> GetStatuses()
        => _envelopeStatusRepository.GetStatuses(_accountRepository.UniqueSessionId);

    public async Task<EnvelopeCurrentModel> AddOrReplaceStatus(CompleteSigningInput input)
    {
        var accountId = _accountRepository.AccountId;
        EnvelopeCurrentModel envelopeCurrentModel = null;
        string envelopeId;
        if (input.ExistingEnvelope)
        {
            envelopeCurrentModel = _envelopeStatusRepository.GetCurrentEnvelope(_accountRepository.UniqueSessionId);
            envelopeId = envelopeCurrentModel.EnvelopeId;
        }
        else
        {
            envelopeId = await GetEnvelopeId(accountId, input.FormId, input.Id);
        }
        var envelope = await GetEnvelope(accountId, envelopeId, envelopeCurrentModel, input.IsFocusedView);
        var envelopeStatusModel = _envelopeStatusModelConverter.Convert(envelope);

        AddOrReplaceStatus(_accountRepository.UniqueSessionId, envelopeStatusModel);

        if(input.IsFocusedView)
        {
            return null;
        }

        return envelopeCurrentModel ?? _envelopeStatusRepository.GetCurrentEnvelope(_accountRepository.UniqueSessionId);
    }

    public void ClearStatuses()
        => _envelopeStatusRepository.RemoveStatuses(_accountRepository.UniqueSessionId);

    private async Task<string> GetEnvelopeId(string accountId, string formId, string formInstanceId)
    {
        var docuSignClient = _docuSignClientsFactory.BuildDocuSignWebFormsApiClient();
        var formManagementApi = new FormInstanceManagementApi(docuSignClient);
        var instances = await formManagementApi.ListInstancesAsync(accountId, formId);
        var formInstance = instances.Items.SingleOrDefault(i => i.Id == formInstanceId);
        var envelopeId = formInstance.Envelopes.FirstOrDefault().Id;

        return envelopeId;
    }

    private async Task<Envelope> GetEnvelope(string accountId, string envelopeId, EnvelopeCurrentModel envelopeCurrentModel, bool isFocusedView)
    {
        var docuSignClient = _docuSignClientsFactory.BuildDocuSignBaseClient();
        var envelopesApi = new EnvelopesApi(docuSignClient);
        var envelope = await envelopesApi.GetEnvelopeAsync(accountId.ToString(), envelopeId);
        var documents = await envelopesApi.ListDocumentsAsync(accountId.ToString(), envelopeId);
        var recipients = await envelopesApi.ListRecipientsAsync(accountId.ToString(), envelopeId);
        envelope.EnvelopeDocuments = documents.EnvelopeDocuments;
        envelope.Recipients = recipients;

        if(!isFocusedView)
        {
            await SetCurrentEnvelope(accountId, envelope, envelopesApi, envelopeCurrentModel);
        }

        return envelope;
    }

    private async Task SetCurrentEnvelope(string accountId, Envelope envelope, EnvelopesApi envelopesApi, EnvelopeCurrentModel envelopeCurrentModel)
    {
        if (envelopeCurrentModel != null)
        {
            envelopeCurrentModel.Status = envelope.Status;
        }
        else
        {
            envelopeCurrentModel = CreateBaseEnvelopeCurrentModel(envelope);
            var signers = envelope.Recipients.Signers;
            var uris = new List<string>();
            foreach (var signer in signers)
            {
                var request = BuildRecipientViewRequest(signer);
                var uri = await envelopesApi.CreateRecipientViewAsync(accountId.ToString(), envelope.EnvelopeId, request);
                uris.Add(uri.Url);
            }

            envelopeCurrentModel.RedirectUris = uris;
        }

        _envelopeStatusRepository.SetCurrentEnvelope(_accountRepository.UniqueSessionId, envelopeCurrentModel);
    }

    private static EnvelopeCurrentModel CreateBaseEnvelopeCurrentModel(Envelope envelope)
    {
        return new EnvelopeCurrentModel
        {
            EnvelopeId = envelope.EnvelopeId,
            Status = envelope.Status
        };
    }

    private RecipientViewRequest BuildRecipientViewRequest(Signer signer)
    {
        var request = new RecipientViewRequest
        {
            AuthenticationMethod = "email",
            UserName = signer.Name,
            Email = signer.Email,
            ClientUserId = signer.ClientUserId,
            ReturnUrl = _accountRepository.RedirectUri,
            PingFrequency = "60",
            PingUrl = _accountRepository.RedirectUri,
            FrameAncestors = new List<string> { _accountRepository.RedirectUri }
        };
        return request;
    }

    private void AddOrReplaceStatus(string sessionId, EnvelopeStatusModel status)
        => _envelopeStatusRepository.AddOrReplaceStatus(sessionId, status);
}