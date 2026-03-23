using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace WebAPI.Security
{
    public class BasicAuthenticationAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var authHeader = context.HttpContext.Request.Headers.Authorization.ToString();

            if (!string.IsNullOrWhiteSpace(authHeader)
                && authHeader.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
            {
                var encodedCredentials = authHeader["Basic ".Length..].Trim();

                try
                {
                    var decodedCredentials = Encoding.UTF8.GetString(Convert.FromBase64String(encodedCredentials));
                    var credentials = decodedCredentials.Split(':', 2);

                    if (credentials.Length == 2
                        && !string.IsNullOrWhiteSpace(credentials[0])
                        && !string.IsNullOrWhiteSpace(credentials[1]))
                    {
                        var apiSecurity = context.HttpContext.RequestServices.GetService<APISecurity>();
                        if (apiSecurity != null && apiSecurity.Validate(credentials[0], credentials[1]))
                        {
                            return;
                        }
                    }
                }
                catch
                {
                }
            }

            context.HttpContext.Response.Headers["WWW-Authenticate"] = "Basic";
            context.Result = new UnauthorizedResult();
        }
    }
}
