using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cyber_Alerts_Monitoring_System.Models
{
    public class CyberAlerts
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int alert_id { get; set; }

        public DateTime received_date { get; set; }

        [StringLength(50)]
        public string sender_details { get; set; }

        [StringLength(50)]
        public string received_from_sender { get; set; }

        public DateTime incident_date { get; set; }

        public DateTime entry_date { get; set; }

        public DateTime? email_date { get; set; }

        [StringLength(50)]
        public string pertaining_to_unit { get; set; }

        public string affected_sail_ip { get; set; }

        public int? affected_port { get; set; } // Make nullable as it might not always be present

        public string malicious_ip { get; set; }

        [StringLength(250)]
        public string alert_details { get; set; }

        public DateTime? first_action_taken_date { get; set; }

        [StringLength(250)]
        public string details_of_action { get; set; }

        [StringLength(200)]
        public string remarks { get; set; }

        public DateTime? replied_to_date { get; set; }

        public DateTime? closing_date { get; set; }

        // Fields for tracking as discussed
        public string EnteredBy { get; set; }

        [Column(TypeName = "varchar(7)")]
        [ForeignKey("AssignedToNodalOfficerNavigation")] // For potential relationship with NodalOfficer
        public string AssignedToNodalOfficer { get; set; }

        // Navigation property (optional, for easier access to related NodalOfficer object)
        public NodalOfficer AssignedToNodalOfficerNavigation { get; set; }
    }
}
