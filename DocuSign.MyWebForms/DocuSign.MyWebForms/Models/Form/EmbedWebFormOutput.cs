using System;

namespace DocuSign.MyWebForms.Models.Form;

public class EmbedWebFormOutput
{
    public string Id { get; set; }
    public string FormId { get; set; }
    public string InstanceToken { get; set; }
    public string IntegrationKey { get; set; }
    public string Url { get; set; }
}