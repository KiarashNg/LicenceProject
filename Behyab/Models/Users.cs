using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Behyab.Models
{
    public class Users
    {
        public Users()
        {
            UserTypeId = 2;
        }
        
        public int Id { get; set; }

        [Required(ErrorMessage ="لطفا نام کاربری را وارد کنید.")]
        [Display(Name ="نام کاربری")]
        public string Username { get; set; }

        [Required(ErrorMessage ="لطفا رمز عبور را وارد کنید.")]
        [Display(Name ="رمز عبور")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage ="لطفا کد ملی را وارد کنید.")]
        [Display(Name ="کد ملی")]
        public string Code { get; set; }

        [Required(ErrorMessage ="لطفا شماره همراه را وارد کنید.")]
        [Display(Name ="شماره همراه")]
        public string Mobile { get; set; }
        [Display(Name ="نوع کاربر")]
        public int UserTypeId { get; set; }
        public virtual UserType UserType { get; set; }
    }      
}