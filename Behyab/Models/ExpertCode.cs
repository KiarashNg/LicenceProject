using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Behyab.Models
{
    public class ExpertCode
    {
        [Required]
        [Display(Name = "شناسه")]
        public int Id { get; set; }
        [Required(ErrorMessage = "لطفا نام تخصص را وارد کنید.")]
        [Display(Name = "شماره نظام پزشکی")]
        public string Code { get; set; }
    }
}