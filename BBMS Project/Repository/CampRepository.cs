using BBMS_Project.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;

namespace BBMS_Project.Repository
{
    public class CampRepository
    {
        private NpgsqlConnection con;

        public void Connection()
        {
            string conStr = Convert.ToString(ConfigurationManager.ConnectionStrings["conn"]);
            con = new NpgsqlConnection(conStr);
        }

        //To view All Camp details with generic list
        public List<Camp> GetAllCampDetails()
        {
            Connection();
            con.Open();

            List<Camp> CampList = new List<Camp>();

            NpgsqlTransaction tran = con.BeginTransaction();

            string mainQuery = "select * from public.spcamp('campref'";
            mainQuery = mainQuery + ",p_operation:=" + 1;
            mainQuery = mainQuery + ");";

            NpgsqlCommand com = new NpgsqlCommand(mainQuery, con);

            com.ExecuteNonQuery();

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(com);

            DataTable dt = new DataTable();
            com.CommandText = "fetch all in" + "\"campref\";";
            com.CommandType = CommandType.Text;
            da.Fill(dt);

            con.Close();

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

            return CampList;
        }

        //To view All Camp details with generic list
        public List<Camp> GetBBCampDetails()
        {
            Connection();
            con.Open();

            List<Camp> CampList = new List<Camp>();

            NpgsqlTransaction tran = con.BeginTransaction();

            string mainQuery = "select * from public.spcamp('campref'";
            mainQuery = mainQuery + ",p_operation:=" + 1;
            mainQuery = mainQuery + ",p_bloodbankid:=" + HttpContext.Current.Session["bloodbankid"];
            mainQuery = mainQuery + ");";

            NpgsqlCommand com = new NpgsqlCommand(mainQuery, con);

            com.ExecuteNonQuery();

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(com);

            DataTable dt = new DataTable();
            com.CommandText = "fetch all in" + "\"campref\";";
            com.CommandType = CommandType.Text;
            da.Fill(dt);

            con.Close();

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

            return CampList;
        }


        //search camp by camp date with stata and city
        public List<Camp> GetCampDetails(Camp obj)
        {
            Connection();
            con.Open();

            List<Camp> CampList = new List<Camp>();

            NpgsqlTransaction tran = con.BeginTransaction();

            string mainQuery = "select * from public.spcamp('campref'";
            mainQuery = mainQuery + ",p_operation:=" + 5;
            mainQuery = mainQuery + ",p_stateid:=" + obj.stateid;
            mainQuery = mainQuery + ",p_cityid:=" + obj.cityid;
            /*mainQuery = mainQuery + ",p_schedstart:=" + "\'" + obj.schedstart + "\'";*/
            mainQuery = mainQuery + ");";

            NpgsqlCommand com = new NpgsqlCommand(mainQuery, con);

            com.ExecuteNonQuery();

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(com);

            DataTable dt = new DataTable();
            com.CommandText = "fetch all in" + "\"campref\";";
            com.CommandType = CommandType.Text;
            da.Fill(dt);

            con.Dispose();
            con.Close();

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
                        /*schedstart = Convert.ToDateTime(dr["schedstart"]),
                        schedend = Convert.ToDateTime(dr["schedend"])*/
                    }
                );
            }

            return CampList;
        }

        //To Add Camp details    
        public bool AddCamp(Camp obj)
        {
            try
            {
                Connection();
                con.Open();
                NpgsqlCommand cmd = new NpgsqlCommand();
                {
                    string query = "select * from public.spcamp('campref'";
                    query = query + ",p_operation:=" + 7;
                    query = query + ",p_name:=" + "\'" + obj.name + "\'";
                    query = query + ",p_address:=" + "\'" + obj.address + "\'";
                    query = query + ",p_stateid:=" + obj.stateid;
                    query = query + ",p_cityid:=" + obj.cityid;
                    query = query + ",p_contactno:=" + "\'" + obj.contactno + "\'";
                    query = query + ",p_conductedby:=" + "\'" + obj.conductedby + "\'";
                    query = query + ",p_organizedby:=" + "\'" + obj.organizedby + "\'";
                    query = query + ",p_schedstart:=" + "\'" + obj.schedstart + "\'";
                    query = query + ",p_schedend:=" + "\'" + obj.schedend + "\'";

                    query = query + ");fetch all in " + "\"campref\";";

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

        //To Add Blood Bank Camp details    
        public bool AddBBCamp(Camp obj)
        {
            try
            {
                Connection();
                con.Open();
                NpgsqlCommand cmd = new NpgsqlCommand();
                {
                    string query = "select * from public.spcamp('campref'";
                    query = query + ",p_operation:=" + 2;
                    query = query + ",p_name:=" + "\'" + obj.name + "\'";
                    query = query + ",p_address:=" + "\'" + obj.address + "\'";
                    query = query + ",p_stateid:=" + obj.stateid;
                    query = query + ",p_cityid:=" + obj.cityid;
                    query = query + ",p_contactno:=" + "\'" + obj.contactno + "\'";
                    query = query + ",p_organizedby:=" + "\'" + obj.organizedby + "\'";
                    query = query + ",p_schedstart:=" + "\'" + obj.schedstart + "\'";
                    query = query + ",p_schedend:=" + "\'" + obj.schedend + "\'";
                    query = query + ",p_bloodbankid:=" + HttpContext.Current.Session["bloodbankid"];
                    query = query + ");fetch all in " + "\"campref\";";

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

        //To Edit Camp details
        public bool UpdateCamp(int id, Camp obj)
        {
            try
            {
                Connection();
                con.Open();
                string query = "select * from public.spcamp('campref'";
                query = query + ",p_operation:=" + 3;
                query = query + ",p_campid:=" + obj.campid;
                query = query + ",p_name:=" + "\'" + obj.name + "\'";
                query = query + ",p_address:=" + "\'" + obj.address + "\'";
                query = query + ",p_stateid:=" + obj.stateid;
                query = query + ",p_cityid:=" + obj.cityid;
                query = query + ",p_contactno:=" + "\'" + obj.contactno + "\'";
               /* query = query + ",p_conductedby:=" + "\'" + obj.conductedby + "\'";*/
                query = query + ",p_organizedby:=" + "\'" + obj.organizedby + "\'";
                query = query + ",p_schedstart:=" + "\'" + obj.schedstart + "\'";
                query = query + ",p_schedend:=" + "\'" + obj.schedend + "\'";

                query = query + ");fetch all in " + "\"campref\";";
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

        //To delete Camp details    
        public bool DeleteCamp(int Id)
        {
            try
            {
                Connection();
                con.Open();

                string query = "select * from public.spcamp('campref'";
                query = query + ",p_operation:=" + 4;

                query = query + ",p_campid:=" + Id;

                query = query + ");fetch all in " + "\"campref\";";

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