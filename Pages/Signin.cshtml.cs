// Based on code by Khalid Abuhakmeh:
// https://khalidabuhakmeh.com/github-openid-auth-aspnet-core-apps

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HelloGithubOAuth.Pages
{
    public class SigninModel : PageModel
    {
        public IEnumerable<AuthenticationScheme>? Schemes { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? ReturnUrl { get; set; }
        public void OnGet()
        {
            Schemes = GetExternalProvidersAsync(HttpContext);
        }

        public IActionResult OnPost([FromForm] string provider)
        {
            if (string.IsNullOrWhiteSpace(provider))
            {
                return BadRequest();
            }

            return IsProviderSupportedAsync(HttpContext, provider) is false
                ? BadRequest()
                : Challenge(new AuthenticationProperties
                {
                    RedirectUri = Url.IsLocalUrl(ReturnUrl) ? ReturnUrl : "/"
                }, provider);
        }

        private static AuthenticationScheme[] GetExternalProvidersAsync(HttpContext context)
        {
            var schemes = context.RequestServices.GetRequiredService<IAuthenticationSchemeProvider>();
            return (schemes.GetAllSchemesAsync())
                .Result
                .Where(scheme => !string.IsNullOrEmpty(scheme.DisplayName))
                .ToArray();
        }

        private static bool IsProviderSupportedAsync(HttpContext context, string provider) =>
            GetExternalProvidersAsync(context)
            .Any(scheme => string.Equals(scheme.Name, provider, StringComparison.OrdinalIgnoreCase));
    }
}
