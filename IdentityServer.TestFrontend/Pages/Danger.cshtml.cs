using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IdentityServer.TestFrontend.Pages
{
    [Authorize]
    public class DangerModel : PageModel
    {
        private readonly ILogger<DangerModel> _logger;

        public DangerModel(ILogger<DangerModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}