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
                Console.WriteLine("Authentication successful for COUser");
                _httpContextAccessor.HttpContext.Session.SetString("Center", "CO");
                _httpContextAccessor.HttpContext.Session.SetString("EmpCode", emp_code);
                Console.WriteLine("Redirecting to /Index from COUser login"); // Add this line
                return RedirectToPage("/Index");
            }
            else if (emp_code == "PlantUser" && password == "PlantPassword")
            {
                Console.WriteLine("Authentication successful for PlantUser");
                _httpContextAccessor.HttpContext.Session.SetString("Center", "Plant");
                _httpContextAccessor.HttpContext.Session.SetString("EmpCode", emp_code);
                Console.WriteLine("Redirecting to /Index from PlantUser login"); // Add this line
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
