namespace DocuSign.MyWebForms.Services.Database;

public interface IAccountRepository
{
    string AccessToken { get; }
    string AccountId { get; }
    string BaseUri { get; }
    string WebFormsUri { get; }
    string RedirectUri { get; }
    public string Email { get; }
    public string AccountName { get; }
    public string UniqueSessionId { get; }
}