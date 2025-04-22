using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace WebApplication1.Pages // Replace YourWebAppName with your actual namespace
{
    public class IndexModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IndexModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string EmpCode { get; set; } // Public property to hold emp_code

        public IActionResult OnGet()
        {
            string center = _httpContextAccessor.HttpContext.Session.GetString("Center");
            EmpCode = _httpContextAccessor.HttpContext.Session.GetString("EmpCode"); // Retrieve emp_code

            if (center == "CO")
            {
                return RedirectToPage("/CyberAlertsCentral");
            }
            else if (center == "Plant")
            {
                return RedirectToPage("/CyberAlertsLocal");
            }
            else
            {
                return RedirectToPage("/Login");
            }
        }
    }
}
