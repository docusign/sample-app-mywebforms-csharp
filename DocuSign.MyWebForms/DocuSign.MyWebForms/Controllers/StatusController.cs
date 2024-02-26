using DocuSign.MyWebForms.Models.Statuses;
using DocuSign.MyWebForms.Services.Statuses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DocuSign.MyWebForms.Controllers;

[Route("api/status")]
[ApiController]
public class StatusController : ControllerBase
{
    private readonly IStatusService _statusService;

    public StatusController(IStatusService statusService)
    {
        _statusService = statusService;
    }

    [Authorize]
    [HttpGet]
    [Route("all")]
    public ActionResult<List<EnvelopeStatusModel>> GetStatuses()
    {
        var statuses = _statusService.GetStatuses();

        return Ok(statuses);
    }
}