using BBMS_Project.Models;
using BBMS_Project.Repository;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BBMS_Project.Controllers
{
    public class BloodBankController : Controller
    {

        BloodBankRepository bbr = new BloodBankRepository();
        DropDown ddl = new DropDown();

        //Get : Blood Bank Login
        [HttpGet]
        public ActionResult bloodBankLogin()
        {
            return View();
        }

        //Post : Blood Bank Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult bloodBankLogin(BloodBankModel obj)
        {
            string email = obj.email;
            string phoneno = obj.phoneno;

            if (bbr.IsValidBldBnk(email, phoneno))
            {
                ViewBag.Message = "Login successfully";
                Session["bloodbankid"] = bbr.bbid;
                Session["bloodbank"] = Convert.ToString(email);
                return RedirectToAction("WelCome", "BloodBank");
            }

            else
            {
                ViewBag.Message = "Your email and Phone No. is incorrect";
                return RedirectToAction("HomePage", "Home");
            }
        }

        public ActionResult bLogout() //Log out
        {
            Session["bloodbank"] = null;
            Session["bloodbankid"] = null;
            return RedirectToAction("HomePage", "Home"); //redirect to Login
        }

        // GET: BloodBank/Details/5
        public ActionResult WelCome()
        {
            if (Session["BloodBank"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("bloodBankLogin", "BloodBank");
            }
        }

        // GET: BloodBank/Create
        public ActionResult AddBloodBank()
        {
            List<StateModel> GetStateList = ddl.GetStateList();
            ViewBag.StateName = new SelectList(GetStateList, "stateid", "statename");

            List<GenderModel> GetGender = ddl.GetGender();
            ViewBag.GenderName = new SelectList(GetGender, "genderid", "gender");

            return View();
        }

        // POST: BloodBank/Create
        [HttpPost]
        public ActionResult AddBloodBank(BloodBankModel obj, FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                if (bbr.AddBloodBank(obj))
                {
                    ViewBag.Message = "BloodBank details added successfully";
                    return RedirectToAction("bloodBankLogin", "BloodBank");
                }
                else
                {
                    return RedirectToAction("HomePage", "Home");
                }

            }
            catch
            {
                return View();
            }
        }

        // GET: BloodBank/Edit/5
        public ActionResult EditBloodBank(int id ,BloodBankModel obj)
        {
            if (Session["bloodbank"] != null)
            {
                List<StateModel> GetStateList = ddl.GetStateList();
                ViewBag.StateName = new SelectList(GetStateList, "stateid", "statename", obj.statename);

                obj.bloodbankid = id;

                return View(bbr.GetAllBloodBank().Find(bb => bb.bloodbankid == id));
            }
            else
            {
                    return RedirectToAction("bloodBankLogin", "BloodBank");
            }
        }

        // POST: BloodBank/Edit/5
        [HttpPost]
        public ActionResult EditBloodBank(int id,BloodBankModel obj, FormCollection collection)
        {
            try
            {        
            
                    if(bbr.UpdateBloodBank(id,obj))
                    {
                        ViewBag.Message = "Details Updated";
                        return RedirectToAction("WelCome", "BloodBank");
                    }
                    else
                { return View(); }
                    
            }
            catch
            {
                return View();
            }
        }

        // GET: BloodBank/Delete/5
        public ActionResult DeleteBloodBank(int id)
        {
            BloodBankModel obj = new BloodBankModel();
            obj.bloodbankid = id;
            return View(bbr.GetAllBloodBank().Find(Dnr => Dnr.bloodbankid == id));
        }

        // POST: BloodBank/Delete/5
        [HttpPost]
        public ActionResult DeleteBloodBank(int id, FormCollection collection)
        {
            try
            {
                if (Session["bloodbank"] != null)
                {
                    if (bbr.DeleteBloodBank(id))
                    {
                        return RedirectToAction("HomePage", "Home");
                    }
                    return View();
                }
                else
                {
                    return RedirectToAction("bloodBankLogin", "BloodBank");
                }
            }
            catch
            {
                return View();
            }
        }

        public ActionResult GetDonorRequest()
        {
            if (Session["BloodBank"] != null)
            {
                return View(bbr.getDonorRequest());
            }
            else
            {
                return RedirectToAction("bloodBankLogin", "BloodBank");
            }
        }

        public ActionResult GetSeekerRequest()
        {
            if (Session["BloodBank"] != null)
            {
                return View(bbr.getSeekerRequest());
            }
            else
            {
                return RedirectToAction("bloodBankLogin", "BloodBank");
            }
        }

        [HttpPost]
        public JsonResult cityList(int id, BloodBankModel obj)
        {
            List<CityModel> GetCityList = ddl.GetCities(id);
            return Json(new SelectList(GetCityList, "cityid", "cityname", obj.cityname, JsonRequestBehavior.AllowGet));
        }


        public ActionResult GetAllBloodBankDetails()
        {
           
            
                ModelState.Clear();
                return View(bbr.GetAllBloodBank());
            

        }


       // GET: Blood Bank Details
        public ActionResult getBloodBankDetails(BloodBankModel obj)
        {
            List<StateModel> GetStateList = ddl.GetStateList();
            ViewBag.StateName = new SelectList(GetStateList, "stateid", "statename");

            return View(obj);
        }

        //POST:  Blood Bank Details
        public ActionResult showBloodBankDetails(FormCollection collection, BloodBankModel obj)
        {
            return View(bbr.searchBloodBank(obj));
        }
    }
}





