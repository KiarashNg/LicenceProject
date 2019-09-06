using Behyab.DAL;
using Behyab.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Behyab.Controllers
{
    [Authorize]
    public class ReservationController : Controller
    {
        private readonly BehyabDB1Context db;
        public ReservationController()
        {
            db = new BehyabDB1Context();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.RemoveAll();

            return RedirectToAction("Index", "Home");
        }
        
        public ActionResult Calendar()
        {
            var list = new List<Doctor>();
            list = db.Doctors.ToList();

            return View(list);
        }   
       
        public ActionResult FollowTime()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FollowTime(Reserve reserve)
        {            
            //در این قسمت میخواهیم پیگیری نوبت را انجام دهیم
            //به این صورت که با ارسال کد ملی از طرف ویو که کاربر وارد میکند چک میشود که همچین نوبتی در جدول رزرو وجود دارد یا خیر
            //اگر وجود داشت نمایش داده میشود در غیر این صورت ارور

            var entity = db.Reserves.Where(x => x.UserCode.Equals(reserve.UserCode)).FirstOrDefault();

            if(entity == null)
            {
                ViewBag.error = "برای این کد ملی نوبتی وجود ندارد.";
                return View();
            }

            return View(entity);
        }
        
        public ActionResult Cancel()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Cancel(Reserve reserve)
        {
            var entity = db.Reserves.Where(x => x.UserCode.Equals(reserve.UserCode)).FirstOrDefault();

            if (entity == null)
            {
                ViewBag.error = "برای این کد ملی نوبتی وجود ندارد.";
                return View();
            }

            return View(entity);
        }

        [HttpGet]
        public ActionResult DeleteReserve(int Id)
        {
            //در این قسمت میخواهیم عملیات لغو نوبت را انجام دهیم
            //به این صورت که با گرفتن آیدی از سمت ویو و کاربر
            //سطر مربوطه در دیتابیس را پیدا کرده و فیلد مربوط به رزرو را فالس کرده و همچنین کدملی و نام کاربری را از آن سطر خالی میکنیم

            var entity = db.Reserves.Find(Id);

            if(entity != null)
            {
                entity.Status = false;
                entity.UserCode = null;
                entity.Username = null;
                db.SaveChanges();
                return RedirectToAction("Cancel");
            }

            return View();
            
        }
        
        public ActionResult Call()
        {

            return View();
        }
        [HttpGet]
        public ActionResult Index()
        {
            //در این اکشن چون هم نیاز به عملیات سرچ و هم نشان دادن جدول هست
            //ما در دو قسمت جدا این کار را امجام میدهیم

            var Expert = db.Experts.ToList();
            ViewBag.expert = new SelectList(Expert, "Id", "Name");

            var Clinic = db.Clinics.ToList();
            ViewBag.clinic = new SelectList(Clinic, "Id", "Name");

            ViewBag.doctor = db.Doctors.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.FirstName + " " + x.LastName
            }).ToList();

            var reserves = db.Reserves.ToList();

            var model = new IndexViewModel
            {
                Search = null,          //این قسمت برای سرچ
                Reserves = reserves     //این قسمت هم برای لیست 
            };                          //که هرکدام در پایین تعریف شده اند                                    

            return View(model);
        }
        
        public ActionResult Index([Bind(Prefix = nameof(IndexViewModel.Search))]SearchParameters sp)
        {
            //در این قسمت عملیات سرچ را انجام میدهیم و در نهایت نشان میدهیم

            var Expert = db.Experts.ToList();
            ViewBag.expert = new SelectList(Expert, "Id", "Name");

            var Clinic = db.Clinics.ToList();
            ViewBag.clinic = new SelectList(Clinic, "Id", "Name");

            ViewBag.doctor = db.Doctors.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.FirstName + " " + x.LastName
            }).ToList();

            var query = db.Reserves.Where(x => true);

            if (sp.ExpertyId.HasValue)
            {
                query = query.Where(x => x.ExpertyId == sp.ExpertyId.Value);
            }

            if (sp.ClinicId.HasValue)
            {
                query = query.Where(x => x.ClinicId == sp.ClinicId.Value);
            }

            if (sp.DoctorId.HasValue)
            {
                query = query.Where(x => x.DoctorId == sp.DoctorId.Value);
            }

            var reserves = query.ToList();

            var model = new IndexViewModel
            {
                Search = sp,
                Reserves = reserves
            };


            return View(model);
        }

        
        public class SearchParameters
        {
            //این کلاس برای پارامتر های مربوط به سرچ میباشد

            [Display(Name = "تخصص")]
            public int? ExpertyId { get; set; }
            [Display(Name = "درمانگاه")]
            public int? ClinicId { get; set; }
            [Display(Name = "نام متخصص")]
            public int? DoctorId { get; set; }
        }

        public class IndexViewModel
        {
            //در این قسمت هم از یک شبه ویومدل استفاده میکنیم که عملیات سرچ و لیست کردن را از هم مجزا کند

            public SearchParameters Search { get; set; }

            public List<Reserve> Reserves { get; set; }
        }

        [HttpGet]
        public ActionResult GetReserve(int Id)
        {
            //در این قسمت هم عملیات اخذ نوبت را انجام میدهیم

            var Expert = db.Experts.ToList();
            ViewBag.expert = new SelectList(Expert, "Id", "Name");

            var Clinic = db.Clinics.ToList();
            ViewBag.clinic = new SelectList(Clinic, "Id", "Name");

            ViewBag.doctor = db.Doctors.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.FirstName + " " + x.LastName
            }).ToList();

            var entity = db.Reserves.Find(Id);         //اینجا سطر مورد نظر را پیدامیکنیم
            var username = User.Identity.Name;          //اینجا از کوکی استفاده کرده و یوزرنیم را میگیریم
            var user = db.Users.Where(x => x.Username.Equals(username)).FirstOrDefault();  //اینجا از جدول کاربران سطر مریوط به این کد ملی را استخراج میکنیم
            
            if(entity != null)
            {
                entity.Status = true;           //وضعیت نوبت در جدول را رزرو شده میکنیم
                entity.Username = username;     //یوزرنیم و کدملی مربوط به این فرد را در جدول رزرو قرار میدهیم
                entity.UserCode = user.Code;

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}