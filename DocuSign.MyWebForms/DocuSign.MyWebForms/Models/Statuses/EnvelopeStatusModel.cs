using DocuSign.MyWebForms.Models.Documents;
using System;
using System.Collections.Generic;

namespace DocuSign.MyWebForms.Models.Statuses;

public class EnvelopeStatusModel
{
    public string Id { get; set; }

    public string EmailSubject { get; set; }
    public string Status { get; set; }

    public List<string> RecipientSignerNames { get; set; } = new List<string>();

    public DateTime? LastUpdate { get; set; }

    public List<DocumentModel> Documents { get; set; } = new List<DocumentModel>();
}