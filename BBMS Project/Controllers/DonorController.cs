using BBMS.Repository;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web.Mvc;
using System.Web.Security;
using BBMS_Project.Models;
using BBMS_Project.Repository;

namespace BBMS.Controllers
{
    public class DonorController : Controller
    {
        // GET: Donor
        DonorRepository DonorRepo = new DonorRepository();
        DropDown ddl = new DropDown();

        //Donor Login
        [HttpGet]
        public ActionResult DonorLogin()
        {
            if (HttpContext.User.Identity.IsAuthenticated) //check if authenticated user is available
            {
                return RedirectToAction("DonorLogin", "Donor"); //if yes redirect to EmployeeController - means user is still log in
            }
            return View();
        }

        /* [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult DonorLogin(DonorModel obj)
         {
             if (obj.Email == "keyError" && obj.Phoneno == "keyError")
             {
                 string str = "Incorrect Login Details";
                 ViewBag.Message = str;
                 ModelState.Clear();
                 *//*return View("DonorLogin");*//*
             }

             else
             {
                 if (DonorRepo.isAuthorized(obj.Email, obj.Phoneno) == true)
                 {
                     string str = "Login Successful";
                     ViewBag.Message = str;
                     Session["DonorID"] = DonorRepo.sid;


                     FormsAuthentication.SetAuthCookie(obj.Email, false);
                     return RedirectToAction("Index1", "Seeker"); //redirect to login form
                 }
             }
             return View(obj);
         }*/

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DonorLogin(DonorModel obj)
        {
            if (obj.Email == "keyError" && obj.Phoneno == "keyError")
            {
                return View("DonorLogin");
            }
            else
            {
                if (DonorRepo.isAuthorized(obj.Email, obj.Phoneno) == true)
                {
                    Session["DonorID"] = DonorRepo.did;
                    Session["Donor"] = Convert.ToString(obj.Email);
                    /* FormsAuthentication.SetAuthCookie(obj.Email, false);*/

                    if (Session["DonorID"] != null)
                    {
                        ViewBag.Message = "Login successfully";
                    }

                }
                else
                {
                    string str = "Incorrect login details!!";
                    TempData["fail"] = str;
                    return RedirectToAction("DonorLogin", "Donor");
                }
            }
            return View(obj);
        }


        public ActionResult Index2()
        {
            return View();
        }

        // Log Out
        public ActionResult LogoutDonor()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Homepage", "Home"); //redirect to index
        }


        //Add Seeker
        public ActionResult AddDonor()
        {

            DonorRepository DonorRepo = new DonorRepository();

            List<StateModel> GetStateList = ddl.GetStateList();
            ViewBag.StateName = new SelectList(GetStateList, "stateid", "statename");
            List<GenderModel> GetGender = ddl.GetGender();
            ViewBag.GenderName = new SelectList(GetGender, "genderid", "Gender");

            List<BloodGroupModel> GetBloodGroup = ddl.GetBloodGroup();
            ViewBag.BloodGroupName = new SelectList(GetBloodGroup, "bloodgroupid", "Bloodgroup");

            return View();
        }