/*using BBMS_Project.Models;
using BBMS_Project.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BBMS_Project.Controllers
{
    public class BloodBankController : Controller
    {
        BloodBankRepository bbr = new BloodBankRepository();
        DropDown ddl = new DropDown();

        //Get : Blood Bank Login
        [HttpGet]
        public ActionResult bloodBankLogin()
        {
            return View();
        }

        //Post : Blood Bank Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult bloodBankLogin(BloodBankModel obj)
        {

            string email = obj.email;
            string phoneno = obj.phoneno;
            Session["bloodbank"] = Convert.ToString(obj.email);
            if (Session["bloodbank"] == null)
            {
                return View("bloodBankLogin");

            }
            else
            {

                if (bbr.IsValidBldBnk(email, phoneno))
                {
                    ViewBag.Message = "Login successfully";
                    FormsAuthentication.SetAuthCookie(email, false);
                    return RedirectToAction("HomePage", "Home");
                }
                else
                {
                    ViewBag.Message = "Your email and Phone No. is incorrect";
                }
                return View(obj);
            }
        }

        public ActionResult bLogout() //Log out
        {
            Session["bloodbank"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("HomePage", "Home"); //redirect to Login
        }

        // GET: BloodBank/Details/5
        public ActionResult WelCome()
        {
            return View();
        }

        // GET: BloodBank/Create
        public ActionResult AddBloodBank()
        {
            List<StateModel> GetStateList = ddl.GetStateList();
            ViewBag.StateName = new SelectList(GetStateList, "stateid", "statename");

            List<GenderModel> GetGender = ddl.GetGender();
            ViewBag.GenderName = new SelectList(GetGender, "genderid", "gender");

            return View();
        }

        // POST: BloodBank/Create
        [HttpPost]
        public ActionResult AddBloodBank(BloodBankModel obj,FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                
                if (bbr.AddBloodBank(obj))
                {
                        ViewBag.Message = "BloodBank details added successfully";
                }
                else
                {
                    return RedirectToAction("AdminLogin", "Admin");
                }
                return View();

            }
            catch
            {
                return View();
            }
        }

        // GET: BloodBank/Edit/5
        public ActionResult EditBloodBank(BloodBankModel obj)
        {

            List<StateModel> GetStateList = ddl.GetStateList();
            ViewBag.StateName = new SelectList(GetStateList, "stateid", "statename", obj.statename);

            obj.bloodbankid = (int)Session["bloodbank"];

            return View(bbr.GetAllBloodBank().Find(Dnr => Dnr.bloodbankid == (int)Session["bloodbank"]));
        }

        // POST: BloodBank/Edit/5
        [HttpPost]
        public ActionResult EditBloodBank(BloodBankModel obj, FormCollection collection)
        {
            try
            {
                if (Session["bloodbank"] != null)
                {

                    if (ModelState.IsValid)
                    {
                        bbr.UpdateBloodBank(obj);
                    }
                    return RedirectToAction("GetAllBloodBankDetails");
                }
                else
                {
                    return RedirectToAction("AdminLogin", "Admin");
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: BloodBank/Delete/5
        public ActionResult DeleteBloodBank(BloodBankModel obj)
        {
            obj.bloodbankid = (int)Session["bloodbank"];
            return View(bbr.GetAllBloodBank().Find(Dnr => Dnr.bloodbankid == (int)Session["bloodbank"]));
        }

        // POST: BloodBank/Delete/5
        [HttpPost]
        public ActionResult DeleteBloodBank(int id, FormCollection collection)  
        {
            try
            {
                if (Session["bloodbank"] != null)
                {
                    if (bbr.DeleteBloodBank(id))
                    {
                        return RedirectToAction("GetAllBloodBankDetails");
                    }
                    return View();
                }
                else
                {
                    return RedirectToAction("AdminLogin", "Admin");
                }
            }
            catch
            {
                return View();
            }
        }
    }
}
*/