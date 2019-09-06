using Behyab.DAL;
using Behyab.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Behyab.Controllers
{
    [Authorize]     //برای اینکه فردی که احراز هویتش تایید نشده نتواند به این قسمت دسترسی داشته باشد از این عبارت استفاده میکنیم
    public class AdminController : Controller  
    {
        private readonly BehyabDB1Context db;  //از کلاس دیبی کانتکست یک شی جدید ایجاد میکنیم برای برقراری ارتباط با دیتابیس
        public AdminController()
        {
            db = new BehyabDB1Context();
        }

        public ActionResult Logout()
        {   
            //در این اکشن ما عملیات خروج کاربر را انجام میدهیم به این صورت که از کوکی یوزرنیم و پسورد را پاک کرده و سشن را خاتمه میدهیم

            FormsAuthentication.SignOut();
            Session.Clear();
            Session.RemoveAll();

            return RedirectToAction("Index", "Home");
        }
        public ActionResult Index()
        {
            //در این اکشن میخواهیم لیست پزشکان را نمایش دهیم

            var list = new List<Doctor>(); //یک لیست جدید از مدل مربوط به پزشک تعریف میکنیم
            list = db.Doctors.ToList();     //این لیست را برابر با جدول دکتر که در دیتابیس هست قرار میدهیم
            return View(list);              //لیست را نشان میدهیم
        }
        [HttpGet]
        public ActionResult CreateDoc()
        {
            //در این فسمت میخواهیم پزشک جدید ایجاد کنیم

            var Expert = db.Experts.ToList();                       //برای سهولت کار برای کاربر از دراپ دان لیست استفاده میکنیم
            ViewBag.expert = new SelectList(Expert, "Id", "Name");  //به این صورت که از جدول مربوطه در دیتابیس شماره و نام هر مدلی که نیاز داریم را گرفته و در ویوبگ قرار میدهیم 
                                                                    //و ویوبگ را در ویو فراخوانی میکنیم
            var Clinic = db.Clinics.ToList();
            ViewBag.clinic = new SelectList(Clinic, "Id", "Name");

            var Week = db.Weeks.ToList();
            ViewBag.week = new SelectList(Week, "Id", "Day");

            return View();
        }

        [HttpPost]
        public ActionResult CreateDoc(Doctor doctor)  //یک شی جدید از مدل پزشک ایجاد میکنیم
        {
            //حال در این قسمت که از متود پست استفاده میکنیم میخواهیم چیزی را که از سمت ویو فرستاده شده را بگیریم و در دیتابیس ذخیره کنیم

            if(!ModelState.IsValid)
            {

            }
           
            db.Doctors.Add(doctor);         //شی گرفته شده را در جدول اضافه میکنیم
            db.SaveChanges();               //و تغییرات را ذخیره میکنیم
            return RedirectToAction("Index");       //و به صفحه اول برمیگردیم
        }
        [HttpGet]
        public ActionResult EditDoc( int Id)
        {
            //در این قسمت که مربوط به ویرایش پزشک میباشد ابتدا میخواهیم پزشک مورد نظر که قرار است ویرایش شود را نشان دهیم

            var entity = db.Doctors.Find(Id);       //به این صورت به وسیله شماره ای که از سمت ویو فرستاده شده در جدول جستجو کرده و مورد موردنظر را پیدا میکنیم و آن را نشان میدهیم
                                                               
            if(entity == null)
            {
                ViewBag.notfound = "دکتری با این مشخصات وجود ندارد.";
                return RedirectToAction("Index");
            }

            var Expert = db.Experts.ToList();
            ViewBag.expert = new SelectList(Expert, "Id", "Name");

            var Clinic = db.Clinics.ToList();
            ViewBag.clinic = new SelectList(Clinic, "Id", "Name");

            var Week = db.Weeks.ToList();
            ViewBag.week = new SelectList(Week, "Id", "Day");

            return View(entity);
        }

        [HttpPost]
        public ActionResult EditDoc(Doctor doctor)
        {
            //حال در این قسمت میخواهیم تغییرات اعمال شده پس از ویرایش را ارسال کنیم و در دیتابیس ذخیره نماییم

            var Expert = db.Experts.ToList();
            ViewBag.expert = new SelectList(Expert, "Id", "Name");

            var Clinic = db.Clinics.ToList();
            ViewBag.clinic = new SelectList(Clinic, "Id", "Name");

            var Week = db.Weeks.ToList();
            ViewBag.week = new SelectList(Week, "Id", "Day");

            db.Entry(doctor).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteDoc(int Id)
        {
            //در این قسمت میخواهیم عملیات حذف را انجام دهیم
            //به این صورت که با استفاده از شماره فرستاده شده از سمت ویو میفهمیم به کدام سطر از جدول اشاره دارد
            //و پس از پیدا کردن آن سطر طبق دستورات زیر آن سطر را حذف میکنیم

            var entity = db.Doctors.FirstOrDefault(x => x.Id == Id);

            if (entity == null)
            {
                ViewBag.notfound = "دکتری با این مشخصات وجود ندارد.";
                return RedirectToAction("Index");
            }

            db.Doctors.Remove(entity);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //بقیه اکشن ها مانند همین ها هستند

        public ActionResult Clinic()
        {
            var list = new List<Clinic>();
            list = db.Clinics.ToList();

            return View(list);
        }
        
        public ActionResult CreateClinic()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateClinic(Clinic clinic)
        {
            if(db.Clinics.Any(x => x.Name == clinic.Name))
            {
                ViewBag.same = "نام درمانگاه تکراری است.";
                return View(clinic);
            }

            db.Clinics.Add(clinic);
            db.SaveChanges();
            return RedirectToAction("Clinic");
        }
        [HttpGet]
        public ActionResult EditClinic(int Id)
        {
            var entity = db.Clinics.Find(Id);
            
            if(entity == null)
            {
                ViewBag.notfound = "درمانگاهی با این مشخصات وجود ندارد.";
                return RedirectToAction("Clinic");
            }

            return View(entity);
        }

        [HttpPost]
        public ActionResult EditClinic(Clinic clinic)
        {
            if (db.Clinics.Any(x => x.Name == clinic.Name))
            {
                ViewBag.same = "نام درمانگاه تکراری است.";
                return View(clinic);
            }

            db.Entry(clinic).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Clinic");
        }

        [HttpGet]
        public ActionResult DeleteClinic(int Id)
        {
            var entity = db.Clinics.FirstOrDefault(x => x.Id == Id);

            if (entity == null)
            {
                ViewBag.notfound = "درمانگاهی با این مشخصات وجود ندارد.";
                return RedirectToAction("Clinic");
            }

            db.Clinics.Remove(entity);
            db.SaveChanges();
            return RedirectToAction("Clinic");
        }

        public ActionResult ExpertCode()
        {
            var list = new List<ExpertCode>();
            list = db.ExpertCodes.ToList();
            return View(list);
        }

        public ActionResult CreateExpCode()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateExpCode(ExpertCode expert)
        {
            if(db.ExpertCodes.Any(x => x.Code == expert.Code))
            {
                ViewBag.same = "این شماره تکراری است.";
                return View(expert);
            }

            db.ExpertCodes.Add(expert);
            db.SaveChanges();
            return RedirectToAction("ExpertCode");
        }

        [HttpGet]
        public ActionResult EditExpCode(int Id)
        {
            var entity = db.ExpertCodes.Find(Id);

            if (entity == null)
            {
                ViewBag.notfound = "شماره ای با این مشخصات وجود ندارد.";
                return RedirectToAction("ExpertCode");
            }

            return View(entity);
        }

        [HttpPost]
        public ActionResult EditExpCode(ExpertCode expert)
        {
            if (db.ExpertCodes.Any(x => x.Code == expert.Code))
            {
                ViewBag.same = "این شماره تکراری است.";
                return View(expert);
            }

            db.Entry(expert).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("ExpertCode");
        }

        [HttpGet]
        public ActionResult DeleteExpCode(int Id)
        {
            var entity = db.ExpertCodes.FirstOrDefault(x => x.Id == Id);

            db.ExpertCodes.Remove(entity);
            db.SaveChanges();
            return RedirectToAction("ExpertCode");
        }

        public ActionResult Experts()
        {
            var list = new List<Experty>();
            list = db.Experts.ToList();
            return View(list);
        }

        public ActionResult CreateExpert()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateExpert(Experty experty)
        {
            if(db.Experts.Any(x => x.Name == experty.Name))
            {
                ViewBag.same = "این تخصص قبلا ثبت شده است.";
                return View(experty);
            }

            db.Experts.Add(experty);
            db.SaveChanges();
            return RedirectToAction("Experts");
        }

        [HttpGet]
        public ActionResult EditExpert(int Id)
        {
            var entity = db.Experts.Find(Id);
            return View(entity);
        }

        [HttpPost]
        public ActionResult EditExpert(Experty experty)
        {
            if(db.Experts.Any(x => x.Name == experty.Name))
            {
                ViewBag.same = "این تخصص قبلا ثبت شده است.";
                return View(experty);
            }

            db.Entry(experty).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Experts");
        }

        [HttpGet]
        public ActionResult DeleteExpert(int Id)
        {
            var entity = db.Experts.FirstOrDefault(x => x.Id == Id);

            db.Experts.Remove(entity);
            db.SaveChanges();
            return RedirectToAction("Experts");
        }

        public ActionResult Reserves()
        {
            var list = new List<Reserve>();
            list = db.Reserves.ToList();
            return View(list);
        }
        [HttpGet]
        public ActionResult CreateReserve()
        {
            var Expert = db.Experts.ToList();
            ViewBag.expert = new SelectList(Expert, "Id", "Name");

            var Clinic = db.Clinics.ToList();
            ViewBag.clinic = new SelectList(Clinic, "Id", "Name");

            ViewBag.doctor = db.Doctors.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.FirstName + " " + x.LastName
            }).ToList();

            var usercode = db.Users.ToList();
            ViewBag.usercode = new SelectList(usercode, "Id", "Code");

            return View();
        }

        [HttpPost]
        public ActionResult CreateReserve(Reserve reserve)
        {
            if(db.Reserves.Any(x => x.UserCode == reserve.UserCode))
            {
                ViewBag.same = "برای این کد ملی قبلا نوبت رزرو شده است.";
                return View(reserve);
            }

            db.Reserves.Add(reserve);
            db.SaveChanges();
            return RedirectToAction("Reserves");
        }

        public ActionResult Users()
        {
            var list = new List<Users>();
            list = db.Users.ToList();
            return View(list);
        }

        public ActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateUser(Users user)
        {
            if(db.Users.Any(x => x.Code == user.Code))
            {
                ViewBag.same = "این کد ملی قبلا ثبت شده است.";
                return View(user);
            }

            db.Users.Add(user);
            db.SaveChanges();
            return RedirectToAction("Users");
        }

        [HttpGet]
        public ActionResult EditUser(int Id)
        {
            var entity = db.Users.Find(Id);

            return View(entity);
        }

        [HttpPost]
        public ActionResult EditUser(UserType user)
        {
            

            db.Entry(user).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Users");
        }

        [HttpGet]
        public ActionResult DeleteUser(int Id)
        {
            var entity = db.Users.FirstOrDefault(x => x.Id == Id);

            db.Users.Remove(entity);
            db.SaveChanges();
            return RedirectToAction("Users");
        }
    }
}