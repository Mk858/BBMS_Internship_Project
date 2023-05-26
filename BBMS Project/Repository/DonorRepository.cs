using BBMS_Project.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BBMS.Repository
{
    public class DonorRepository
    {
        public NpgsqlConnection con;
        public int did;

        // Connection
        public void Connection()
        {
            string constr = Convert.ToString(ConfigurationManager.ConnectionStrings["conn"]);
            con = new NpgsqlConnection(constr);
        }
        // Donor Login
        public bool isAuthorized(string email, string phoneno)
        {
            Connection();
            con.Open();
            DonorModel obj = new DonorModel();
            string query = "select * from public.donorauth(";
            query = query + "d_email:=" + "\'" + email + "\'";
            query = query + ",d_phoneno:=" + "\'" + phoneno + "\'";
            query = query + ");";

            NpgsqlCommand cmd = new NpgsqlCommand(query, con);

            var res = (int)cmd.ExecuteScalar();

            /*if (res > 0)
            {
                did = res;
                return true;
            }
            else
            {
                did = 0;
                return false;
            }
            con.Close();
            con.Dispose();
            return false;*/
            if (res > 0)
            {
                did = res;
                return true;
            }

            con.Close();
            con.Dispose();
            return false;
        }

        //Add Seeker
        public bool AddDonor(DonorModel obj)
        {
            try
            {
                Connection();
                con.Open();

                NpgsqlCommand cmd = new NpgsqlCommand();
                {
                    string query = "select * from public.donordata('donorref'";
                    query = query + ",p_d_operation:=" + 8;
                    query = query + ",p_d_name:=" + "\'" + obj.Fullname + "\'";
                    query = query + ",p_d_genderid:=" + obj.Gender;
                    query = query + ",p_d_age:=" + obj.Age;
                    query = query + ",p_d_birthdate:=" + "\'" + obj.Birthdate/*.ToString("yyyy-MM-dd")*/ + "\'";
                    query = query + ",p_d_bloodgroupid:=" + obj.Bloodgroup;
                    query = query + ",p_d_address:=" + "\'" + obj.Address + "\'";
                    query = query + ",p_d_pincode:=" + "\'" + obj.Pincode + "\'";
                    query = query + ",p_d_cityid:=" + obj.City;

                    query = query + ",p_d_stateid:=" + obj.State;
                    query = query + ",p_d_phoneno:=" + "\'" + obj.Phoneno + "\'";
                   /* query = query + ",p_d_pancardno:=" + "\'" + obj.Pancardno + "\'";*/
                    /*                    query = query + ",p_d_password:=" + "\'" + obj.Password + "\'";
                    */
                    query = query + ",p_d_email:=" + "\'" + obj.Email + "\'";
                   /* query = query + ",s_createdby:=" + "\'" + obj.Fullname + "\'";*/

                    query = query + ");fetch all in " + "\"donorref\";";

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
        public bool AddRequest(DonorModel obj)
        {
            try
            {
                Connection();
                con.Open();

                NpgsqlCommand cmd = new NpgsqlCommand();
                {
                    string query = "select * from public.donorreq('donoreqref'";
                    query = query + ",d_check:=" + 8;
                    query = query + ",d_donorid:=" + HttpContext.Current.Session["DonorID"];
                   /* query = query + ",d_donorrequestid:=" + obj.donorreqid;*/
                    query = query + ",d_bloodgroupid:=" + obj.Bloodgroup;
                    query = query + ",d_stateid:=" + obj.State;
                    query = query + ",d_cityid:=" + obj.City;
                    query = query + ",d_bloodbankid:=" + obj.Bloodbank;
                    query = query + ",d_requestdate:=" + "\'" + obj.Requestdate/*ToString("yyyy-MM-dd")*/ + "\'";
                    query = query + ",d_volume:=" + obj.Volume;
                    //query = query + ",s_createdby:=" + "\'" + obj.Fullname + "\'";

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

        // Get Donor Details
        public List<DonorModel> GetAllDonor()
        {
            Connection();

            List<DonorModel> DonorList = new List<DonorModel>();

            con.Open();

            NpgsqlTransaction tran = con.BeginTransaction();

            string query = "select * from public.donordata('donorref'";
            query = query + ",p_d_operation:=" + 1;
            query = query + ");";

            NpgsqlCommand com = new NpgsqlCommand(query, con);

            com.ExecuteNonQuery();

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(com);

            DataTable dt = new DataTable();

            com.CommandText = "fetch all in" + "\"donorref\";";
            com.CommandType = CommandType.Text;
            da.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {

                DonorList.Add(

                  new DonorModel
                  {
                      donorid = Convert.ToInt16(dr["donorid"]),
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
                      /*                      password = Convert.ToString(dr["password"])
                      */
                      /*createdby = Convert.ToString(dr["createdby"]),
                      createdon = Convert.ToDateTime(dr["createdon"]),
                      modifiedby = Convert.ToString(dr["modifiedby"]),
                      modifiedon = Convert.ToDateTime(dr["modifiedon"]),
                      status = Convert.ToBoolean(dr["status"]),*/


                  }
                  );


            }

            return DonorList;
        }

        //Update Donor
        public bool UpdateDonor(DonorModel obj)
        {
            try
            {
                Connection();
                con.Open();
                //  var list = new List<StateList>();
                NpgsqlCommand cmd = new NpgsqlCommand();
                {
                    string query = "select * from public.donordata('donorref'";
                    query = query + ",p_d_operation:=" + 3;

                    query = query + ",p_d_id:=" + +obj.donorid + HttpContext.Current.Session["DonorID"] ;
                    query = query + ",p_d_name:=" + "\'" + obj.Fullname + "\'";
                    query = query + ",p_d_genderid:=" + obj.genderid;
                    query = query + ",p_d_age:=" + obj.Age;
                    query = query + ",p_d_birthdate:=" + "\'" + obj.Birthdate/*ToString("yyyy-MM-dd")*/ + "\'";
                    query = query + ",p_d_bloodgroupid:=" + obj.bloodgroupid;
                    query = query + ",p_d_address:=" + "\'" + obj.Address + "\'";
                    query = query + ",p_d_pincode:=" + "\'" + obj.Pincode + "\'";
                    query = query + ",p_d_cityid:=" + obj.cityid;

                    query = query + ",p_d_stateid:=" + obj.stateid;
                    query = query + ",p_d_phoneno:=" + "\'" + obj.Phoneno + "\'";
/*                    query = query + ",p_d_pancardno:=" + "\'" + obj.Pancardno + "\'";
*/                    /*                    query = query + ",p_d_password:=" + "\'" + obj.password + "\'";
                    */
                    query = query + ",p_d_email:=" + "\'" + obj.Email + "\'";
                    /*query = query + ",p_d_modified:=" + "\'" + obj.Fullname + "\'";*/

                    query = query + ");fetch all in " + "\"donorref\";";

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



        public bool DeleteAdminDonor(int id)
        {
            try
            {
                Connection();
                con.Open();
                string query = "select * from public.donordata('donorref'";
                query = query + ",p_d_operation:=" + 2;
                query = query + ",p_d_id:=" + id;


                query = query + ");fetch all in " + "\"donorref\";";
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


        public bool DeleteDonor(int id)
        {
            try
            {
                Connection();
                con.Open();
                string query = "select * from public.donordata('donorref'";
                query = query + ",p_d_operation:=" + 2;
                query = query + ",p_d_id:="  /*HttpContext.Current.Session["DonorID"]*/ + id;


                query = query + ");fetch all in " + "\"donorref\";";
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
        // Get History
        public List<DonorModel> GetAllHistory()
        {
            Connection();

            List<DonorModel> HistoryList = new List<DonorModel>();

            con.Open();

            NpgsqlTransaction tran = con.BeginTransaction();

            string query = "select * from public.donorreq('donoreqref'";
            query = query + ",d_check:=" + 6;
            query = query + ",d_donorid:=" + HttpContext.Current.Session["DonorID"];
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

                HistoryList.Add(

                  new DonorModel
                  {

                      Bloodgroup = Convert.ToString(dr["bloodgroup"]),
                      State = Convert.ToString(dr["statename"]),
                      City = Convert.ToString(dr["cityname"]),
                      Bloodbank = Convert.ToString(dr["name"]),
                      Volume = Convert.ToString(dr["volume"]),
                      Requestdate = Convert.ToDateTime(dr["requestdate"]),

                  }
                  );
            }

            return HistoryList;
        }


        //State list
        public List<DonorModel> GetStateList()
        {
            Connection();
            con.Open();

            NpgsqlTransaction tran = con.BeginTransaction();

            NpgsqlCommand com = new NpgsqlCommand("select * from state", con);
            com.ExecuteNonQuery();

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(com);

            DataTable dt = new DataTable();

            da.Fill(dt);

            List<DonorModel> stateList = new List<DonorModel>();

            //Bind EmpModel generic list using dataRow    
            foreach (DataRow dr in dt.Rows)
            {

                stateList.Add(

                    new DonorModel
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


        // Get gender list
        public List<DonorModel> GetGender()
        {
            Connection();
            con.Open();

            NpgsqlTransaction tran = con.BeginTransaction();



            NpgsqlCommand com = new NpgsqlCommand("select genderid, gender from gender", con);
            com.ExecuteNonQuery();

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(com);

            DataTable dt = new DataTable();


            da.Fill(dt);

            List<DonorModel> GenderList = new List<DonorModel>();

            //Bind EmpModel generic list using dataRow    
            foreach (DataRow dr in dt.Rows)
            {

                GenderList.Add(

                    new DonorModel
                    {
                        genderid = Convert.ToInt32(dr["genderid"]),
                        Gender = Convert.ToString(dr["gender"])
                    }
                );
            }

            con.Close();
            return GenderList;
        }

        // get bloodgroup list
        public List<DonorModel> GetBloodGroup()
        {
            Connection();
            con.Open();

            NpgsqlTransaction tran = con.BeginTransaction();

            string query = "select * from public.donordata('donorref'";
            query = query + ",p_d_operation:=" + 7;
            query = query + ");";

            NpgsqlCommand com = new NpgsqlCommand(query, con);
            com.ExecuteNonQuery();

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(com);

            DataTable dt = new DataTable();

            com.CommandText = "fetch all in " + "\"donorref\";";
            com.CommandType = CommandType.Text;
            da.Fill(dt);

            List<DonorModel> BloodGroupList = new List<DonorModel>();

            //Bind EmpModel generic list using dataRow    
            foreach (DataRow dr in dt.Rows)
            {

                BloodGroupList.Add(

                    new DonorModel
                    {
                        bloodgroupid = Convert.ToInt32(dr["bloodgroupid"]),
                        Bloodgroup = Convert.ToString(dr["bloodgroup"])
                    }
                );
            }

            con.Close();
            return BloodGroupList;
        }

    }
}





/*using BBMS_Project.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;

namespace BBMS_Project.Repository
{
    public class DonorRepository
    {

        public NpgsqlConnection con;
        public int sid;

        public void Connection()
        {
            string conStr = Convert.ToString(ConfigurationManager.ConnectionStrings["conn"]);
            con = new NpgsqlConnection(conStr);
        }
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
                        fullname = Convert.ToString(dr["fullname"]),
                        gender = Convert.ToString(dr["gender"]),
                        age = Convert.ToInt32(dr["age"]),
                        birthdate = Convert.ToDateTime(dr["birthdate"]),
                        bloodgroup = Convert.ToString(dr["bloodgroup"]),
                        address = Convert.ToString(dr["address"]),
                        pincode = Convert.ToString(dr["pincode"]),
                        cityname = Convert.ToString(dr["cityname"]),
                        statename = Convert.ToString(dr["statename"]),
                        phoneno = Convert.ToString(dr["phoneno"]),
                        pancardno = Convert.ToString(dr["pancardno"]),
                        email = Convert.ToString(dr["email"]),
                        *//* password = Convert.ToString(dr["password"]),*//*

                    }
                    );
            }



            return DonorList;
        }

        public bool IsValidDonor(string email, string phoneno)
        {
            AdminModel obj = new AdminModel();
            Connection();
            con.Open();

            bool IsValid = false;

            string query = "select * from public.donorauth(";
            query = query + "d_check:=" + 1;
            query = query + ",d_email:=" + "\'" + email + "\'";
            query = query + ",d_phoneno:=" + "\'" + phoneno + "\'";
            query = query + ");";

            using (NpgsqlCommand cmd = new NpgsqlCommand(query, con))
            {

                NpgsqlDataAdapter sda = new NpgsqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                int i = (int)cmd.ExecuteScalar();
                con.Close();

                if (i > 0)
                {
                    IsValid = true;
                }
            }
            return IsValid;

        }

        //Add Donor
        public bool AddDonor(DonorModel obj)
        {
            try
            {
                Connection();
                con.Open();

                NpgsqlCommand cmd = new NpgsqlCommand();
                {
                    string query = "select * from public.donorauth(";
                    query = query + "d_check:=" + 2;
                    query = query + ",d_fullname:=" + "\'" + obj.fullname + "\'";
                    query = query + ",d_genderid:=" + obj.gender;
                    query = query + ",d_age:=" + obj.age;
                    query = query + ",d_birthdate:=" + "\'" + obj.birthdate + "\'";
                    query = query + ",d_bloodgroupid:=" + obj.bloodgroup;
                    query = query + ",d_address:=" + "\'" + obj.address + "\'";
                    query = query + ",d_pincode:=" + "\'" + obj.pincode + "\'";
                    query = query + ",d_cityid:=" + obj.cityname;
                    query = query + ",d_stateid:=" + obj.statename;
                    query = query + ",d_phoneno:=" + "\'" + obj.phoneno + "\'";
                    query = query + ",d_pancardno:=" + "\'" + obj.pancardno + "\'";
                    query = query + ",d_email:=" + "\'" + obj.email + "\'";
                    query = query + ");";

                    cmd = new NpgsqlCommand(query, con);

                    int i = (int)cmd.ExecuteScalar();
                    con.Close();

                    if (i == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return false;
        }

        public bool DeleteDonor(int id)
        {
            try
            {
                Connection();
                con.Open();
                string query = "select * from public.DonorData('DonorRef'";
                query = query + ",p_d_operation:=" + 2;
                query = query + ",p_d_id:=" + id;


                query = query + ");fetch all in " + "\"DonorRef\";";
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

        public bool UpdateDonor(DonorModel obj)
        {
            try
            {
                Connection();
                con.Open();
                //  var list = new List<StateList>();
                NpgsqlCommand cmd = new NpgsqlCommand();
                {
                    string query = "select * from public.DonorData('DonorRef'";
                    query = query + ",p_d_operation:=" + 3;
                    query = query + ",p_d_id:=" + obj.donorid;
                    query = query + ",p_d_name:=" + "\'" + obj.fullname + "\'";
                    query = query + ",p_d_genderid:=" + "\'" + obj.gender + "\'";
                    query = query + ",p_d_age:=" + "\'" + obj.age + "\'";
                    query = query + ",p_d_birthdate:=" + "\'" + obj.birthdate + "\'";
                    query = query + ",p_d_bloodgroupid:=" + "\'" + obj.bloodgroup + "\'";
                    query = query + ",p_d_address:=" + "\'" + obj.address + "\'";
                    query = query + ",p_d_pincode:=" + "\'" + obj.pincode + "\'";
                    query = query + ",p_d_cityid:=" + "\'" + obj.cityname + "\'";
                    query = query + ",p_d_stateid:=" + "\'" + obj.statename + "\'";
                    query = query + ",p_d_phoneno:=" + "\'" + obj.phoneno + "\'";
                    query = query + ",p_d_pancardno:=" + "\'" + obj.pancardno + "\'";
                    query = query + ",p_d_email:=" + "\'" + obj.email + "\'";
                    *//*query = query + ",p_d_password:=" + "\'" + obj.password + "\'";*//*


                    query = query + ");fetch all in " + "\"DonorRef\";";

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
    }
}*/