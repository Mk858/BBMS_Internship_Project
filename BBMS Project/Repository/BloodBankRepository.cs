using BBMS_Project.Models;
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
    public class BloodBankRepository
    {
        public NpgsqlConnection con;
        public int bbid;
        public void Connection()
        {
            string conStr = Convert.ToString(ConfigurationManager.ConnectionStrings["conn"]);
            con = new NpgsqlConnection(conStr);
        }

        //To authorise login email and contact no. is valid or not
        public bool IsValidBldBnk(string email, string phoneno)
        {
            AdminModel obj = new AdminModel();
            Connection();
            con.Open();

            bool IsValid = false;

            string query = "select * from public.bloodbankauth(";
            query = query + "b_check:=" + 1;
            query = query + ",b_email:=" + "\'" + email + "\'";
            query = query + ",b_phoneno:=" + "\'" + phoneno + "\'";
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
                    bbid = i;
                    IsValid = true;
                }
                else
                {
                    IsValid = false;
                }
            }
            return IsValid;

        }

        //To Add BloodBAnk details    
        public bool AddBloodBank(BloodBankModel obj)
        {
            try
            {
                Connection();
                con.Open();
                NpgsqlCommand cmd = new NpgsqlCommand();
                {
                    string query = "select * from public.bloodbankauth(";
                    query = query + "b_check:=" + 2;
                    query = query + ",b_name:=" + "\'" + obj.name + "\'";
                    query = query + ",b_address:=" + "\'" + obj.address + "\'";
                    query = query + ",b_phoneno:=" + "\'" + obj.phoneno + "\'";
                    query = query + ",b_website:=" + "\'" + obj.website + "\'";
                    query = query + ",b_email:=" + "\'" + obj.email + "\'";
                    query = query + ",b_cityid:=" + obj.cityname;
                    query = query + ",b_stateid:=" + obj.statename + ");";

                  /*  query = query + ",p_b_createdby:=" + "\'" + obj.name + "\'";*/
                    /* query = query + ");fetch all in " + "\"bloodbankref\";";*/

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

        public List<BloodBankModel> GetAllBloodBank()
        {
            Connection();

            List<BloodBankModel> BldBnkList = new List<BloodBankModel>();

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

                BldBnkList.Add(

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


                    });
            }
            return BldBnkList;
        }

        public bool UpdateBloodBank(int id,BloodBankModel obj)
        {
            try
            {
                Connection();
                con.Open();
                //  var list = new List<StateList>();
                NpgsqlCommand cmd = new NpgsqlCommand();
                {
                    string query = "select * from public.spbloodbank('bloodbankref'";
                    query = query + ",p_b_operation:=" + 3;
                    query = query + ",p_b_id:=" + id;
                    query = query + ",p_b_name:=" + "\'" + obj.name + "\'";
                    query = query + ",p_b_address:=" + "\'" + obj.address + "\'";
                    query = query + ",p_b_phoneno:=" + "\'" + obj.phoneno + "\'";
                    query = query + ",p_b_website:=" + "\'" + obj.website + "\'";
                    query = query + ",p_b_email:=" + "\'" + obj.email + "\'";
                    /* query = query + ",p_b_bloodgroupid:=" + "\'" + obj.bloodgroup + "\'";*/
                    query = query + ",p_b_cityid:=" + obj.cityname;
                    query = query + ",p_b_stateid:=" + obj.statename;

                    // query = query + ",p_b_modifiedby:=" + "\'" + obj.name + "\'";
                    query = query + ");fetch all in " + "\"bloodbankref\";";


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

        public bool DeleteBloodBank(int id)
        {
            try
            {
                Connection();
                con.Open();
                string query = "select * from public.spbloodbank('bloodbankref'";
                query = query + ",p_b_operation:=" + 2;
                query = query + ",p_b_id:=" + id;


                query = query + ");fetch all in " + "\"bloodbankref\";";
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

        public List<DonorModel> getDonorRequest()
        {
            Connection();

            List<DonorModel> dnrreqlist = new List<DonorModel>();

            con.Open();
            NpgsqlTransaction tran = con.BeginTransaction();

            string query = "select * from public.donorreq('donoreqref'";
            query = query + ",d_check:=" + 7;
            query = query + ",d_bloodbankid:=" + HttpContext.Current.Session["bloodbankid"];
            query = query + ");fetch all in \"donoreqref\";";

            NpgsqlCommand com = new NpgsqlCommand(query, con);

            com.ExecuteNonQuery();

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(com);

            DataTable dt = new DataTable();

            com.CommandType = CommandType.Text;
            da.Fill(dt);



            //Bind generic list using dataRow     
            foreach (DataRow dr in dt.Rows)
            {

                dnrreqlist.Add(

                    new DonorModel
                    {
                        donorreqid = Convert.ToInt32(dr["donorrequestid"]),
                        Fullname = Convert.ToString(dr["fullname"]),
                        Bloodgroup = Convert.ToString("bloodgroup"),
                        Requestdate = Convert.ToDateTime(dr["requestdate"]),
                        Volume = Convert.ToString(dr["volume"]),
                        City = Convert.ToString(dr["cityname"]),
                        State = Convert.ToString(dr["statename"]),
                        Reqstat = Convert.ToString(dr["requeststatus"])
                    });
            }
            return dnrreqlist;
        }



        public List<SeekerModel> getSeekerRequest()
        {
            Connection();

            List<SeekerModel> skrreqlist = new List<SeekerModel>();

            con.Open();

            NpgsqlTransaction tran = con.BeginTransaction();

            string query = "select * from public.seekerreq('seekerref'";
            query = query + ",s_check:=" + 8;
            query = query + ",s_bloodbankid:=" + HttpContext.Current.Session["bloodbankid"];
            query = query + ");fetch all in \"seekerref\";";

            NpgsqlCommand com = new NpgsqlCommand(query, con);

            com.ExecuteNonQuery();

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(com);

            DataTable dt = new DataTable();

            com.CommandType = CommandType.Text;
            da.Fill(dt);

            //Bind generic list using dataRow     
            foreach (DataRow dr in dt.Rows)
            {

                skrreqlist.Add(

                    new SeekerModel
                    {
                        seekerrequestid = Convert.ToInt32(dr["seekerrequestid"]),
                        Fullname = Convert.ToString(dr["fullname"]),
                        Bloodgroup = Convert.ToString("bloodgroup"),
                        RequirementDate = Convert.ToDateTime(dr["requirementdate"]),
                        Volume = Convert.ToDecimal(dr["volume"]),
                        City = Convert.ToString(dr["cityname"]),
                        State = Convert.ToString(dr["statename"]),
                        ReqStat = Convert.ToString(dr["requeststatus"])
                    });
            }
            return skrreqlist;
        }

        public List<BloodBankModel> searchBloodBank(BloodBankModel obj)
        {
            Connection();

            List<BloodBankModel> BldBnkList = new List<BloodBankModel>();

            con.Open();

            NpgsqlTransaction tran = con.BeginTransaction();

            string query = "select * from public.spbloodbank('bloodbankref'";
            query = query + ",p_b_operation:=" + 4;
            query = query + ",p_b_stateid:=" + obj.stateid;
            query = query + ",p_b_cityid:=" + obj.cityid;
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

                BldBnkList.Add(

                    new BloodBankModel
                    {

                        bloodbankid = Convert.ToInt32(dr["bloodbankid"]),
                        name = Convert.ToString(dr["name"]),
                        address = Convert.ToString(dr["address"]),
                        phoneno = Convert.ToString(dr["phoneno"]),
                        website = Convert.ToString(dr["website"]),
                        email = Convert.ToString(dr["email"]),
                    });
            }
            return BldBnkList;
        }

    }
}