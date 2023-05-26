using BBMS_Project.Models;
using BBMS_Project.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BBMS_Project.Controllers
{
    public class CampController : Controller
    {
        CampRepository cr = new CampRepository();
        DropDown ddl = new DropDown();

        // GET: All Camp Details
        public ActionResult getAllCampDetails()
        {
            if (Session["BloodBank"] != null)
            {
                return View(cr.GetBBCampDetails());
            }
            else
            {
                return RedirectToAction("bloodBankLogin", "BloodBank");
            }
        }

        // GET: Camp
        public ActionResult getCampDetails(Camp obj)
        {
            List<StateModel> GetStateList = ddl.GetStateList();
            ViewBag.StateName = new SelectList(GetStateList, "stateid", "statename");

            return View(obj);
        }

        //POST: Camp
        public ActionResult showCampDetails(FormCollection collection, Camp obj)
        {
            List<StateModel> GetStateList = ddl.GetStateList();
            ViewBag.StateName = new SelectList(GetStateList, "stateid", "statename");

            return View(cr.GetCampDetails(obj));
        }

        // GET: Camp/Create
        public ActionResult AddCamp()
        {
            if (Session["BloodBank"] != null)
            {
                List<StateModel> GetStateList = ddl.GetStateList();
                ViewBag.StateName = new SelectList(GetStateList, "stateid", "statename");
                return View();
            }
            else
            {
                return RedirectToAction("bloodBankLogin", "BloodBank");
            }
        }

        // POST: Camp/Create
        [HttpPost]
        public ActionResult AddCamp(Camp obj,FormCollection collection)
        {
            try
            {
                if (Session["BloodBank"] != null)
                {
                    // TODO: Add insert logic here

                    if (cr.AddBBCamp(obj))
                    {
                        ViewBag.Message = "Camp details added successfully";
                        return RedirectToAction("GetAllCampDetails","Camp");
                    }
                }
                else
                {
                    return RedirectToAction("bloodBankLogin", "BloodBank");
                }
                return View();
            }
            catch
            {
                return View();
            }
        }        

        // GET: Camp/Edit/5
        public ActionResult EditCamp(int id)
        {
            Camp obj = new Camp();

            List<StateModel> GetStateList = ddl.GetStateList();
            ViewBag.StateName = new SelectList(GetStateList, "stateid", "statename", obj.state);

            return View(cr.GetAllCampDetails().Find(Camp => Camp.campid == id));
        }

        // POST: Camp/Edit/5
        [HttpPost]
        public ActionResult EditCamp(int id,Camp obj, FormCollection collection)
        {
            try
            {

                if (Session["BloodBank"] != null)
                {
                    // TODO: Add update logic here
                    if (cr.UpdateCamp(id, obj))
                    {
                        ViewBag.Message = "Camp details Updated successfully";
                        return RedirectToAction("GetAllCampDetails");
                    }
                }
                else
                {
                    return RedirectToAction("bloodBankLogin", "BloodBank");
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Camp/Delete/5
        public ActionResult DeleteCamp(int id)
        {
            if (Session["BloodBank"] != null)
            {
                return View(cr.GetAllCampDetails().Find(Camp => Camp.campid == id));
            }
            else
            {
                return RedirectToAction("bloodBankLogin", "BloodBank");
            }
        }

        // POST: Camp/Delete/5
        [HttpPost]
        public ActionResult DeleteCamp(int id, FormCollection collection)
        {
            try
            {
                if (Session["BloodBank"] != null)
                {
                    // TODO: Add delete logic here
                    if (cr.DeleteCamp(id))
                    {
                        ViewBag.AlertMsg = "Camp details deleted successfully";
                        return RedirectToAction("GetAllCampDetails");
                    }
                }
                else
                {
                    return RedirectToAction("bloodBankLogin", "BloodBank");
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public JsonResult cityList(int id,Camp obj)
        {
            List<CityModel> GetCityList = ddl.GetCities(id);
            return Json(new SelectList(GetCityList, "cityid", "cityname",obj.city,JsonRequestBehavior.AllowGet));
        }
    }
}
