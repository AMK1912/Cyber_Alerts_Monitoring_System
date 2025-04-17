using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cyber_Alerts_Monitoring_System.Models
{
    public class NodalOfficer
    {
        [Key]
        [Column(TypeName = "varchar(7)")]
        public string sail_pno { get; set; }

        [Required]
        [StringLength(150)]
        public string name { get; set; }

        [StringLength(200)]
        public string centre { get; set; }

        public int unit_cd { get; set; }

        public int mobile_no { get; set; }

        [StringLength(100)]
        [EmailAddress]
        public string email_id { get; set; }

        public DateTime start_date { get; set; }

        public DateTime? end_date { get; set; }

    }
}
