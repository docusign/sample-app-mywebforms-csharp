using System.Collections.Generic;

namespace DocuSign.MyWebForms.Models.Statuses
{
    public class EnvelopeCurrentModel
    {
        public string EnvelopeId { get; set; }
        public string Status { get; set; }
        public List<string> RedirectUris { get; set; } = new List<string>();
    }
}
