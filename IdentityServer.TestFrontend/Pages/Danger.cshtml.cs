using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IdentityServer.TestFrontend.Pages
{
    public class DangeryModel : PageModel
    {
        private readonly ILogger<DangeryModel> _logger;

        public DangeryModel(ILogger<DangeryModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}