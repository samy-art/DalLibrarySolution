using DalLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCDAl.Controllers
{
    public class HomeController : Controller
    {
        

        CDal dal = new CDal();
        public ActionResult Index()
        {

            return View(dal.GetEmployees());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}