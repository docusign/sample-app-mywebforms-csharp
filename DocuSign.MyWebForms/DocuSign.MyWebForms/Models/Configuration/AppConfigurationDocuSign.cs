namespace DocuSign.MyWebForms.Models.Configuration;

public class AppConfigurationDocuSign
{
    public string IntegrationKey { get; set; }

    public string SecretKey { get; set; }

    public string UserId { get; set; }

    public string AccountId { get; set; }

    public string AuthServer { get; set; }

    public string RedirectUri { get; set; }

    public string RSAPrivateKeyFile { get; set; }

    public int JWTLifeTime { get; set; }

    public string WebFormsBasePath { get; set; }

    public string PersonalLoanTemplateName { get; set; }

    public string AutoLoanTemplateName { get; set; }

    public string SailboatLoanTemplateName { get; set; }
}