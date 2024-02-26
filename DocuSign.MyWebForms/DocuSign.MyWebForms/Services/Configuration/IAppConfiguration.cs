using DocuSign.MyWebForms.Models.Configuration;

namespace DocuSign.MyWebForms.Services.Configuration;

public interface IAppConfiguration
{
    AppConfigurationDocuSign DocuSign { get; set; }
}