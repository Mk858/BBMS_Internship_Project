using BBMS_Project.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.UI.WebControls;

namespace BBMS_Project.Repository
{
    public class AdminRepository
    {
        public NpgsqlConnection con;
        public int sid;

        public void Connection()
        {
            string conStr = Convert.ToString(ConfigurationManager.ConnectionStrings["conn"]);
            con = new NpgsqlConnection(conStr);
        }


        // ************************************** ADMIN ******************************************************
        // Admin Login
        public bool IsValidAdmin(string email, string password)
        {
            AdminModel obj = new AdminModel();
            Connection();
            con.Open();

            bool IsValid = false;

            string query = "select * from public.adminlogin(";
            query = query + "a_email:=" + "\'" + email + "\'";
            query = query + ",a_password:=" + "\'" + password + "\'";
            query = query + ");";

            using (NpgsqlCommand cmd = new NpgsqlCommand(query, con))
            {

                NpgsqlDataAdapter sda = new NpgsqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                string i = cmd.ExecuteScalar().ToString();
                con.Close();

                if (i == "True")
                {
                    IsValid = true;
                }



            }
            return IsValid;

        }

        public bool Pichart()
        {
            string query = "SELECT reqstat, COUNT(seekerrequestid) Totalrequest";
            query += " FROM seekerrequest WHERE status = 'true' GROUP BY reqstat";
            Connection();
            con.Open();
            List<SeekerModel> chartData = new List<SeekerModel>();


            using (NpgsqlCommand cmd = new NpgsqlCommand(query, con))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
                using (NpgsqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        chartData.Add(new SeekerModel
                        {
                            ReqStat = sdr["reqstat"].ToString(),
                            seekerrequestid = Convert.ToInt32(sdr["seekerrequestid"])
                        });
                    }
                }

                con.Close();
            }


