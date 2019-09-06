using Behyab.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Behyab.DAL
{
    //هدف از ایجاد این کلاس ارتباط بین مدل و دیتابیس میباشد
    public class BehyabDB1Context : DbContext
    {
        public BehyabDB1Context()
            :base("name=BehyabDB1Context")
        {

        }
        //در اینجا میگوییم که این مدل ها را در دیتابس قرار بده
        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Experty> Experts { get; set; }
        public DbSet<Reserve> Reserves { get; set; }
        public DbSet<ExpertCode> ExpertCodes { get; set; }
        public DbSet<Week> Weeks { get; set; }
    }
}