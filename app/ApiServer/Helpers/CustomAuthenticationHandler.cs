using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Server.Classes;
using System.Net;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace Server.Helpers;

public class BasicAuthenticationOptions : AuthenticationSchemeOptions
{
}
public class CustomAuthenticationHandler : AuthenticationHandler<BasicAuthenticationOptions>
{
    private readonly IHostEnvironment env;
    private readonly ConfigApp appConfig;

    public CustomAuthenticationHandler(
        IOptionsMonitor<BasicAuthenticationOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ConfigApp _appConfig,
        IHostEnvironment _env,
        ISystemClock clock) : base(options, logger, encoder, clock)
    {
        appConfig = _appConfig;
        env = _env;
    }

    protected async override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        try
        {
            StringValues values;
            var token = Request.Headers["X-API-KEY"];
            if (string.IsNullOrEmpty(token)) throw new Exception("You are not an authorized user, review your credentials");

            if (token.ToString() != appConfig.Application.ApiKey) throw new Exception("You are not an authorized user, review your credentials");

            var userClaims = new List<Claim>
            {
                new Claim("User", "Neitek User Challenge", ClaimValueTypes.Email),
                new Claim("Token", token, ClaimValueTypes.String)
            };
            var userIdentity = new ClaimsIdentity(userClaims, "Tokens");
            var principal = new ClaimsPrincipal(userIdentity);
            var ticket = new AuthenticationTicket(principal, new AuthenticationProperties { ExpiresUtc = DateTime.UtcNow.AddMinutes(10).ToUniversalTime() }, this.Scheme.Name);
            return AuthenticateResult.Success(ticket);
        }
        catch (Exception ex)
        {
            Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            Response.ContentType = "text/html";
            await Response.WriteAsync(ex.Message);
            return AuthenticateResult.Fail(ex);
        }
    }
}
