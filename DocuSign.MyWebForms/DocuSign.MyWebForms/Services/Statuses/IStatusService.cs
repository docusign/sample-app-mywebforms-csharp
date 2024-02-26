using DocuSign.MyWebForms.Models.Form;
using DocuSign.MyWebForms.Models.Statuses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DocuSign.MyWebForms.Services.Statuses;

public interface IStatusService
{
    List<EnvelopeStatusModel> GetStatuses();

    Task<EnvelopeCurrentModel> AddOrReplaceStatus(CompleteSigningInput input);

    void ClearStatuses();
}