        [HttpPost]
        public ActionResult AddDonor(DonorModel Donor)
        {

            try
            {
                DonorRepository DonorRepo = new DonorRepository();

                if (!ModelState.IsValid)
                {

                    if (DonorRepo.AddDonor(Donor))
                    {
                        ViewBag.Message = "Donor details added successfully";
                    }
                    return RedirectToAction("DonorLogin", "Donor");

                }

                return View();

            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }


        //GetAllSeeker Data
        public ActionResult GetAllDonorDetails()
        {
            ModelState.Clear();
            return View(DonorRepo.GetAllDonor());
            ModelState.Clear();
            return View(TempData["data"] = DonorRepo.GetAllDonor());

        }

        // Edit Seeker Details
        public ActionResult EditDonorDetails(DonorModel obj)
        {
            /*if (Session["DonorID"] != null)
            {*/

                List<GenderModel> GetGender = ddl.GetGender();
                ViewBag.GenderName = new SelectList(GetGender, "genderid", "gender");

                List<BloodGroupModel> GetBloodGroup = ddl.GetBloodGroup();
                ViewBag.BloodGroupName = new SelectList(GetBloodGroup, "bloodgroupid", "bloodgroup");

                ViewBag.StateName = DonorRepo.GetStateList();



                DonorModel Donor = new DonorModel();
                Donor.donorid = (int)Session["DonorID"];

                return View(DonorRepo.GetAllDonor().Find(Skr => Skr.donorid == (int)Session["DonorID"]));
            
            /*else
            {
                return RedirectToAction("DonorLogin", "Donor");
            }*/

        }

        [HttpPost]
        public ActionResult EditDonorDetails(DonorModel obj, FormCollection fc)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    DonorRepo.UpdateDonor(obj);
                    ViewBag.Message = "Donor details updated successfully";

                }
                return View();
                /*                return RedirectToAction("Index2", "Donor");
                */
            }
            catch
            {
                return View();
            }
        }


        //Add Seeker Request
        [HttpGet]
        public ActionResult AddRequest()
        {
            if (Session["DonorID"] != null)
            {

                /*                DonorRepository DonorRepo = new DonorRepository();
                */
                ViewBag.BloodGroupName = ddl.GetBloodGroup();
                ViewBag.StateName = ddl.GetStateList();

                return View();
            }
            else
            {
                return RedirectToAction("DonorLogin", "Donor");
            }
        }


        [HttpPost]
        public ActionResult AddRequest(DonorModel Donor)
        {
            DonorRepository DonorRepo = new DonorRepository();

            try
            {


                /*if (Session[Seek.Email] != null)
                {*/


                /*if (ModelState.IsValid)
                {*/

                if (DonorRepo.AddRequest(Donor))
                {
                    ViewBag.Message = "Request sent successfully";
                }
                return RedirectToAction("Index2", "Donor");

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        [HttpGet]
        public ActionResult HistoryDetails()
        {
            if (Session["DonorID"] != null)
            {

                ModelState.Clear();
                return View(TempData["data"] = DonorRepo.GetAllHistory());
            }
            else
            {
                return RedirectToAction("DonorLogin", "Donor");
            }
        }


        public ActionResult DeleteDonor(int id)
        {
            if (Session["DonorID"] != null)
            {
                DonorModel Donor = new DonorModel();
                Donor.donorid = (int)Session["DonorID"];
                DonorRepo.DeleteDonor(id);

                LogoutDonor();
                return RedirectToAction("Homepage", "Home");
            }

            else
            {
                return RedirectToAction("DonorLogin", "Donor");
            }

        }



        /*  [HttpGet]
          public ActionResult DeleteSeeker(int id)
          {
              try
              {
                  if (Session["DonorID"] != null)
                  {
                      DonorModel Seeker = new DonorModel();
                      Seeker.DonorID = (int)Session["DonorID"];

                      return View(DonorRepo.GetAllSeeker().Find(Skr => Skr.DonorID == (int)Session["DonorID"]));
                      DonorRepo.DeleteSeeker(id);

                     *//* if (DonorRepo.DeleteSeeker(id))
                      {
                          return RedirectToAction("Index1", "Seeker");
                      }

                  }
                  else
                  {
                      return RedirectToAction("DonorLogin", "Seeker");
                  }*//*
                      return View();
              }
              catch
              {
                  return View();
              }

          }*/

        [HttpPost]
        public ActionResult ConvertToPDF()
        {
            if (TempData["data"] != null)
            {
                List<DonorModel> dt = TempData["data"] as List<DonorModel>;
                var document = new iTextSharp.text.Document(PageSize.A4, 9, 9, 9, 9);
                var output = new MemoryStream();
                PdfWriter.GetInstance(document, output);
                document.Open();
                var numOfColumns = 6;
                iTextSharp.text.Font fontHeader = new iTextSharp.text.Font(null, 10, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font fontText = new iTextSharp.text.Font(null, 10, iTextSharp.text.Font.NORMAL);
                var dataTable = new PdfPTable(numOfColumns);
                dataTable.DefaultCell.Padding = 1;
                dataTable.DefaultCell.BorderWidth = 1;
                dataTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;

                /*
                                dataTable.AddCell(new PdfPCell(new Phrase("Name", fontHeader)));
                                dataTable.AddCell(new PdfPCell(new Phrase("Age", fontHeader)));
                                dataTable.AddCell(new PdfPCell(new Phrase("Gender", fontHeader)));
                                dataTable.AddCell(new PdfPCell(new Phrase("DOB", fontHeader)));
                                dataTable.AddCell(new PdfPCell(new Phrase("Email", fontHeader)));
                                dataTable.AddCell(new PdfPCell(new Phrase("Bloodgroup", fontHeader)));

                                dataTable.AddCell(new PdfPCell(new Phrase("Bloodgroup", fontHeader)));
                */


                dataTable.AddCell(new PdfPCell(new Phrase("Bloodgroup", fontHeader)));
                dataTable.AddCell(new PdfPCell(new Phrase("State", fontHeader)));
                dataTable.AddCell(new PdfPCell(new Phrase("City", fontHeader)));
                dataTable.AddCell(new PdfPCell(new Phrase("Bloodbank", fontHeader)));
                dataTable.AddCell(new PdfPCell(new Phrase("Volume(ml)", fontHeader)));
                dataTable.AddCell(new PdfPCell(new Phrase("Requestdate", fontHeader)));
                dataTable.HeaderRows = 1;
                dataTable.DefaultCell.BorderWidth = 1;
                dataTable.TotalWidth = 500f;
                dataTable.WidthPercentage = 100;
                float[] widths = new float[] { 75f, 75f, 75f, 75f, 75f, 80f };
                dataTable.SetWidths(widths);
                foreach (DonorModel report in dt)
                {
                    dataTable.AddCell(new PdfPCell(new Phrase(report.Bloodgroup, fontText)));
                    dataTable.AddCell(new PdfPCell(new Phrase(report.State, fontText)));
                    dataTable.AddCell(new PdfPCell(new Phrase(report.City, fontText)));
                    dataTable.AddCell(new PdfPCell(new Phrase(report.Bloodbank, fontText)));
                    dataTable.AddCell(new PdfPCell(new Phrase(Convert.ToString(report.Volume), fontText)));

                    string dateTime = String.Format("{0:MM/dd/yyyy}", report.Requestdate);

                    dataTable.AddCell(new PdfPCell(new Phrase(dateTime, fontText)));
                }
                document.Add(dataTable);
                document.Close();
                TempData.Keep("data");
                TempData.Peek("data");
                return File(output.ToArray(), "application/pdf", "Report" + '_' + string.Format("{0:ddMMyyyy}", (DateTime.Now)) + ".pdf");
            }
            else
                return Json(null);
        }

        // cascading dropdown city
        public JsonResult cityList(int id)
        {
            List<SelectListItem> citylist = new List<SelectListItem>(id);

            string constr = ConfigurationManager.ConnectionStrings["conn"].ToString();
            NpgsqlConnection con = new NpgsqlConnection(constr);

            con.Open();

            NpgsqlTransaction tran = con.BeginTransaction();

            string query = "select * from public.donordata('donorref'";
            query = query + ",p_d_operation:=" + 5;
            query = query + ",p_d_stateid:=" + id;
            query = query + ");";

            NpgsqlCommand com = new NpgsqlCommand(query, con);
            com.ExecuteNonQuery();

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(com);

            DataTable dt = new DataTable();

            com.CommandText = "fetch all in " + "\"donorref\";";
            com.CommandType = CommandType.Text;
            da.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                citylist.Add(new SelectListItem
                {
                    Value = Convert.ToInt32(dr["cityid"]).ToString(),
                    Text = Convert.ToString(dr["cityname"])
                });
            }

            return Json(new SelectList(citylist, "Value", "Text", JsonRequestBehavior.AllowGet));

        }

        // casacding dropdown bloodbank

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
    public class DonorController : Controller
    {

        DonorRepository DonorRepo = new DonorRepository();
        DropDown ddl = new DropDown();

        //Donor Login
        [HttpGet]
        public ActionResult DonorLogin()
        {
            return View();
        }

        [HttpPost]

        [ValidateAntiForgeryToken]
        public ActionResult DonorLogin(DonorModel obj)
        {

            string email = obj.email;
            string pno = obj.phoneno;
            Session["donor"] = Convert.ToString(obj.email);
            if (Session["donor"] == null)
            {
                return View("Login");

            }
            else
            {

                if (DonorRepo.IsValidDonor(email, pno))
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

        public ActionResult dLogout() //Log out
        {
            Session["donor"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("AdminLogin"); //redirect to Login
        }

        public ActionResult AddDonor()
        {

            List<StateModel> GetStateList = ddl.GetStateList();
            ViewBag.StateName = new SelectList(GetStateList, "stateid", "statename");

            List<GenderModel> GetGender = ddl.GetGender();
            ViewBag.GenderName = new SelectList(GetGender, "genderid", "Gender");

            List<BloodGroupModel> GetBloodGroup = ddl.GetBloodGroup();
            ViewBag.BloodGroupName = new SelectList(GetBloodGroup, "bloodgroupid", "bloodgroup");

            return View();
        }

        // POST: Camp/Create
        [HttpPost]
        public ActionResult AddDonor(FormCollection collection, DonorModel obj)
        {
            try
            {
                // TODO: Add insert logic here
                if (Session["donor"] == null)
                {
                    if (DonorRepo.AddDonor(obj))
                    {
                        ViewBag.Message = "Donor details added successfully";
                    }
                }
                else
                {
                    return RedirectToAction("DonorLogin", "Donor");
                }
                return View();

            }
            catch
            {
                return View();
            }
        }


        // Edit Donor Details
        public ActionResult EditDonorDetails(int id)
        {
            DonorModel Donor = new DonorModel();
            Donor.donorid = id;

            List<StateModel> GetStateList = ddl.GetStateList();
            ViewBag.StateName = new SelectList(GetStateList, "stateid", "statename", Donor.statename);

            List<GenderModel> GetGender = ddl.GetGender();
            ViewBag.GenderName = new SelectList(GetGender, "genderid", "gender", Donor.gender);

            List<BloodGroupModel> GetBloodGroup = ddl.GetBloodGroup();
            ViewBag.BloodGroupName = new SelectList(GetBloodGroup, "bloodgroupid", "bloodgroup", Donor.bloodgroup);


            return View(DonorRepo.GetAllDonor().Find(Dnr => Dnr.donorid == id));
        }

        [HttpPost]

        public ActionResult EditDonorDetails(int id, DonorModel obj)
        {
            try
            {
                if (Session["name"] != null)
                {

                    if (!ModelState.IsValid)
                    {
                        DonorRepo.UpdateDonor(obj);
                    }
                    return RedirectToAction("GetAllDonorDetails");
                }
                else
                {
                    return RedirectToAction("DonorLogin", "Donor");
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
                    if (DonorRepo.DeleteDonor(id))
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

    }
}
*/