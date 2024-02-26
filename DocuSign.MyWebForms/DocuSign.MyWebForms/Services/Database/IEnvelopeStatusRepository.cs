using DocuSign.MyWebForms.Models.Statuses;
using System.Collections.Generic;

namespace DocuSign.MyWebForms.Services.Database;

public interface IEnvelopeStatusRepository
{
    List<EnvelopeStatusModel> GetStatuses(string sessionId);

    void AddOrReplaceStatus(string sessionId, EnvelopeStatusModel status);

    void RemoveStatuses(string sessionId);

    void SetCurrentEnvelope(string sessionId, EnvelopeCurrentModel current);
    EnvelopeCurrentModel GetCurrentEnvelope(string sessionId);

}