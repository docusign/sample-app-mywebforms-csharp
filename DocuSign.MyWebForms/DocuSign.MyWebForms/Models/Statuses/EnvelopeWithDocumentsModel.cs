using DocuSign.eSign.Model;
using System.Collections.Generic;

namespace DocuSign.MyWebForms.Models.Statuses;

public class EnvelopeWithDocumentsModel
{
    public Envelope Envelope { get; set; }

    public List<EnvelopeDocument> Documents { get; set; }
}