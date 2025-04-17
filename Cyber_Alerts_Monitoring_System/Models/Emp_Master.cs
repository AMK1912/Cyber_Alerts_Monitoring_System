using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cyber_Alerts_Monitoring_System.Models
{
    public class Emp_Master
    {
        [Key]
        [Column(TypeName = "varchar(7)")]
        public string emp_code { get; set; }

        [Required]
        public string password { get; set; }
    }
}
