using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Behyab.Models
{
    public class Reserve
    {
        public Reserve()
        {
            Status = true;
        }

        public int Id { get; set; }

        [Display(Name = "وضعیت")]
        public bool Status { get; set; }

        [Display(Name = "درمانگاه")]
        public int? ClinicId { get; set; }
        public virtual Clinic Clinic { get; set; }

        [Display(Name = "تخصص")]
        public int? ExpertyId { get; set; }
        public virtual Experty Experty { get; set; }

        [Display(Name = "نام متخصص")]
        public int? DoctorId { get; set; }
        public virtual Doctor Doctor { get; set; }

        [Display(Name = "کد ملی کاربر")]
        public string UserCode { get; set; }

        [Display(Name = "نام کاربری")]
        public string Username { get; set; }

        [Display(Name = "روز و ساعت")]
        public DateTime DateTime { get; set; }
    }
}