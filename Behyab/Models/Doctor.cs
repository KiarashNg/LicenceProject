using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Behyab.Models
{
    public class Doctor
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "لطفا نام کاربری را وارد کنید.")]
        [Display(Name ="نام کاربری")]
        [MaxLength(100)]
        public string Username { get; set; }

        [Required(ErrorMessage = "لطفا رمز عبور را وارد کنید.")]
        [Display(Name = "رمز عبور")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "لطفا نام خود را وارد کنید.")]
        [Display(Name ="نام")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "لطفا نام خانوادگی را وارد کنید.")]
        [Display(Name = "نام خانوادگی")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "لطفا تخصص را وارد کنید.")]
        [Display(Name = "تخصص")]
        public int? ExpertyId { get; set; }
        public virtual Experty Experty { get; set; }

        [Required(ErrorMessage = "لطفا شماره نظام پزشکی را وارد کنید.")]
        [Display(Name = "شماره نظام ‍‍‍‍‍‍پزشکی")]
        public string ExpCode { get; set; }

        [Required(ErrorMessage = "لطفا نام درمانگاه را وارد کنید.")]
        [Display(Name = "نام درمانگاه")]
        public int? ClinicId { get; set; }
        public virtual Clinic Clinic { get; set; }

        [Display(Name ="روز هفته")]
        public int? WeekId { get; set; }
        public virtual Week Week { get; set; }
    }
}