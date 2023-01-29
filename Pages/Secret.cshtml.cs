using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HelloGithubOAuth.Pages
{
    [Authorize]
    public class SecretModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
