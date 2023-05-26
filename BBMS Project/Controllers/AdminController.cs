using BBMS.Repository;
using BBMS_Project.Models;
using BBMS_Project.Repository;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Newtonsoft.Json;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web.Mvc;
using System.Web.Security;

namespace BBMS_Project.Controllers
{
    public class AdminController : Controller
    {

        AdminRepository AdminRepo = new AdminRepository();
        SeekerRepository SeekerRepo = new SeekerRepository();
        DonorRepository DonorRepo = new DonorRepository();
        BloodStockRepository BloodStockRepo = new BloodStockRepository();
        BloodBankRepository BloodBankRepo = new BloodBankRepository();
       CampRepository CampRepo = new CampRepository();
        DropDown ddl = new DropDown();

        public ActionResult Index()
        {
            return View();
        }


        // ************************************** ADMIN LOGIN ******************************************************
        //Admin Login
        [HttpGet]
        public ActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]

        [ValidateAntiForgeryToken]
        public ActionResult AdminLogin(AdminModel obj)
        {

            string email = obj.email;
            string password = obj.password;
            Session["Admin"] = Convert.ToString(obj.email);
            if (Session["Admin"] == null)
            {
                return View("AdminLogin");
            }
            else
            {
                if (AdminRepo.IsValidAdmin(email, password))
                {

                    FormsAuthentication.SetAuthCookie(email, false);
                    ViewBag.Message = "Login successfully";
                    /* System.Threading.Thread.Sleep(15000);*/
                    /*return RedirectToAction("Index", "Home");*/
                }
                else
                {
                    ViewBag.Msg = "Your email and password is incorrect";

                }
                return View(obj);
            }
        }


