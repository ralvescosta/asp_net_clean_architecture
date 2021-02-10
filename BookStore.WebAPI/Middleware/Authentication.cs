using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace BookStore.WebAPI.Middleware
{
    public class AuthenticationOptions : AuthenticationSchemeOptions
    {
    }

    public class AuthenticationHandler : AuthenticationHandler<AuthenticationOptions>
    {
        public AuthenticationHandler(
            IOptionsMonitor<AuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
            
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {

            if (!Request.Headers.ContainsKey("Authorization"))
                return Task.FromResult(AuthenticateResult.Fail("Unauthorized"));

            string authorizationHeader = Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(authorizationHeader))
            {
                return Task.FromResult(AuthenticateResult.NoResult());
            }

            if (!authorizationHeader.StartsWith("bearer", StringComparison.OrdinalIgnoreCase))
            {
                return Task.FromResult(AuthenticateResult.Fail("Unauthorized"));
            }

            string token = authorizationHeader.Substring("bearer".Length).Trim();

            if (string.IsNullOrEmpty(token))
            {
                return Task.FromResult(AuthenticateResult.Fail("Unauthorized"));
            }

            try
            {
                return Task.FromResult(validateToken(token));
            }
            catch (Exception ex)
            {
                return Task.FromResult(AuthenticateResult.Fail(ex.Message));
            }
        }

        private AuthenticateResult validateToken(string token)
        {
            
            
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, ""),
                };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new System.Security.Principal.GenericPrincipal(identity, null);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return AuthenticateResult.Success(ticket);
        }
    }
}