using BBMS_Project.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace BBMS_Project.Repository
{
    public class SeekerRepository
    {
        public NpgsqlConnection con;
        public int sid;

        // Connection
        public void Connection()
        {
            string constr = Convert.ToString(ConfigurationManager.ConnectionStrings["conn"]);
            con = new NpgsqlConnection(constr);
        }
        // Seeker Login
        public bool isAuthorized(string email, string phoneno)
        {
            Connection();
            con.Open();
            SeekerModel obj = new SeekerModel();
            string query = "select * from public.seekerlogin(";
            query = query + "s_email:=" + "\'" + email + "\'";
            query = query + ",s_phoneno:=" + "\'" + phoneno + "\'";
            query = query + ");";

            NpgsqlCommand cmd = new NpgsqlCommand(query, con);

            var res = (int)cmd.ExecuteScalar();

            if (res > 0)
            {
                sid = res;
                return true;
            }

            con.Close();
            con.Dispose();
            return false;
        }

        //Add Seeker
        public bool AddSeeker(SeekerModel obj)
        {
            try
            {
                Connection();
                con.Open();

                NpgsqlCommand cmd = new NpgsqlCommand();
                {
                    string query = "select * from public.seekerreg('seekerref'";
                    query = query + ",s_check:=" + 1;
                    query = query + ",s_fullname:=" + "\'" + obj.Fullname + "\'";
                    query = query + ",s_genderid:=" + obj.Gender;
                    query = query + ",s_age:=" + obj.Age;
                    query = query + ",s_birthdate:=" + "\'" + obj.Birthdate.ToString("yyyy-MM-dd") + "\'";
                    query = query + ",s_bloodgroupid:=" + obj.Bloodgroup;
                    query = query + ",s_address:=" + "\'" + obj.Address + "\'";
                    query = query + ",s_pincode:=" + "\'" + obj.Pincode + "\'";
                    query = query + ",s_cityid:=" + obj.City;

                    query = query + ",s_stateid:=" + obj.State;
                    query = query + ",s_phoneno:=" + "\'" + obj.Phoneno + "\'";
                    query = query + ",s_email:=" + "\'" + obj.Email + "\'";
                    query = query + ",s_createdby:=" + "\'" + obj.Fullname + "\'";

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


        // Seeker Request
        public bool AddRequest(SeekerModel obj)
        {
            try
            {
                Connection();
                con.Open();

                NpgsqlCommand cmd = new NpgsqlCommand();
                {
                    string query = "select * from public.seekerreq('seekerref'";
                    query = query + ",s_check:=" + 1;
                    query = query + ",s_seekerid:=" + HttpContext.Current.Session["SeekerID"];
                    query = query + ",s_bloodgroupid:=" + obj.Bloodgroup;
                    query = query + ",s_stateid:=" + obj.State;
                    query = query + ",s_cityid:=" + obj.City;
                    query = query + ",s_bloodbankid:=" + obj.Bloodbank;
                    query = query + ",s_requirementdate:=" + "\'" + obj.RequirementDate.ToString("yyyy-MM-dd") + "\'";
                    query = query + ",s_volume:=" + obj.Volume;
                    //query = query + ",s_createdby:=" + "\'" + obj.Fullname + "\'";

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

        // Get Seeker Details
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
                      genderid = Convert.ToInt32(dr["genderid"]),
                      Age = Convert.ToInt32(dr["age"]),
                      Birthdate = Convert.ToDateTime(dr["birthdate"]),
                      Bloodgroup = Convert.ToString(dr["bloodgroup"]),
                      bloodgroupid = Convert.ToInt32(dr["bloodgroupid"]),
                      Address = Convert.ToString(dr["address"]),
                      Pincode = Convert.ToString(dr["pincode"]),
                      City = Convert.ToString(dr["cityname"]),
                      cityid = Convert.ToInt32(dr["cityid"]),
                      State = Convert.ToString(dr["statename"]),
                      stateid = Convert.ToInt32(dr["stateid"]),
                      Phoneno = Convert.ToString(dr["phoneno"]),
                      Email = Convert.ToString(dr["email"]),
                      /*createdby = Convert.ToString(dr["createdby"]),
                      createdon = Convert.ToDateTime(dr["createdon"]),
                      modifiedby = Convert.ToString(dr["modifiedby"]),
                      modifiedon = Convert.ToDateTime(dr["modifiedon"]),
                      status = Convert.ToBoolean(dr["status"]),*/


                  }
                  );


            }

            return SeekerList;
        }

        //Update Seeker
        public bool UpdateSeeker(SeekerModel obj)
        {
            try
            {
                Connection();
                con.Open();
                //  var list = new List<StateList>();
                NpgsqlCommand cmd = new NpgsqlCommand();
                {
                    string query = "select * from public.seekerdata('seekerref'";
                    query = query + ",p_s_operation:=" + 3;
                    query = query + ",p_s_id:=" + obj.seekerid + HttpContext.Current.Session["SeekerID"];
                    query = query + ",p_s_name:=" + "\'" + obj.Fullname + "\'";
                    query = query + ",p_s_genderid:=" + "\'" + obj.genderid + "\'";
                    query = query + ",p_s_age:=" + "\'" + obj.Age + "\'";
                    query = query + ",p_s_birthdate:=" + "\'" + obj.Birthdate + "\'";
                    query = query + ",p_s_bloodgroupid:=" + "\'" + obj.bloodgroupid + "\'";
                    query = query + ",p_s_address:=" + "\'" + obj.Address + "\'";
                    query = query + ",p_s_pincode:=" + "\'" + obj.Pincode + "\'";
                    query = query + ",p_s_cityid:=" + "\'" + obj.cityid + "\'";
                    query = query + ",p_s_stateid:=" + "\'" + obj.stateid + "\'";
                    query = query + ",p_s_phoneno:=" + "\'" + obj.Phoneno + "\'";
                    query = query + ",p_s_email:=" + "\'" + obj.Email + "\'";
                    query = query + ",p_s_modified:=" + "\'" + obj.Fullname + "\'";

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


        public bool DeleteSeeker(int id)
        {
            try
            {
                Connection();
                con.Open();
                string query = "select * from public.seekerdata('seekerref'";
                query = query + ",p_s_operation:=" + 2;
                query = query + ",p_s_id:=" +  id;


                query = query + ");fetch all in " + "\"seekerref\";";
                NpgsqlCommand com = new NpgsqlCommand(query, con);

                com.CommandType = CommandType.Text;


                int i = com.ExecuteNonQuery();
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
            catch (Exception ex) { Console.WriteLine(ex.Message); return false; }
        }


        public bool DeleteAdminSeeker(int id)
        {
            try
            {
                Connection();
                con.Open();
                string query = "select * from public.seekerdata('seekerref'";
                query = query + ",p_s_operation:=" + 2;
                query = query + ",p_s_id:=" + id;


                query = query + ");fetch all in " + "\"seekerref\";";
                NpgsqlCommand com = new NpgsqlCommand(query, con);

                com.CommandType = CommandType.Text;


                int i = com.ExecuteNonQuery();
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
            catch (Exception ex) { Console.WriteLine(ex.Message); return false; }
        }

        /* public bool DeleteSeeker(SeekerModel obj)
         {
             try
             {
                 Connection();
                 con.Open();
                 string query = "select * from public.seekerdata('seekerref'";
                 query = query + ",p_s_operation:=" + 2;
                 query = query + ",p_s_id:=" + obj.seekerid  + HttpContext.Current.Session["SeekerID"];


                 query = query + ");fetch all in " + "\"seekerref\";";
                 NpgsqlCommand com = new NpgsqlCommand(query, con);

                 com.CommandType = CommandType.Text;


                 int i = com.ExecuteNonQuery();
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
             catch (Exception ex) { Console.WriteLine(ex.Message); return false; }
         }*/


        // Get History
        public List<SeekerModel> GetAllHistory()
        {
            Connection();

            List<SeekerModel> HistoryList = new List<SeekerModel>();

            con.Open();

            NpgsqlTransaction tran = con.BeginTransaction();

            string query = "select * from public.seekerreq('seekerref'";
            query = query + ",s_check:=" + 3;
            query = query + ",s_seekerid:=" + HttpContext.Current.Session["SeekerID"];
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

                HistoryList.Add(

                  new SeekerModel
                  {

                      Bloodgroup = Convert.ToString(dr["bloodgroup"]),
                      State = Convert.ToString(dr["statename"]),
                      City = Convert.ToString(dr["cityname"]),
                      Bloodbank = Convert.ToString(dr["name"]),
                      Volume = Convert.ToInt32(dr["volume"]),
                      RequirementDate = Convert.ToDateTime(dr["requirementdate"]),
                      Createdon = Convert.ToDateTime(dr["createdon"]),

                  }
                  );
            }

            return HistoryList;
        }


        //State list
        public List<SeekerModel> GetStateList()
        {
            Connection();
            con.Open();

            NpgsqlTransaction tran = con.BeginTransaction();

            NpgsqlCommand com = new NpgsqlCommand("select * from state", con);
            com.ExecuteNonQuery();

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(com);

            DataTable dt = new DataTable();

            da.Fill(dt);

            List<SeekerModel> stateList = new List<SeekerModel>();

            //Bind EmpModel generic list using dataRow    
            foreach (DataRow dr in dt.Rows)
            {

                stateList.Add(

                    new SeekerModel
                    {
                        stateid = Convert.ToInt32(dr["stateid"]),
                        State = Convert.ToString(dr["statename"])
                    }
                );
            }
            con.Dispose();
            con.Close();
            return stateList;
        }

    }
}