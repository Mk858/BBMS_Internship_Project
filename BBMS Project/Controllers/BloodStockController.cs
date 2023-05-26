using BBMS_Project.Models;
using BBMS_Project.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BBMS_Project.Controllers
{
    public class BloodStockController : Controller
    {
        BloodStockRepository bsr = new BloodStockRepository();
        DropDown ddl = new DropDown();

        // GET: BloodStock
        public ActionResult getBloodStock()
        {
            if (Session["BloodBank"] != null)
            {
                return View(bsr.getUserBloodStock());
            }
            else
            {
                return RedirectToAction("bloodBankLogin", "BloodBank");
            }
        }

        // GET: BloodStock/Create
        public ActionResult AddBloodStock()
        {
            if (Session["BloodBank"] != null)
            {
                ViewBag.bgls = ddl.GetBloodGroup();
                return View();
            }
            else
            {
                return RedirectToAction("bloodBankLogin", "BloodBank");
            }
        }

        // POST: BloodStock/Create
        [HttpPost]
        public ActionResult AddBloodStock(BloodStockModel obj, FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                if (bsr.AddBloodStock(obj))
                {
                    ViewBag.Message = "BloodStock details added successfully";
                    return RedirectToAction("getBloodStock", "BloodStock");
                }
                else
                {
                    ViewBag.Message = "BloodStock details add Failed";
                    return View();
                }

            }
            catch
            {
                return View();
            }
        }

        // GET: BloodStock/Edit/5
        public ActionResult EditBloodStock(int id)
        {
            if (Session["BloodBank"] != null)
            {
                BloodStockModel obj = new BloodStockModel();
               /* List<StateModel> GetStateList = ddl.GetStateList();
                ViewBag.StateName = new SelectList(GetStateList, "stateid", "statename", obj.statename);*/
                List<BloodGroupModel> GetBloodGroup = ddl.GetBloodGroup();
                ViewBag.BloodGroupName = new SelectList(GetBloodGroup, "bloodgroupid", "Bloodgroup",obj.bloodgroup);


                obj.bloodstockid = id;

                return View(bsr.getUserBloodStock().Find(bs => bs.bloodstockid == id));
            }
            else
            {
                return RedirectToAction("bloodBankLogin", "BloodBank");
            }
        }

        // POST: BloodStock/Edit/5
        [HttpPost]
        public ActionResult EditBloodStock(int id, BloodStockModel obj,FormCollection collection)
        {
            try
            {
                if (Session["bloodbank"] != null)
                {

                    if (bsr.UpdateBloodStock(id, obj))
                    {
                        ViewBag.Message = "BloodStock details Updated successfully";
                        return RedirectToAction("getBloodStock", "BloodStock");
                    }
                    else
                    {
                        ViewBag.Message = "BloodStock details Updation Failed";
                        return RedirectToAction("getBloodStock", "BloodStock");
                    }
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

        // GET: BloodStock/Delete/5
        public ActionResult DeleteBloodStock(int id , BloodStockModel obj)
        {
            obj.bloodbankid = id;
            return View(bsr.getBloodStock().Find(bs => bs.bloodstockid == id));
            return View();
        }

        // POST: BloodStock/Delete/5
        [HttpPost]
        public ActionResult DeleteBloodStock(int id, FormCollection collection)
        {
            try
            {
                if (Session["bloodbank"] != null)
                {
                    if (bsr.DeleteBloodStock(id))
                    {
                        return RedirectToAction("getBloodStock", "BloodStock");
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
    }
}
