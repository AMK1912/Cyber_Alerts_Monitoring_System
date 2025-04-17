using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using Cyber_Alerts_Monitoring_System.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;



namespace Cyber_Alerts_Monitoring_System.Pages
{
    public class CyberAlertsCentralModel : PageModel
    {
        [BindProperty]
        public DateTime received_date { get; set; }

        [BindProperty]
        public string sender_details { get; set; }

        [BindProperty]
        public DateTime incident_date { get; set; }

        [BindProperty]
        public DateTime entry_date { get; set; }

        [BindProperty]
        public DateTime? email_date { get; set; }

        [BindProperty]
        public string pertaining_to_unit { get; set; }

        [BindProperty]
        public string affected_sail_ip { get; set; }

        [BindProperty]
        public int? affected_port { get; set; }

        [BindProperty]
        public string malicious_ip { get; set; }

        [BindProperty]
        public string alert_details { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            // This method will be called when the form is submitted
            // Here you would typically:
            // 1. Validate the input data.
            // 2. Create a new CyberAlert object using the bound properties.
            // 3. Save the CyberAlert object to the database.
            // 4. Redirect the user to a success page or display a confirmation.

            // For now, let's just log the received data to the console.
            Console.WriteLine($"Received Date: {received_date}");
            Console.WriteLine($"Sender Details: {sender_details}");
            Console.WriteLine($"Incident Date: {incident_date}");
            Console.WriteLine($"Entry Date: {entry_date}");
            Console.WriteLine($"Email Date: {email_date}");
            Console.WriteLine($"Pertaining To Unit: {pertaining_to_unit}");
            Console.WriteLine($"Affected SAIL IP: {affected_sail_ip}");
            Console.WriteLine($"Affected Port: {affected_port}");
            Console.WriteLine($"Malicious IP: {malicious_ip}");
            Console.WriteLine($"Alert Details: {alert_details}");

            // For now, just redirect back to the same page
            return Page();
        }
    }
}
