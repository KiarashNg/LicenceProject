using Behyab.DAL;
using Behyab.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Behyab.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home

        private readonly BehyabDB1Context db;

        public HomeController()
        {
            db = new BehyabDB1Context();
        }
        
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Call()
        {
            return View();
        }
    }
}