            return true;

        }

        /* public string CountDonor()
         {
             Connection();
             con.Open();

             string query = "SELECT COUNT(*) FROM Donor WHERE status = 'true'";
             DonorModel dr = new DonorModel();
             dr.Donorcount = Convert.ToInt32(query);
             return query;
         }*/

        // ************************************** Donor Request ******************************************************
        public bool ApproveDonorReq(int id, int vol)
        {
            try
            {
                Connection();
                con.Open();
                //  var list = new List<StateList>();
                NpgsqlCommand cmd = new NpgsqlCommand();
                {
                    string query = "select * from public.donorreq('donoreqref'";
                    query = query + ",d_check:=" + 2;
                    query = query + ",d_donorrequestid:=" + id;
                    query = query + ",d_volume:=" + vol;

                    query = query + ");fetch all in " + "\"donoreqref\";";

                    cmd = new NpgsqlCommand(query, con);

                    int i = cmd.ExecuteNonQuery();
                    con.Close();

                    if (i >= 1)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return false;
        }

        public bool RejectDonorReq(int id)
        {
            try
            {
                Connection();
                con.Open();
                //  var list = new List<StateList>();
                NpgsqlCommand cmd = new NpgsqlCommand();
                {
                    string query = "select * from public.donorreq('donoreqref'";
                    query = query + ",d_check:=" + 3;
                    query = query + ",d_donorrequestid:=" + id;

                    query = query + ");fetch all in " + "\"donoreqref\";";

                    cmd = new NpgsqlCommand(query, con);

                    int i = cmd.ExecuteNonQuery();
                    con.Close();

                    if (i >= 1)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return false;
        }

        // Get All Donor Request Data
        public List<DonorModel> GetAllDonorRequest()
        {
            Connection();

            List<DonorModel> DonorReqList = new List<DonorModel>();

            con.Open();

            NpgsqlTransaction tran = con.BeginTransaction();

            string query = "select * from public.donorreq('donoreqref'";
            query = query + ",d_check:=" + 1;
            query = query + ");";

            NpgsqlCommand com = new NpgsqlCommand(query, con);

            com.ExecuteNonQuery();

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(com);

            DataTable dt = new DataTable();

            com.CommandText = "fetch all in" + "\"donoreqref\";";
            com.CommandType = CommandType.Text;
            da.Fill(dt);



            //Bind EmpModel generic list using dataRow     
            foreach (DataRow dr in dt.Rows)
            {

                DonorReqList.Add(

                    new DonorModel
                    {

                        donorreqid = Convert.ToInt32(dr["donorrequestid"]),
                        Fullname = Convert.ToString(dr["fullname"]),
                        Bloodgroup = Convert.ToString(dr["bloodgroup"]),
                        Requestdate = Convert.ToDateTime(dr["requestdate"]),
                        Bloodbank = Convert.ToString(dr["name"]),
                        City = Convert.ToString(dr["cityname"]),
                        State = Convert.ToString(dr["statename"]),
                        Volume = Convert.ToString(dr["volume"]),
                        Reqstat = Convert.ToString(dr["requeststatus"])
                    });
            }

            return DonorReqList;
        }

        // Get All Approve Donor Request Data
        public List<DonorModel> GetAllApprovedonorRequest()
        {
            Connection();

            List<DonorModel> DonorReqList = new List<DonorModel>();

            con.Open();

            NpgsqlTransaction tran = con.BeginTransaction();

            string query = "select * from public.donorreq('donoreqref'";
            query = query + ",d_check:=" + 4;
            query = query + ");";

            NpgsqlCommand com = new NpgsqlCommand(query, con);

            com.ExecuteNonQuery();

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(com);

            DataTable dt = new DataTable();

            com.CommandText = "fetch all in" + "\"donoreqref\";";
            com.CommandType = CommandType.Text;
            da.Fill(dt);



            //Bind EmpModel generic list using dataRow     
            foreach (DataRow dr in dt.Rows)
            {

                DonorReqList.Add(

                    new DonorModel
                    {

                        donorreqid = Convert.ToInt32(dr["donorrequestid"]),
                        Fullname = Convert.ToString(dr["fullname"]),
                        Bloodgroup = Convert.ToString(dr["bloodgroup"]),
                        Requestdate = Convert.ToDateTime(dr["requestdate"]),
                        Bloodbank = Convert.ToString(dr["name"]),
                        City = Convert.ToString(dr["cityname"]),
                        State = Convert.ToString(dr["statename"]),
                        Volume = Convert.ToString(dr["volume"]),
                        Reqstat = Convert.ToString(dr["requeststatus"])
                    }); ;
            }

            return DonorReqList;
        }

        // Get All Reject Donor Request Data
        public List<DonorModel> GetAllRejectdonorRequest()
        {
            Connection();

            List<DonorModel> DonorReqList = new List<DonorModel>();

            con.Open();

            NpgsqlTransaction tran = con.BeginTransaction();

            string query = "select * from public.donorreq('donoreqref'";
            query = query + ",d_check:=" + 5;
            query = query + ");";

            NpgsqlCommand com = new NpgsqlCommand(query, con);

            com.ExecuteNonQuery();

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(com);

            DataTable dt = new DataTable();

            com.CommandText = "fetch all in" + "\"donoreqref\";";
            com.CommandType = CommandType.Text;
            da.Fill(dt);



            //Bind EmpModel generic list using dataRow     
            foreach (DataRow dr in dt.Rows)
            {

                DonorReqList.Add(

                    new DonorModel
                    {

                        donorreqid = Convert.ToInt32(dr["donorrequestid"]),
                        Fullname = Convert.ToString(dr["fullname"]),
                        Bloodgroup = Convert.ToString(dr["bloodgroup"]),
                        Requestdate = Convert.ToDateTime(dr["requestdate"]),
                        Bloodbank = Convert.ToString(dr["name"]),
                        City = Convert.ToString(dr["cityname"]),
                        State = Convert.ToString(dr["statename"]),
                        Volume = Convert.ToString(dr["volume"]),
                        Reqstat = Convert.ToString(dr["requeststatus"])
                    });
            }

            return DonorReqList;
        }


        // ************************************** Seeker Request ******************************************************


        // Get All Seeker Data
        public List<SeekerModel> GetAllSeekerRequest()
        {
            Connection();

            List<SeekerModel> SeekerReqList = new List<SeekerModel>();

            con.Open();

            NpgsqlTransaction tran = con.BeginTransaction();

            string query = "select * from public.seekerreq('seekerref'";
            query = query + ",s_check:=" + 2;
            query = query + ");";

            NpgsqlCommand com = new NpgsqlCommand(query, con);

            com.ExecuteNonQuery();

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(com);

            DataTable dt = new DataTable();

            com.CommandText = "fetch all in" + "\"seekerref\";";
            com.CommandType = CommandType.Text;
            da.Fill(dt);



            //Bind EmpModel generic list using dataRow     
            foreach (DataRow dr in dt.Rows)
            {

                SeekerReqList.Add(

                    new SeekerModel
                    {

                        seekerrequestid = Convert.ToInt32(dr["seekerrequestid"]),
                        Fullname = Convert.ToString(dr["fullname"]),
                        Bloodgroup = Convert.ToString(dr["bloodgroup"]),
                        RequirementDate = Convert.ToDateTime(dr["requirementdate"]),
                        Bloodbank = Convert.ToString(dr["name"]),
                        City = Convert.ToString(dr["cityname"]),
                        State = Convert.ToString(dr["statename"]),
                        Volume = Convert.ToDecimal(dr["volume"]),
                        ReqStat = Convert.ToString(dr["requeststatus"]),

                    }
               );
            }
            return SeekerReqList;
        }



        public bool ApproveSeekerReq(int id, int vol)
        {
            try
            {
                Connection();
                con.Open();
                //  var list = new List<StateList>();
                NpgsqlCommand cmd = new NpgsqlCommand();
                {
                    string query = "select * from public.seekerreq('seekerref'";
                    query = query + ",s_check:=" + 4;
                    query = query + ",s_seekerrequestid:=" + id;
                    query = query + ",s_volume:=" + vol;

                    query = query + ");fetch all in " + "\"seekerref\";";

                    cmd = new NpgsqlCommand(query, con);

                    int i = cmd.ExecuteNonQuery();
                    con.Close();

                    if (i >= 1)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return false;
        }

        public bool RejectSeekerReq(int id)
        {
            try
            {
                Connection();
                con.Open();
                //  var list = new List<StateList>();
                NpgsqlCommand cmd = new NpgsqlCommand();
                {
                    string query = "select * from public.seekerreq('seekerref'";
                    query = query + ",s_check:=" + 5;
                    query = query + ",s_seekerrequestid:=" + id;

                    query = query + ");fetch all in " + "\"seekerref\";";

                    cmd = new NpgsqlCommand(query, con);

                    int i = cmd.ExecuteNonQuery();
                    con.Close();

                    if (i >= 1)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return false;
        }

        // Get All Approve Seeker Request Data
        public List<SeekerModel> GetAllApproveSeekerRequest()
        {
            Connection();

            List<SeekerModel> SeekerReqList = new List<SeekerModel>();

            con.Open();

            NpgsqlTransaction tran = con.BeginTransaction();

            string query = "select * from public.seekerreq('seekerref'";
            query = query + ",s_check:=" + 6;
            query = query + ");";

            NpgsqlCommand com = new NpgsqlCommand(query, con);

            com.ExecuteNonQuery();

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(com);

            DataTable dt = new DataTable();

            com.CommandText = "fetch all in" + "\"seekerref\";";
            com.CommandType = CommandType.Text;
            da.Fill(dt);



            //Bind EmpModel generic list using dataRow     
            foreach (DataRow dr in dt.Rows)
            {

                SeekerReqList.Add(

                    new SeekerModel
                    {

                        seekerrequestid = Convert.ToInt32(dr["seekerrequestid"]),
                        Fullname = Convert.ToString(dr["fullname"]),
                        Bloodgroup = Convert.ToString(dr["bloodgroup"]),
                        RequirementDate = Convert.ToDateTime(dr["requirementdate"]),
                        Bloodbank = Convert.ToString(dr["name"]),
                        City = Convert.ToString(dr["cityname"]),
                        State = Convert.ToString(dr["statename"]),
                        Volume = Convert.ToDecimal(dr["volume"]),
                        ReqStat = Convert.ToString(dr["requeststatus"])
                    });
            }

            return SeekerReqList;
        }

        // Get All Reject Seeker Request Data
        public List<SeekerModel> GetAllRejectSeekerRequest()
        {
            Connection();

            List<SeekerModel> SeekerReqList = new List<SeekerModel>();

            con.Open();

            NpgsqlTransaction tran = con.BeginTransaction();

            string query = "select * from public.seekerreq('seekerref'";
            query = query + ",s_check:=" + 7;
            query = query + ");";

            NpgsqlCommand com = new NpgsqlCommand(query, con);

            com.ExecuteNonQuery();

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(com);

            DataTable dt = new DataTable();

            com.CommandText = "fetch all in" + "\"seekerref\";";
            com.CommandType = CommandType.Text;
            da.Fill(dt);



            //Bind EmpModel generic list using dataRow     
            foreach (DataRow dr in dt.Rows)
            {

                SeekerReqList.Add(

                    new SeekerModel
                    {

                        seekerrequestid = Convert.ToInt32(dr["seekerrequestid"]),
                        Fullname = Convert.ToString(dr["fullname"]),
                        Bloodgroup = Convert.ToString(dr["bloodgroup"]),
                        RequirementDate = Convert.ToDateTime(dr["requirementdate"]),
                        Bloodbank = Convert.ToString(dr["name"]),
                        City = Convert.ToString(dr["cityname"]),
                        State = Convert.ToString(dr["statename"]),
                        Volume = Convert.ToDecimal(dr["volume"]),
                        ReqStat = Convert.ToString(dr["requeststatus"])
                    });
            }

            return SeekerReqList;
        }


        // *************************************** DONOR ******************************************************

        // Get All Donor Data
        public List<DonorModel> GetAllDonor()
        {
            Connection();

            List<DonorModel> DonorList = new List<DonorModel>();

            con.Open();

            NpgsqlTransaction tran = con.BeginTransaction();

            string query = "select * from public.DonorData('donorref'";
            query = query + ",p_d_operation:=" + 1;
            query = query + ");";


            NpgsqlCommand com = new NpgsqlCommand(query, con);

            com.ExecuteNonQuery();

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(com);

            DataTable dt = new DataTable();

            com.CommandText = "fetch all in" + "\"donorref\";";
            com.CommandType = CommandType.Text;
            da.Fill(dt);

            //Bind EmpModel generic list using dataRow     
            foreach (DataRow dr in dt.Rows)
            {

                DonorList.Add(

                    new DonorModel
                    {

                        donorid = Convert.ToInt32(dr["donorid"]),
                        Fullname = Convert.ToString(dr["fullname"]),
                        Gender = Convert.ToString(dr["gender"]),
                        Age = Convert.ToInt32(dr["age"]),
                        Birthdate = Convert.ToDateTime(dr["birthdate"]),
                        Bloodgroup = Convert.ToString(dr["bloodgroup"]),
                        Address = Convert.ToString(dr["address"]),
                        Pincode = Convert.ToString(dr["pincode"]),
                        City = Convert.ToString(dr["cityname"]),
                        State = Convert.ToString(dr["statename"]),
                        Phoneno = Convert.ToString(dr["phoneno"]),
                        Pancardno = Convert.ToString(dr["pancardno"]),
                        Email = Convert.ToString(dr["email"]),
                        /* password = Convert.ToString(dr["password"]),*/

                    }
                    );
            }



            return DonorList;
        }

        //*************************************** SEEKER ****************************************************** 

        public List<SeekerModel> GetAllSeeker()
        {
            Connection();

            List<SeekerModel> SeekerList = new List<SeekerModel>();

            con.Open();

            NpgsqlTransaction tran = con.BeginTransaction();

            string query = "select * from public.seekerdata('seekerref'";
            query = query + ",p_s_operation:=" + 1;
            query = query + ");";

            NpgsqlCommand com = new NpgsqlCommand(query, con);

            com.ExecuteNonQuery();

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(com);

            DataTable dt = new DataTable();

            com.CommandText = "fetch all in" + "\"seekerref\";";
            com.CommandType = CommandType.Text;
            da.Fill(dt);



            //Bind EmpModel generic list using dataRow     
            foreach (DataRow dr in dt.Rows)
            {

                SeekerList.Add(

                  new SeekerModel
                  {

                      seekerid = Convert.ToInt16(dr["seekerid"]),
                      Fullname = Convert.ToString(dr["fullname"]),
                      Gender = Convert.ToString(dr["gender"]),
                      Age = Convert.ToInt32(dr["age"]),
                      Birthdate = Convert.ToDateTime(dr["birthdate"]),
                      Bloodgroup = Convert.ToString(dr["bloodgroup"]),
                      Address = Convert.ToString(dr["address"]),
                      Pincode = Convert.ToString(dr["pincode"]),
                      City = Convert.ToString(dr["cityname"]),
                      State = Convert.ToString(dr["statename"]),
                      Phoneno = Convert.ToString(dr["phoneno"]),
                      Email = Convert.ToString(dr["email"]),



                  }
                  );


            }

            return SeekerList;
        }

        // *************************************** BloodBank ******************************************************


        public List<BloodBankModel> GetAllBloodBank()
        {
            Connection();

            List<BloodBankModel> SeekerList = new List<BloodBankModel>();

            con.Open();

            NpgsqlTransaction tran = con.BeginTransaction();

            string query = "select * from public.spbloodbank('bloodbankref'";
            query = query + ",p_b_operation:=" + 1;
            query = query + ");";

            NpgsqlCommand com = new NpgsqlCommand(query, con);

            com.ExecuteNonQuery();

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(com);

            DataTable dt = new DataTable();

            com.CommandText = "fetch all in" + "\"bloodbankref\";";
            com.CommandType = CommandType.Text;
            da.Fill(dt);



            //Bind EmpModel generic list using dataRow     
            foreach (DataRow dr in dt.Rows)
            {

                SeekerList.Add(

                    new BloodBankModel
                    {

                        bloodbankid = Convert.ToInt32(dr["bloodbankid"]),
                        name = Convert.ToString(dr["name"]),
                        address = Convert.ToString(dr["address"]),
                        phoneno = Convert.ToString(dr["phoneno"]),
                        website = Convert.ToString(dr["website"]),
                        email = Convert.ToString(dr["email"]),
                        cityname = Convert.ToString(dr["cityname"]),
                        statename = Convert.ToString(dr["statename"]),
                        status = Convert.ToString(dr["status"]),

                        /* createdby = Convert.ToString(dr["createdby"]),
                         createdon = Convert.ToDateTime(dr["createdon"]),
                         modifiedby = Convert.ToString(dr["modifiedby"]),
                         modifiedon = Convert.ToDateTime(dr["modifiedon"]),
                         status = Convert.ToBoolean(dr["status"]),*/


                    }
                    );


            }

            return SeekerList;
        }

   

        //***************************************  Camp Schedule & Registration  *************************************** 

       
        //To view All Camp details with generic list
        public List<Camp> GetAllCampDetails()
        {
            Connection();
            con.Open();

            List<Camp> CampList = new List<Camp>();

            NpgsqlTransaction tran = con.BeginTransaction();

            string mainQuery = "select * from public.spcamp('campref'";
            mainQuery = mainQuery + ",p_operation:=" + 6;
            mainQuery = mainQuery + ");";

            NpgsqlCommand com = new NpgsqlCommand(mainQuery, con);

            com.ExecuteNonQuery();

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(com);

            DataTable dt = new DataTable();
            com.CommandText = "fetch all in" + "\"campref\";";
            com.CommandType = CommandType.Text;
            da.Fill(dt);



            //Bind Camp generic list using dataRow     
            foreach (DataRow dr in dt.Rows)
            {

                CampList.Add(

                    new Camp
                    {
                        campid = Convert.ToInt32(dr["campid"]),
                        name = Convert.ToString(dr["name"]),
                        address = Convert.ToString(dr["address"]),
                        state = Convert.ToString(dr["statename"]),
                        city = Convert.ToString(dr["cityname"]),
                        contactno = Convert.ToString(dr["contactno"]),
                        conductedby = Convert.ToString(dr["conductedby"]),
                        organizedby = Convert.ToString(dr["organizedby"]),
                        schedstart = Convert.ToDateTime(dr["schedstart"].ToString()),
                        schedend = Convert.ToDateTime(dr["schedend"].ToString())
                    }
                );
            }

            /*  con.Dispose();
              con.Close();
              */
            return CampList;
        }

       
    }


}