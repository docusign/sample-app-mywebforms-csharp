using DocuSign.eSign.Client;
using DocuSign.MyWebForms.Models.Errors;
using Newtonsoft.Json;
using System;
using System.Net;

namespace DocuSign.MyWebForms.Infrustructure.Exceptions;

public class ApplicationApiException : ApplicationException
{
    public ApiErrorDetails Details { get; private set; }

    private ApplicationApiException(ApiErrorDetails details)
    {
        Details = details;
    }

    public ApplicationApiException(ApiException ex)
    {
        Details = ParseApiException(ex);
    }

    public static ApplicationApiException CreateInvalidAccountException()
    {
        return new ApplicationApiException(ApiErrorDetails.CreateInvalidAccountIdError());
    }

    public static Exception CreateInvalidBaseUriException()
    {
        return new ApplicationApiException(ApiErrorDetails.CreateInvalidBaseUriError());
    }

    private static ApiErrorDetails ParseApiException(ApiException ex)
    {
        return (HttpStatusCode)ex.ErrorCode switch
        {
            HttpStatusCode.InternalServerError => ApiErrorDetails.CreateInvalidBaseUriError(),
            HttpStatusCode.BadRequest => (ApiErrorDetails)JsonConvert.DeserializeObject<ApiErrorDetails>(ex.ErrorContent),
            _ => new ApiErrorDetails(),
        };
    }
}