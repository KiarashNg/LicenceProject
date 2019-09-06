using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Behyab.Models
{
    public class Week
    {
        [Required]
        [Display(Name = "شماره")]
        public int Id { get; set; }
        [Required(ErrorMessage = "لطفا نام روز هفته را وارد کنید.")]
        [Display(Name = "روز هفته")]
        public string Day { get; set; }
    }
}