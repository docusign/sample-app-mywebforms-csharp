using DocuSign.MyWebForms.Models.Configuration;
using Microsoft.Extensions.Configuration;

namespace DocuSign.MyWebForms.Services.Configuration.Implementation;

public class AppSettingsConfiguration : IAppConfiguration
{
    public AppSettingsConfiguration(IConfiguration configuration)
    {
        DocuSign = new AppConfigurationDocuSign
        {
            IntegrationKey = configuration["DocuSign:IntegrationKey"],
            SecretKey = configuration["DocuSign:SecretKey"],
            AuthServer = configuration["DocuSign:AuthServer"],
            UserId = configuration["DocuSign:UserId"],
            AccountId = configuration["DocuSign:AccountId"],
            RedirectUri = configuration["DocuSign:RedirectUri"],
            RSAPrivateKeyFile = configuration["DocuSign:RSAPrivateKeyFile"],
            JWTLifeTime = int.Parse(configuration["DocuSign:JWTLifeTime"]),
            WebFormsBasePath = configuration["DocuSign:WebFormsBasePath"],
            PersonalLoanTemplateName = configuration["DocuSign:PersonalLoanTemplateName"],
            AutoLoanTemplateName = configuration["DocuSign:AutoLoanTemplateName"],
            SailboatLoanTemplateName = configuration["DocuSign:SailboatLoanTemplateName"],
        };
    }

    public AppConfigurationDocuSign DocuSign { get; set; }
}