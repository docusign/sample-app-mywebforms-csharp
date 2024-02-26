using DocuSign.eSign.Model;
using DocuSign.MyWebForms.Models.Documents;
using DocuSign.MyWebForms.Models.Statuses;
using DocuSign.MyWebForms.Services.Documents;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DocuSign.MyWebForms.Services.Statuses.Implementation;

public class EnvelopeStatusModelConverter : IEnvelopeStatusModelConverter
{
    private readonly IDocumentModelConverter _documentModelConverter;

    public EnvelopeStatusModelConverter(IDocumentModelConverter documentModelConverter)
    {
        _documentModelConverter = documentModelConverter;
    }

    public EnvelopeStatusModel Convert(Envelope envelope)
        => new()
        {
            Id = envelope.EnvelopeId,
            EmailSubject = envelope.EmailSubject,
            Status = envelope.Status,
            RecipientSignerNames = envelope.Recipients?.Signers?.Select(s => s.Name).ToList() 
                ?? new List<string>(),
            LastUpdate = envelope.StatusChangedDateTime is null 
                ? null 
                : DateTime.TryParse(envelope.StatusChangedDateTime, out DateTime lastUpdate)
                    ? lastUpdate
                    : null,
            Documents = envelope.EnvelopeDocuments?.Select(_documentModelConverter.Convert).ToList() 
                ?? new List<DocumentModel>()
        };
}