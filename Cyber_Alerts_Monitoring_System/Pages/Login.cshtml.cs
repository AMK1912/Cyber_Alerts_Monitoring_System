using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace Cyber_Alerts_Monitoring_System.Pages
{
    public class LoginModel : PageModel
    {

        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [BindProperty]
        public string emp_code { get; set; }

        [BindProperty]
        public string password { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(emp_code) || string.IsNullOrEmpty(password))
            {
                ViewData["ErrorMessage"] = "Please Enter both Employee Code and Password";
                return Page();
            }

            if (emp_code == "COUser" && password == "COPassword")
            {
                // Simulate successful login for Corporate Office
                _httpContextAccessor.HttpContext.Session.SetString("Center", "CO");
                return RedirectToPage("/Index");
            }
            else if (emp_code == "PlantUser" && password == "PlantPassword")
            {
                // Simulate successful login for a Plant/Unit
                _httpContextAccessor.HttpContext.Session.SetString("Center", "Plant");
                return RedirectToPage("/Index");
            }
            else
            {
                ViewData["ErrorMessage"] = "Invalid Emp Code or Password";
                return Page();
            }
        }
    }
}
