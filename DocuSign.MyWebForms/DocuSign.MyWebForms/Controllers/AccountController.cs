using DocuSign.MyWebForms.Infrustructure.Exceptions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using IAuthenticationService = DocuSign.MyWebForms.Services.Auth.IAuthenticationService;

namespace DocuSign.MyWebForms.Controllers;

[Route("api/account")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AccountController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpGet]
    [Route("login")]
    public async Task<IActionResult> Login()
    {
        try
        {
            ClaimsPrincipal principal =
                _authenticationService.AuthenticateFromJwt();

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal);
            HttpContext.User = principal;

        }
        catch (ApplicationApiException ex)
        {
            return BadRequest(ex.Message);
        }

        return NoContent();
    }

    [HttpGet]
    [Route("isauthenticated")]
    public IActionResult IsAuthenticated()
    {
        return Ok(_authenticationService.CheckToken());
    }

    [Authorize]
    [HttpGet]
    [Route("logout")]
    public async Task<IActionResult> Logout()
    {
        _authenticationService.ClearOnLogout();
        HttpContext.Session.Clear();
        await HttpContext.SignOutAsync();

        return Redirect(Url.Content("~/"));
    }
}