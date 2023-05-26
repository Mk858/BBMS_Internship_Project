using BBMS_Project.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;

namespace BBMS_Project.Repository
{
    public class BloodStockRepository
    {
        public NpgsqlConnection con;

        public void Connection()
        {
            string conStr = Convert.ToString(ConfigurationManager.ConnectionStrings["conn"]);
            con = new NpgsqlConnection(conStr);
        }

        //Get List of Blood stock of perticular Blood Bank
        public List<BloodStockModel> getBloodStock()
        {
            Connection();

            List<BloodStockModel> BldstckList = new List<BloodStockModel>();

            con.Open();

            NpgsqlTransaction tran = con.BeginTransaction();

            string query = "select * from public.spbloodstock('stockref'";
            query = query + ",p_operation:=" + 1;
           /* query = query + ",p_bloodbankid:= "  + HttpContext.Current.Session["bloodbankid"];*/
            query = query + ");";

            NpgsqlCommand com = new NpgsqlCommand(query, con);

            com.ExecuteNonQuery();

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(com);

            DataTable dt = new DataTable();

            com.CommandText = "fetch all in" + "\"stockref\";";
            com.CommandType = CommandType.Text;
            da.Fill(dt);



            //Bind generic list using dataRow     
            foreach (DataRow dr in dt.Rows)
            {

                BldstckList.Add(

                    new BloodStockModel
                    {
                        bloodstockid = Convert.ToInt32(dr["bloodstockid"]),
                        bloodgroup = Convert.ToString(dr["bloodgroup"]),
                        bloodbankname = Convert.ToString(dr["name"]),
                        statename = Convert.ToString(dr["statename"]),
                        cityname = Convert.ToString(dr["cityname"]),
                        quantity = Convert.ToInt32(dr["quantity"])                        
                    });
            }
            return BldstckList;
        }


        public List<BloodStockModel> getUserBloodStock()
        {
            Connection();

            List<BloodStockModel> BldstckList = new List<BloodStockModel>();

            con.Open();

            NpgsqlTransaction tran = con.BeginTransaction();

            string query = "select * from public.spbloodstock('stockref'";
            query = query + ",p_operation:=" + 7;
            query = query + ",p_bloodbankid:=" + HttpContext.Current.Session["bloodbankid"];
            query = query + ");";

            NpgsqlCommand com = new NpgsqlCommand(query, con);

            com.ExecuteNonQuery();

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(com);

            DataTable dt = new DataTable();

            com.CommandText = "fetch all in" + "\"stockref\";";
            com.CommandType = CommandType.Text;
            da.Fill(dt);



            //Bind generic list using dataRow     
            foreach (DataRow dr in dt.Rows)
            {

                BldstckList.Add(

                    new BloodStockModel
                    {
                        bloodstockid = Convert.ToInt32(dr["bloodstockid"]),
                        bloodgroup = Convert.ToString(dr["bloodgroup"]),
                        bloodbankname = Convert.ToString(dr["name"]),
                        statename = Convert.ToString(dr["statename"]),
                        cityname = Convert.ToString(dr["cityname"]),
                        quantity = Convert.ToInt32(dr["quantity"])
                    });
            }
            return BldstckList;
        }


        //To Add BloodStock details
        public bool AddBloodStock(BloodStockModel obj)
        {
            try
            {
                Connection();
                con.Open();
                NpgsqlCommand cmd = new NpgsqlCommand();
                {
                    string query = "select * from public.spbloodstock('stockref'";
                    query = query + ",p_operation:=" + 2;
                    query = query + ",p_bloodgroupid:=" + obj.bloodgroup;
                    query = query + ",p_bloodbankid:=" + HttpContext.Current.Session["bloodbankid"];
                    query = query + ",p_quantity:=" + obj.quantity;
                    query = query + ");fetch all in " + "\"stockref\";";

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

        public bool UpdateBloodStock(int id,BloodStockModel obj)
        {
            try
            {
                Connection();
                con.Open();
                NpgsqlCommand cmd = new NpgsqlCommand();
                {
                    string query = "select * from public.spbloodstock('stockref'";
                    query = query + ",p_operation:=" + 3;
                    query = query + ",p_stockid:=" + id;
                    /*query = query + ",p_bloodgroupid:=" + obj.bloodgroup;*/
                    query = query + ",p_bloodbankid:=" + HttpContext.Current.Session["bloodbankid"]; ;
                    /* query = query + ",p_stateid:=" + obj.statename;
                     query = query + ",p_cityid:=" + obj.cityname;*/
                    query = query + ",p_quantity:=" + obj.quantity;
                    query = query + ");fetch all in " + "\"stockref\";";

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

        public bool DeleteBloodStock(int id)
        {
            try
            {
                Connection();
                con.Open();
                string query = "select * from public.spbloodstock('stockref'";
                query = query + ",p_operation:=" + 4;
                query = query + ",p_stockid:=" + id;
                query = query + ");fetch all in " + "\"stockref\";";

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
    }
}