        public ActionResult Logout() //Log out
        {
            Session["Admin"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("AdminLogin"); //redirect to Login
        }

        // ************************************** Seeker ******************************************************

        //GetAllSeeker Data
        public ActionResult GetAllSeekerDetails()
        {
            if (Session["Admin"] != null)
            {
                ModelState.Clear();
                return View(AdminRepo.GetAllSeeker());
            }
            else
            {
                return RedirectToAction("AdminLogin", "Admin");
            }
        }

        // Delete Seeker 
        [HttpGet]
        public ActionResult DeleteSeeker(int id)
         {
            try
            {
                if (Session["Admin"] != null)
                {
                    if (SeekerRepo.DeleteAdminSeeker(id))
                    {
                        ViewBag.Message = "Deleted successfully";
                        return RedirectToAction("GetAllSeekerDetails");
                    }
                   
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
             return null;
        }

        // ************************************** Seeker Request  ******************************************************
        public ActionResult ApproveSeekerReq(int id, int vol)
        {
            try
            {
                if (Session["Admin"] != null)
                {
                    if (ModelState.IsValid)
                    {
                        AdminRepo.ApproveSeekerReq(id, vol);
                        ViewBag.Message = "Request Approved";
                    }
                    return RedirectToAction("GetAllSeekerRequest");
                }
                else
                {
                    return RedirectToAction("AdminLogin", "Admin");
                }
            }
            catch
            {
                return View("Index", "Admin");
            }


        }

        public ActionResult RejectSeekerReq(int id)
        {
            try
            {
                if (Session["Admin"] != null)
                {
                    if (ModelState.IsValid)
                    {
                        AdminRepo.RejectSeekerReq(id);
                        ViewBag.Message = "Rejected Seeker Request";
                    }
                    return RedirectToAction("GetAllSeekerRequest");
                }
                else
                {
                    return RedirectToAction("AdminLogin", "Admin");
                }
            }
            catch
            {
                return View("Index", "Home");
            }
        }


        //Get All Approved Seeker Details
        public ActionResult GetAllApproveSeekerRequest()
        {
            if (Session["Admin"] != null)
            {
                ModelState.Clear();
                return View(AdminRepo.GetAllApproveSeekerRequest());
            }
            else
            {
                return RedirectToAction("AdminLogin", "Admin");
            }

        }

        //Get All Approved Seeker Details
        public ActionResult GetAllRejectSeekerRequest()
        {
            if (Session["Admin"] != null)
            {
                ModelState.Clear();
                return View(AdminRepo.GetAllRejectSeekerRequest());
            }
            else
            {
                return RedirectToAction("AdminLogin", "Admin");
            }

        }


        // ************************************** Donor ******************************************************
        //GetAllDonor Data
        public ActionResult GetAllDonorDetails()
        {
            if (Session["Admin"] != null)
            {
                ModelState.Clear();
               /* AdminRepo.CountDonor();*/
                return View(AdminRepo.GetAllDonor());
            }
            else
            {
                return RedirectToAction("AdminLogin", "Admin");
            }

        }


        // Edit Donor Details
        public ActionResult EditDonorDetails(int id)
        {
            
                DonorModel Donor = new DonorModel();
                Donor.donorid = id;

                List<StateModel> GetStateList = ddl.GetStateList();
                ViewBag.StateName = new SelectList(GetStateList, "stateid", "statename", Donor.State);

                List<GenderModel> GetGender = ddl.GetGender();
                ViewBag.GenderName = new SelectList(GetGender, "genderid", "gender", Donor.Gender);

                List<BloodGroupModel> GetBloodGroup = ddl.GetBloodGroup();
                ViewBag.BloodGroupName = new SelectList(GetBloodGroup, "bloodgroupid", "bloodgroup", Donor.Bloodgroup);
            

            return View(AdminRepo.GetAllDonor().Find(Dnr => Dnr.donorid == id));
        }

        [HttpPost]

        public ActionResult EditDonorDetails(int id, DonorModel obj)
        {
            try
            {
                if (Session["Admin"] != null)
                {
                    if (!ModelState.IsValid)
                    {
                        DonorRepo.UpdateDonor(obj);
                        ViewBag.Message = "Donor Details Updated  successfully";
                    }
                    return RedirectToAction("GetAllDonorDetails");
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


        // Delete Donor 
        [HttpGet]
        public ActionResult DeleteDonor(int id)
        {
            try
            {
                if (Session["Admin"] != null)
                {
                    ViewBag.Message = "Are you sure you want to delete this?";
                    if (DonorRepo.DeleteAdminDonor(id))
                    {
                        return RedirectToAction("GetAllDonorDetails");
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



        // ************************************** Donor Request  ******************************************************


        //Get All Donor Request
        public ActionResult GetAlldonorRequest()
        {

            if (Session["Admin"] == null)
            {
                ModelState.Clear();
                return View("AdminLogin");
            }
            else
            {
                return View(AdminRepo.GetAllDonorRequest());
            }

        }


        public ActionResult ApproveDonorReq(int id, int vol)
        {
            try
            {
                if (Session["Admin"] != null)
                {
                    if (ModelState.IsValid)
                    {
                        AdminRepo.ApproveDonorReq(id, vol);
                        ViewBag.Message = "Approved Donor Request";
                    }
                    return RedirectToAction("GetAllDonorRequest");
                }
                else
                {
                    return RedirectToAction("AdminLogin", "Admin");
                }
            }
            catch
            {
                return View("Index", "Home");
            }
        }

        public ActionResult RejectDonorReq(int id)
        {
            try
            {
                if (Session["Admin"] != null)
                {
                    if (ModelState.IsValid)
                    {
                        AdminRepo.RejectDonorReq(id);
                        ViewBag.Message = "Rjected Donor Request";
                    }
                    return RedirectToAction("GetAllDonorRequest");
                }
                else
                {
                    return RedirectToAction("AdminLogin", "Admin");
                }
            }
            catch
            {
                return View("Index", "Home");
            }
        }


        //Get All Approved Donor Details
        public ActionResult GetAllApprovedonorRequest()
        {
            if (Session["Admin"] != null)
            {
                ModelState.Clear();
                return View(AdminRepo.GetAllApprovedonorRequest());
            }
            else
            {
                return RedirectToAction("AdminLogin", "Admin");
            }

        }


        //Get All Reject Donor Details
        public ActionResult GetAllRejectdonorRequest()
        {
            if (Session["Admin"] != null)
            {
                ModelState.Clear();
                return View(AdminRepo.GetAllRejectdonorRequest());
            }
            else
            {
                return RedirectToAction("AdminLogin", "Admin");
            }

        }



        

        // ************************************** Blood Stock  ******************************************************

          //GetAllDonor Data
          public ActionResult GetAllBloodStock()
          {
              if (Session["Admin"] != null)
              {
                  ModelState.Clear();
                  return View(BloodStockRepo.getBloodStock());
              }
              else
              {
                  return RedirectToAction("AdminLogin", "Admin");
              }

          }



        // ************************************** BloodBank  ******************************************************

       

          // GET: BloodBank/Create
          public ActionResult AddBloodBank()
          {

              List<StateModel> GetStateList = ddl.GetStateList();
              ViewBag.StateName = new SelectList(GetStateList, "stateid", "statename");

              List<GenderModel> GetGender = ddl.GetGender();
              ViewBag.GenderName = new SelectList(GetGender, "genderid", "gender");


              return View();
          }

          // POST: Camp/Create
          [HttpPost]
          public ActionResult AddBloodBank(BloodBankModel obj)
           {
              try
              {
                  // TODO: Add insert logic here
                  if (Session["Admin"] != null /*|| Session["user"] != null*/)
                  {
                    if (BloodBankRepo.AddBloodBank(obj))
                    {
                        ViewBag.Message = "BloodBank details added successfully";
                        /* return RedirectToAction("GetAllBloodBankDetails", "Admin");*/
                    }
                    else
                    {
                        ViewBag.Message = "Failed";
                    }
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


        public ActionResult GetAllBloodBankDetails()
        {
            if (Session["Admin"] != null)
            {
                ModelState.Clear();
                return View(AdminRepo.GetAllBloodBank());
            }
            else
            {
                return RedirectToAction("AdminLogin", "Admin");
            }

        }


        // Edit Donor Details
        public ActionResult EditBloodBank(int id)
        {

            List<StateModel> GetStateList = ddl.GetStateList();
            ViewBag.StateName = new SelectList(GetStateList, "stateid", "statename");


            BloodBankModel BloodBank = new BloodBankModel();
            BloodBank.bloodbankid = id;

            return View(AdminRepo.GetAllBloodBank().Find(Dnr => Dnr.bloodbankid == id));

            //return View();
        }

        [HttpPost]

        public ActionResult EditBloodBank(int id, BloodBankModel obj)
        {
            try
            {
                if (Session["Admin"] != null)
                {

                    if (!ModelState.IsValid)
                    {
                        BloodBankRepo.UpdateBloodBank(id,obj);
                        ViewBag.Message = "BloodBank details Updated successfully";
                    }
                    else
                    {
                        ViewBag.Message = "Failed";
                    }
                    /* return RedirectToAction("GetAllBloodBankDetails");*/
                    
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


        [HttpGet]
        public ActionResult DeleteBloodBank(int id)
        {
            try
            {
                if (Session["Admin"] != null)
                {
                    if (BloodBankRepo.DeleteBloodBank(id))
                    {
                        ViewBag.Message = "Deleted successfully";
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



        /* -------------------------------------- Camp Schedule and Registration -------------------------------------------------------- */

        // GET: All Camp Details
        public ActionResult getAllCampDetails()
        {
            if (Session["Admin"] != null)
            {
                return View(AdminRepo.GetAllCampDetails());
            }
            else
            {
                return RedirectToAction("AdminLogin", "Admin");
            }
        }

     

        //POST: Camp
        public ActionResult showCampDetails(FormCollection collection, Camp obj)
        {
            List<StateModel> GetStateList = ddl.GetStateList();
            ViewBag.StateName = new SelectList(GetStateList, "stateid", "statename");

            return View(CampRepo.GetCampDetails(obj));
        }

        // GET: Camp/Create
        public ActionResult AddCamp()
        {
            if (Session["Admin"] != null)
            {
                List<StateModel> GetStateList = ddl.GetStateList();
                ViewBag.StateName = new SelectList(GetStateList, "stateid", "statename");
            }
            else
            {
                return RedirectToAction("AdminLogin", "Admin");
            }
            return View();
        }

        // POST: Camp/Create
        [HttpPost]
        public ActionResult AddCamp(FormCollection collection, Camp obj)
        {
            try
            {
                if (Session["Admin"] != null)
                {
                    // TODO: Add insert logic here

                    if (CampRepo.AddCamp(obj))
                    {
                        ViewBag.Message = "Camp details added successfully";
                    }
                    else
                    {
                        ViewBag.Message =" Failed";
                    }
                }
                else
                {
                    return RedirectToAction("AdminLogin", "Admin");
                }
                /* return RedirectToAction("AddCamp");*/
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Camp/Edit/5
        public ActionResult UpdateCamp(int id)
        {
            List<StateModel> GetStateList = ddl.GetStateList();
            ViewBag.StateName = new SelectList(GetStateList, "stateid", "statename");

            return View(AdminRepo.GetAllCampDetails().Find(Camp => Camp.campid == id));
        }

        // POST: Camp/Edit/5
        [HttpPost]
        public ActionResult UpdateCamp(int id, Camp obj, FormCollection collection)
        {
            try
            {

                if (Session["Admin"] != null)
                {
                    // TODO: Add update logic here
                    if (CampRepo.UpdateCamp(id, obj))
                    {
                        ViewBag.Message = "Camp details Updated successfully";
                    }
                    else
                    {
                        ViewBag.Message = "Failed";
                    }
                }
                else
                {
                    return RedirectToAction("AdminLogin", "Admin");
                }
                /* return RedirectToAction("GetAllCampDetails");*/
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
            try
            {
                if (Session["Admin"] != null)
                {
                    // TODO: Add delete logic here
                    if (CampRepo.DeleteCamp(id))
                    {
                        ViewBag.AlertMsg = "Camp details deleted successfully";

                    }
                    else
                    {
                        ViewBag.Message = "Failed";
                    }
                }
                else
                {
                    return RedirectToAction("AdminLogin", "Admin");
                }

                return RedirectToAction("GetAllCampDetails");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public JsonResult cityList(int id)
        {
            List<CityModel> GetCityList = ddl.GetCities(id);
            return Json(new SelectList(GetCityList, "cityid", "cityname", JsonRequestBehavior.AllowGet));
        }



        // Edit (Admin) Seeker Details
        public ActionResult EditAdminSeekerDetails(int id)
        {

            List<GenderModel> GetGender = ddl.GetGender();
            ViewBag.GenderName = new SelectList(GetGender, "genderid", "gender");

            List<BloodGroupModel> GetBloodGroup = ddl.GetBloodGroup();
            ViewBag.BloodGroupName = new SelectList(GetBloodGroup, "bloodgroupid", "bloodgroup");

            ViewBag.StateName = ddl.GetStateList();

            SeekerModel Seeker = new SeekerModel();
            Seeker.seekerid = id;

            return View(AdminRepo.GetAllSeeker().Find(Dnr => Dnr.seekerid == id));


            //* SeekerModel Seeker = new SeekerModel();
            Seeker.seekerid = (int)Session["SeekerID"];

            return View(AdminRepo.GetAllSeeker().Find(Skr => Skr.seekerid == (int)Session["SeekerID"]));//*

            //return View();
        }

        [HttpPost]

        public ActionResult EditAdminSeekerDetails(SeekerModel obj, FormCollection fc)
        {
            try
            {
                if (Session["Admin"] != null)
                {
                    if (!ModelState.IsValid)
                    {
                        SeekerRepo.UpdateSeeker(obj);
                        ViewBag.Message = "Seeker Details Updated successfully";
                    }
                    else
                    {
                        return RedirectToAction("AdminLogin", "Admin");
                    }




                }
                return RedirectToAction("GetALLSeekerDetails", "Admin");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult GetAllSeekerRequest()
        {
            if (Session["Admin"] != null)
            {
                ModelState.Clear();
                return View(AdminRepo.GetAllSeekerRequest());
            }
            else
            {
                return RedirectToAction("AdminLogin", "Admin");
            }


        }

  
   
        

    }



}