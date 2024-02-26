using DocuSign.MyWebForms.Services.Documents;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DocuSign.MyWebForms.Controllers;

[Route("api/document")]
[ApiController]
public class DocumentController : ControllerBase
{
    private readonly IDocumentService _documentService;

    public DocumentController(IDocumentService documentService)
    {
        _documentService = documentService;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Document(string documentId, string envelopeId, string documentName)
    {
        var documentStream = await _documentService.GetDocument(
            envelopeId,
            documentId);

        return File(documentStream, "application/octet-stream", $"{documentName}.pdf");
    }
}