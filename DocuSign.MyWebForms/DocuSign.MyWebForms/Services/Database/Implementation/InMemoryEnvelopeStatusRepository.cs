using DocuSign.MyWebForms.Models.Statuses;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace DocuSign.MyWebForms.Services.Database.Implementation;

public class InMemoryEnvelopeStatusRepository : IEnvelopeStatusRepository
{
    private readonly IDictionary<string, List<EnvelopeStatusModel>> _sessionIdToEnvelopeStatusesDictionary 
        = new ConcurrentDictionary<string, List<EnvelopeStatusModel>>();

    private readonly IDictionary<string, EnvelopeCurrentModel> _sessionIdToEnvelopeCurrentModelDictionary
        = new ConcurrentDictionary<string, EnvelopeCurrentModel>();

    public List<EnvelopeStatusModel> GetStatuses(string sessionId)
    {
        if (_sessionIdToEnvelopeStatusesDictionary.TryGetValue(sessionId, out List<EnvelopeStatusModel> statuses))
        {
            return statuses;
        }

        return new List<EnvelopeStatusModel>();
    }

    public void AddOrReplaceStatus(string sessionId, EnvelopeStatusModel status)
    {
        if (_sessionIdToEnvelopeStatusesDictionary.TryGetValue(sessionId, out List<EnvelopeStatusModel> statuses))
        {
            if (!statuses.Any(s => s.Id == status.Id))
            {
                statuses.Add(status);
            }
            else
            {
                var index = statuses.FindIndex(s => s.Id == status.Id);
                statuses[index] = status;
            }
        }
        else
        {
            statuses = new List<EnvelopeStatusModel> { status };

            _sessionIdToEnvelopeStatusesDictionary.Add(sessionId, statuses);
        }
    }

    public void SetCurrentEnvelope(string sessionId, EnvelopeCurrentModel current)
    {
        if (_sessionIdToEnvelopeCurrentModelDictionary.TryGetValue(sessionId, out _))
        {
            _sessionIdToEnvelopeCurrentModelDictionary[sessionId] = current;
        }
        else
        {
            _sessionIdToEnvelopeCurrentModelDictionary.Add(sessionId, current);
        }
    }

    public EnvelopeCurrentModel GetCurrentEnvelope(string sessionId)
    {
        if (_sessionIdToEnvelopeCurrentModelDictionary.TryGetValue(sessionId, out EnvelopeCurrentModel current))
        {
            return current;
        }

        return null;
    }

    public void RemoveStatuses(string sessionId)
    {
        _sessionIdToEnvelopeStatusesDictionary.Remove(sessionId);
    }
}