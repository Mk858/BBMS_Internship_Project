using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BBMS_Project.Controllers
{
    public class HomeController : Controller
    {
       
        public ActionResult Index()
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("AdminLogin", "Admin");

            }
            else
            {
                return View();
            }


        }

        public ActionResult HomePage() { 

            return View();
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

        public ActionResult EditDonor()
        {

            return RedirectToAction("EditDonordetails","Admin");
        
        }
        public ActionResult Slogin()
        {
            return RedirectToAction("SeekerLogin", "Admin");
        }
    }
}