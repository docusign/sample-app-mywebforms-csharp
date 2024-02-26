using DocuSign.MyWebForms.Models.Form;

namespace DocuSign.MyWebForms.Services.Form;

public interface IFormService
{
    EmbedWebFormOutput EmbedForm(LoanType loanType);
}