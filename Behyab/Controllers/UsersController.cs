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

    public class UsersController : Controller
    {
        readonly BehyabDB1Context db;

        public UsersController()
        {
            db = new BehyabDB1Context();
        }

        public ActionResult Register()
        {
            return View();
        }

        // GET: Users
        [HttpPost]
        public ActionResult Register(Users entity)
        {
            //این قسمت ثبت نام کاربر جدید میباشد که دستوراتش همانند ثبت یک سطر جدید در دیتابیس هست که قبلا در کنترلر ادمین توضیح دادم

            if (!ModelState.IsValid)
            {
                
            }

            if(db.Users.Any(x => x.Username == entity.Username))  //فقط در این قسمت چک میشود که یوزرنیم تکراری نباشد
            {
                ViewBag.username = "این نام کاربری قبلا ثبت شده است.";
                return View(entity);
            }

            if (db.Users.Any(x => x.Code == entity.Code))      //و همچنین چک میشود که کد ملی هم تکراری نباشد
            {
                ViewBag.message = "این کد ملی قبلا ثبت شده است.";
                return View(entity);
            }

            db.Users.Add(entity);
            db.SaveChanges();
            return RedirectToAction("Login");
        }

        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Users user)
        {
            if (!ModelState.IsValid)
            {

            }

            var obj = db.Users.Where(u => u.Username.Equals(user.Username) && u.Password.Equals(user.Password)).FirstOrDefault();  //در این قسمت چک میشود که یوزرنیم و پسورد صحیح باشد

            if (obj.UserTypeId == 1) //در این قسمت ادمین بودن یا نبودن چک میشود
            {
                FormsAuthentication.SetAuthCookie(user.Username, false);

                return RedirectToAction("Index","Admin");
            }

            

            if (obj == null)
            {
                ViewBag.log = "رمز عبور یا نام کاربری اشتباه است.";
                return View(user);
            }
           
            FormsAuthentication.SetAuthCookie(user.Username, false);  //و در نهایت یوزرنیم در کوکی ذخیره میشود

            return RedirectToAction("Index", "Reservation");   //و اینجا میرویم به اکشن ایندکس در کنترلر رزرویشن

        }
    }
}