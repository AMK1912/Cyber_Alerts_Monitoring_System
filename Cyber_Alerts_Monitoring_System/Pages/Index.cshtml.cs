using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using System;

namespace YourWebAppName.Pages // Replace with your actual namespace
{
    public class IndexModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IndexModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string EmpCode { get; set; }

        public IActionResult OnGet()
        {
            string center = _httpContextAccessor.HttpContext.Session.GetString("Center");
            EmpCode = _httpContextAccessor.HttpContext.Session.GetString("EmpCode");

            Console.WriteLine($"Index Page - Center value from session: {center}"); // Add this line
            Console.WriteLine($"Index Page - EmpCode value from session: {EmpCode}"); // Add this line

            if (center == "CO")
            {
                Console.WriteLine("Index Page - Redirecting to /CyberAlertsCentral"); // Add this line
                return RedirectToPage("/CyberAlertsCentral");
            }
            else if (center == "Plant")
            {
                Console.WriteLine("Index Page - Redirecting to /CyberAlertsLocal"); // Add this line
                return RedirectToPage("/CyberAlertsLocal");
            }
            else
            {
                Console.WriteLine("Index Page - Center not found, redirecting to /Login"); // Add this line
                return RedirectToPage("/Login");
            }
        }
    }
}
