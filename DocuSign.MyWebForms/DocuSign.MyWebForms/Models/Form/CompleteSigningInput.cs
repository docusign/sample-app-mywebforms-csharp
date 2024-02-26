namespace DocuSign.MyWebForms.Models.Form;

public class CompleteSigningInput
{
    public string Id { get; set; }
    public string FormId { get; set; }
    public bool ExistingEnvelope { get; set; }
    public bool IsFocusedView { get; set; }
}