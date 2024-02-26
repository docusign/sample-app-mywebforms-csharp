using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using DocuSign.eSign.Client;
using DocuSign.eSign.Client.Auth;
using DocuSign.MyWebForms.Infrustructure.Exceptions;
using DocuSign.MyWebForms.Services.Clients;
using DocuSign.MyWebForms.Services.Configuration;
using DocuSign.MyWebForms.Services.Statuses;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace DocuSign.MyWebForms.Services.Auth.Implementation;

public class AuthenticationService : IAuthenticationService
{
    private readonly IDocuSignClientsFactory _docuSignClientsFactory;
    private readonly IAppConfiguration _appConfiguration;
    private readonly IStatusService _statusService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthenticationService(
        IDocuSignClientsFactory docuSignClientsFactory,
        IAppConfiguration appConfiguration,
        IStatusService statusService,
        IHttpContextAccessor httpContextAccessor)
    {
        _docuSignClientsFactory = docuSignClientsFactory;
        _appConfiguration = appConfiguration;
        _statusService = statusService;
        _httpContextAccessor = httpContextAccessor;
    }

    public ClaimsPrincipal AuthenticateFromJwt()
    {
        var apiClient = _docuSignClientsFactory.BuildDocuSignAuthClient(_appConfiguration.DocuSign.AuthServer);
        OAuth.OAuthToken authToken = null;
        try
        {
            authToken = apiClient.RequestJWTUserToken(
                _appConfiguration.DocuSign.IntegrationKey,
                _appConfiguration.DocuSign.UserId,
                _appConfiguration.DocuSign.AuthServer,
                File.ReadAllBytes(_appConfiguration.DocuSign.RSAPrivateKeyFile),
                _appConfiguration.DocuSign.JWTLifeTime,
                new List<string> { "signature", "impersonation", "webforms_read", "webforms_instance_write", "webforms_instance_read" });
        }
        catch (ApiException ex)
        {
            throw new ApplicationApiException(ex);
        }

        var userInfo = apiClient.GetUserInfo(authToken.access_token);
        var account = userInfo.Accounts.SingleOrDefault(x => x.AccountId == _appConfiguration.DocuSign.AccountId);

        if (account != null)
        {
            var claims = new List<Claim>
            {
                new("access_token", authToken.access_token),
                new(ClaimTypes.NameIdentifier, userInfo.Sub),
                new(ClaimTypes.Name, userInfo.Name),
                new(ClaimTypes.Email, userInfo.Email),
                new("base_uri", account.BaseUri),
                new("account_name", account.AccountName),
                new("account_id", account.AccountId),
                new("webforms_uri", _appConfiguration.DocuSign.WebFormsBasePath),
                new("redirect_uri", _appConfiguration.DocuSign.RedirectUri),
                new("unique_session_id", Guid.NewGuid().ToString()),
                new("expires_at", DateTime.UtcNow.AddHours(_appConfiguration.DocuSign.JWTLifeTime).ToString())
            };

            var claimsIdentity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            return new ClaimsPrincipal(claimsIdentity);
        }

        throw ApplicationApiException.CreateInvalidAccountException();
    }

    public void ClearOnLogout()
    {
        _statusService.ClearStatuses();
    }

    public bool CheckToken()
        => _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated && CheckExpirationDate();

    private bool CheckExpirationDate()
        => DateTime.UtcNow.Subtract(TimeSpan.FromHours(GetJWTLifeTime())) <= GetExpiresAt();

    private int GetJWTLifeTime()
        => _appConfiguration.DocuSign.JWTLifeTime;

    private DateTime GetExpiresAt()
    {
        var expiresAt = _httpContextAccessor.HttpContext.User.FindFirstValue("expires_at");
        return expiresAt != null ? DateTime.Parse(expiresAt) : DateTime.MinValue;
    }        
}