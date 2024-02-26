using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace DocuSign.MyWebForms.Services.Database.Implementation;

public class ClaimsAccountRepository : IAccountRepository
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ClaimsAccountRepository(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string AccountId => FindFirstValue("account_id");

    public string BaseUri => FindFirstValue("base_uri");

    public string AccessToken => FindFirstValue("access_token");

    public string Email => FindFirstValue(ClaimTypes.Email);

    public string AccountName => FindFirstValue("account_name");

    public string WebFormsUri => FindFirstValue("webforms_uri");

    public string RedirectUri => FindFirstValue("redirect_uri");

    public string UniqueSessionId => FindFirstValue("unique_session_id");

    private string FindFirstValue(string key)
        => _httpContextAccessor.HttpContext.User.FindFirstValue(key) ?? string.Empty;
}