using BookStore.Domain.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
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
        private readonly IAuthenticationUseCase authenticateUseCase;
        public AuthenticationHandler(
            IOptionsMonitor<AuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IAuthenticationUseCase authenticateUseCase
            )
            : base(options, logger, encoder, clock)
        {
            this.authenticateUseCase = authenticateUseCase;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Authorization");

            string authorizationHeader = Request.Headers["Authorization"];

            try
            {
                var auth = await authenticateUseCase.Auth(authorizationHeader);
                if (auth == null) return AuthenticateResult.Fail("");

                Request.HttpContext.Items["Auth"] = auth;
                return AuthenticationTicket();
            }
            catch
            {
                return AuthenticateResult.Fail("");
            }
        }

        private AuthenticateResult AuthenticationTicket()
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