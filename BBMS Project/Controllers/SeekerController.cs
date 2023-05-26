using BBMS_Project.Models;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Npgsql;
using System.Configuration;
using System.Data;
using BBMS_Project.Repository;
using System.Web.Helpers;

namespace BBMS_Project.Controllers
{
    public class SeekerController : Controller
    {
        // ------------------------------------------------ Seeker ----------------------------------------------------


        SeekerRepository SeekerRepo = new SeekerRepository();
        DropDown ddl = new DropDown();

        //Seeker Login
        [HttpGet]
        public ActionResult SeekerLogin()
        {
            if (HttpContext.User.Identity.IsAuthenticated) //check if authenticated user is available
            {
                return RedirectToAction("SeekerLogin", "Seeker"); //if yes redirect to EmployeeController - means user is still log in
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SeekerLogin(SeekerModel obj)
        {
            if (obj.Email == "keyError" && obj.Phoneno == "keyError")
            {
                return View("SeekerLogin");
            }
            else
            {
                if (SeekerRepo.isAuthorized(obj.Email, obj.Phoneno) == true)
                {
                    Session["SeekerID"] = SeekerRepo.sid;
                    Session["Seeker"] = Convert.ToString(obj.Email);
                    /* FormsAuthentication.SetAuthCookie(obj.Email, false);*/

                    if (Session["SeekerID"] != null )
                    {
                        ViewBag.Message = "Login successfully";
                       
                    }
                    
                }
                else
                {
                    string str = "Incorrect login details";
                    TempData["fail"] = str;
                    return RedirectToAction("SeekerLogin", "Seeker");
                }
            }
            return View(obj);
        }

        // View when seeker logs in
        public ActionResult Index1()
        {
            return View();
        }

        // Log Out 
        public ActionResult LogoutSeeker()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Homepage", "Home"); //redirect to index
        }


        //Add Seeker
        public ActionResult AddSeeker()
        {
            List<StateModel> GetStateList = ddl.GetStateList();
            ViewBag.StateName = new SelectList(GetStateList, "stateid", "statename");
            List<GenderModel> GetGender = ddl.GetGender();
            ViewBag.GenderName = new SelectList(GetGender, "genderid", "Gender");

            List<BloodGroupModel> GetBloodGroup = ddl.GetBloodGroup();
            ViewBag.BloodGroupName = new SelectList(GetBloodGroup, "bloodgroupid", "Bloodgroup");

            return View();
        }


        [HttpPost]
        public ActionResult AddSeeker(SeekerModel Seek)
        {

            try
            {

                if (!ModelState.IsValid)
                {

                    if (SeekerRepo.AddSeeker(Seek))
                    {
                        ViewBag.Message = "Seeker details added successfully";
                    }
                    return RedirectToAction("SeekerLogin");

                }

                return View();

            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }


        /* //Add Seeker
         public ActionResult AddSeeker()
         {
             List<StateModel> GetStateList = ddl.GetStateList();
             ViewBag.StateName = new SelectList(GetStateList, "stateid", "statename");
             List<GenderModel> GetGender = ddl.GetGender();
             ViewBag.GenderName = new SelectList(GetGender, "genderid", "Gender");

             List<BloodGroupModel> GetBloodGroup = ddl.GetBloodGroup();
             ViewBag.BloodGroupName = new SelectList(GetBloodGroup, "bloodgroupid", "Bloodgroup");

             return View();
         }


         [HttpPost]
         public ActionResult AddSeeker(SeekerModel Seek)
         {

             try
             {

                 if (!ModelState.IsValid)
                 {

                     if (SeekerRepo.AddSeeker(Seek))
                     {
                         ViewBag.Message = "Seeker details added successfully";
                     }
                     return View();
                 }

                 return View();

             }
             catch (Exception ex)
             {
                 ViewBag.Message = ex.Message;
                 return View();
             }
         }*/



        // Edit Seeker Details
        public ActionResult EditSeekerDetails(SeekerModel obj)
        {

            List<GenderModel> GetGender = ddl.GetGender();
            ViewBag.GenderName = new SelectList(GetGender, "genderid", "gender");

            List<BloodGroupModel> GetBloodGroup = ddl.GetBloodGroup();
            ViewBag.BloodGroupName = new SelectList(GetBloodGroup, "bloodgroupid", "bloodgroup");

            ViewBag.StateName = ddl.GetStateList();

            SeekerModel Seeker = new SeekerModel();
            Seeker.seekerid = (int)Session["SeekerID"];

            return View(SeekerRepo.GetAllSeeker().Find(Skr => Skr.seekerid == (int)Session["SeekerID"]));

            //return View();
        }

        [HttpPost]
        public ActionResult EditSeekerDetails(SeekerModel obj, FormCollection fc)
         {
            try
            {

                if (!ModelState.IsValid)
                {
                    SeekerRepo.UpdateSeeker(obj);
                }
                return View();            }
            catch
            {
                return View();
            }
        }


        public ActionResult DeleteSeeker(int id)
        {
            if (Session["SeekerID"] != null)
            {
                SeekerModel Seeker = new SeekerModel();
                Seeker.seekerid = (int)Session["SeekerID"];
                SeekerRepo.DeleteSeeker(id);

                LogoutSeeker();
                return RedirectToAction("Homepage", "Home");
            }

            else
            {
                return RedirectToAction("SeekerLogin", "Seeker");
            }

        }

        /* [HttpGet]
         public ActionResult DeleteSeeker(int id)
         {
             try
             {
                 if (Session["SeekerID"] != null)
                 {
                     if (SeekerRepo.DeleteSeeker(id))
                     {
                         return RedirectToAction("Homepage", "Home");
                     }
                 }
                 else
                 {
                     return RedirectToAction("SeekerLogin", "Seeker");
                 }
                 return View();
             }
             catch
             {
                 return View();
             }

         }*/

        //Add Seeker Request
        [HttpGet]
        public ActionResult AddRequest()
        {

            //SeekerRepository SeekRepo = new SeekerRepository();

            ViewBag.BloodGroupName = ddl.GetBloodGroup();
            ViewBag.StateName = ddl.GetStateList();

            return View();
        }



        [HttpPost]
        public ActionResult AddRequest(SeekerModel Seek)
        {
            // SeekerRepository repo = new SeekerRepository();

            try
            {

                if (SeekerRepo.AddRequest(Seek))
                {
                    ViewBag.Message = "Request sent successfully";
                }
                return RedirectToAction("Index1", "Seeker");

                /* }*/
                /*  }*/
            }
            catch (Exception ex)
            {
                /* ViewBag.Message = ex.Message;*/
                throw ex;
            }
            /* return View();*/
        }

        [HttpGet]
        public ActionResult HistoryDetails()
        {
            ModelState.Clear();
            return View(TempData["data"] = SeekerRepo.GetAllHistory());
        }

        [HttpPost]
        public ActionResult ConvertToPDF()
        {
            if (TempData["data"] != null)
            {
                List<SeekerModel> dt = TempData["data"] as List<SeekerModel>;
                var document = new iTextSharp.text.Document(PageSize.A4, 9, 9, 9, 9);
                var output = new MemoryStream();
                PdfWriter.GetInstance(document, output);
                document.Open();
                var numOfColumns = 7;
                Font fontHeader = new Font(null, 7, Font.BOLD);
                Font fontText = new Font(null, 7, Font.NORMAL);
                var dataTable = new PdfPTable(numOfColumns);
                dataTable.DefaultCell.Padding = 1;
                dataTable.DefaultCell.BorderWidth = 1;
                dataTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                dataTable.AddCell(new PdfPCell(new Phrase("Bloodgroup", fontHeader)));
                dataTable.AddCell(new PdfPCell(new Phrase("State", fontHeader)));
                dataTable.AddCell(new PdfPCell(new Phrase("City", fontHeader)));
                dataTable.AddCell(new PdfPCell(new Phrase("Bloodbank", fontHeader)));
                dataTable.AddCell(new PdfPCell(new Phrase("Volume", fontHeader)));
                dataTable.AddCell(new PdfPCell(new Phrase("RequirementDate", fontHeader)));
                dataTable.AddCell(new PdfPCell(new Phrase("Createdon", fontHeader)));
                dataTable.HeaderRows = 1;
                dataTable.DefaultCell.BorderWidth = 1;
                dataTable.TotalWidth = 500f;
                dataTable.WidthPercentage = 100;
                float[] widths = new float[] { 75f, 75f, 75f, 75f, 75f, 80f, 80f };
                dataTable.SetWidths(widths);
                foreach (SeekerModel report in dt)
                {
                    dataTable.AddCell(new PdfPCell(new Phrase(report.Bloodgroup, fontText)));
                    dataTable.AddCell(new PdfPCell(new Phrase(report.State, fontText)));
                    dataTable.AddCell(new PdfPCell(new Phrase(report.City, fontText)));
                    dataTable.AddCell(new PdfPCell(new Phrase(report.Bloodbank, fontText)));
                    dataTable.AddCell(new PdfPCell(new Phrase(Convert.ToString(report.Volume), fontText)));
                    dataTable.AddCell(new PdfPCell(new Phrase(Convert.ToString(report.RequirementDate), fontText)));
                    dataTable.AddCell(new PdfPCell(new Phrase(Convert.ToString(report.Createdon), fontText)));
                }
                document.Add(dataTable);
                document.Close();
                TempData.Keep("data");
                TempData.Peek("data");
                return File(output.ToArray(), "application/pdf", "Report" + '_' + string.Format("{0:ddMMyyyy_HHmm}", (DateTime.Now)) + ".pdf");
            }
            else
                return Json(null);
        }

      /*  [HttpPost]
        public JsonResult BloodbankList(int id)
        {
            List<BloodBankModel> bloodbanklist = ddl.bloodbanklist(id);
            return Json(new SelectList(bloodbanklist, "Value", "Text", JsonRequestBehavior.AllowGet));
        }*/



             public JsonResult bloodbanklist(int id)
             {
                 List<SelectListItem> bloodbanklist = new List<SelectListItem>(id);

                 string constr = ConfigurationManager.ConnectionStrings["conn"].ToString();
                 NpgsqlConnection con = new NpgsqlConnection(constr);

                 con.Open();

                 NpgsqlTransaction tran = con.BeginTransaction();

                 string query = "select * from public.alldropdown('dropdownref'";
                 query = query + ",p_operation:=" + 5;
                 query = query + ",p_cityid:=" + id;
                 query = query + ");";

                 NpgsqlCommand com = new NpgsqlCommand(query, con);
                 com.ExecuteNonQuery();

                 NpgsqlDataAdapter da = new NpgsqlDataAdapter(com);

                 DataTable dt = new DataTable();

                 com.CommandText = "fetch all in " + "\"dropdownref\";";
                 com.CommandType = CommandType.Text;
                 da.Fill(dt);

                 foreach (DataRow dr in dt.Rows)
                 {
                     bloodbanklist.Add(new SelectListItem
                     {
                         Value = Convert.ToInt32(dr["bloodbankid"]).ToString(),
                         Text = Convert.ToString(dr["name"])
                     });
                 }

                 return Json(new SelectList(bloodbanklist, "Value", "Text", JsonRequestBehavior.AllowGet));

             }

        [HttpPost]
        public JsonResult cityList(int id, SeekerModel obj)
        {
            List<CityModel> GetCityList = ddl.GetCities(id);
            return Json(new SelectList(GetCityList, "cityid", "cityname", obj.City, JsonRequestBehavior.AllowGet));
        }


    }
    
}
