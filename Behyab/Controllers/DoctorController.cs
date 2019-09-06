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
    public class DoctorController : Controller
    {
        readonly BehyabDB1Context db;
        public DoctorController()
        {
            db = new BehyabDB1Context();
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {
            var Experties = db.Experts.ToList();
            var selectlist = new SelectList(Experties, "Id", "Name");
            ViewBag.Experties = selectlist;

            var Clinics = db.Clinics.ToList();
            var selectlist1 = new SelectList(Clinics, "Id", "Name");
            ViewBag.Clinics = selectlist1;

            return View();
        }

        // GET: Users
        [HttpPost]
        public ActionResult Register(Doctor entity)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "خطایی رخ داده است.");
            }

            if (db.Doctors.Any(x => x.ExpCode == entity.ExpCode))
            {
                ViewBag.message = "این کد قبلا ثبت شده است.";
                return View(entity);
            }

            db.Doctors.Add(entity);
            db.SaveChanges();
            return RedirectToAction("Login");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Doctor entity)
        {

            //در این قسمت ورود پزشک میباشد که ابتدا چک میشود که آیا همچین مشخصاتی در دیتابیس وجود دارد یا خیر
            //اگر نبود که ارور های لازم نمایش داده میشود
            //اگر هم بود که به وسیله ی آیدی و یوزرنیم یک سشن یا همان نشست به این کاربر داده میشود
            //میخواستم با کوکی انجام بدم که مثل ورود کاربر باشه اما چون کوکی ها باهم قاطی میشد اینجا از سشن استفاده کردم

            if (!ModelState.IsValid)
            {

            }

            var obj = db.Doctors.Where(u => u.Username.Equals(entity.Username) && u.Password.Equals(entity.Password)).FirstOrDefault();

            if (obj == null)
            {
                ViewBag.log = "رمز عبور یا نام کاربری اشتباه است.";
                return View(entity);
            }

            Session["Id"] = obj.Id.ToString();
            Session["Username"] = obj.Username.ToString();
            return RedirectToAction("LoggedIn");

        }


        public ActionResult LoggedIn()
        {
            //در این قسمت هم با چک کردن سشن نکیگذاریم که فردی از بیرون بتواند دسترسی به این صفحه داشته باشد

            if (Session["Id"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
           
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.RemoveAll();

            return RedirectToAction("Index", "Home");
        }
    }
}