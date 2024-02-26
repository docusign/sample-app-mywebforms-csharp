using DocuSign.MyWebForms.Infrustructure.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DocuSign.MyWebForms.Services.Form;
using DocuSign.MyWebForms.Models.Form;
using System.Threading.Tasks;
using DocuSign.MyWebForms.Services.Statuses;
using System;
using DocuSign.MyWebForms.Models.Statuses;

namespace DocuSign.MyWebForms.Controllers;

[Route("api/form")]
[ApiController]
public class FormController : ControllerBase
{
    private readonly IStatusService _statusService;
    private readonly IFormService _formService;

    public FormController(
        IFormService formService,
        IStatusService statusService)
    {
        _formService = formService;
        _statusService = statusService;
    }

    [Authorize]
    [HttpGet]
    [Route("embedform")]
    public ActionResult<EmbedWebFormOutput> EmbedForm(LoanType loanType)
    {
        try
        {
            var result = _formService.EmbedForm(loanType);
            return Ok(result);
        }
        catch (ApplicationApiException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize]
    [HttpPost]
    [Route("update/status")]
    public async Task<ActionResult<EnvelopeCurrentModel>> UpdateEnvelopeStatus([FromBody] CompleteSigningInput input)
    {
        try
        {
            var result = await _statusService.AddOrReplaceStatus(input);

            return Ok(result);
        }
        catch (ApplicationApiException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}