using BookStore.Domain.Enums;
using BookStore.Domain.Interfaces;
using BookStore.WebAPI.Attributes;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
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
            var metaData = Context.GetEndpoint().Metadata.GetMetadata<PermissionAttribute>();
            try
            {
                var permissionRequired = GetPermissionRequiredEnumInstance(metaData);

                var auth = await authenticateUseCase.Auth(authorizationHeader, permissionRequired);
                if (auth.IsLeft()) 
                    return AuthenticateResult.Fail("");

                Request.HttpContext.Items["Auth"] = auth.GetRight();
                return AuthenticationTicket();
            }
            catch
            {
                return AuthenticateResult.Fail("");
            }
        }
        #region privateMethods
        private static Permissions GetPermissionRequiredEnumInstance(PermissionAttribute attribuite) 
        {
            if (attribuite == null) return Permissions.Unauthorized;

            var type = attribuite.GetType().Name;
            var permission = Regex.Split(type, @"(?<!^)(?=[A-Z])")[0];
            Enum.TryParse<Permissions>(permission, false, out var enumResult);
            return enumResult;
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
        #endregion
    }
}