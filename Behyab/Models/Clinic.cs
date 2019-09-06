using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Behyab.Models
{
    public class Clinic
    {
        [Required]
        [Display(Name ="شماره")]
        public int Id { get; set; }
        [Required(ErrorMessage ="لطفا نام درمانگاه را وارد کنید.")]
        [Display(Name = "نام")]
        public string Name { get; set; }
    }
}