using System.Security.Claims;

namespace DocuSign.MyWebForms.Services.Auth;

public interface IAuthenticationService
{
    ClaimsPrincipal AuthenticateFromJwt();

    void ClearOnLogout();

    bool CheckToken();
}