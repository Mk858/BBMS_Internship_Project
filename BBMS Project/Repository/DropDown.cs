using BBMS_Project.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;

namespace BBMS_Project.Repository
{
    public class DropDown
    {
        private NpgsqlConnection con;

        //To Handle connection related activities    
        private void Connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["conn"].ToString();
            con = new NpgsqlConnection(constr);

        }

        public List<GenderModel> GetGender()
        {
            Connection();
            con.Open();

            NpgsqlTransaction tran = con.BeginTransaction();



            NpgsqlCommand com = new NpgsqlCommand("select genderid, gender from gender", con);
            com.ExecuteNonQuery();

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(com);

            DataTable dt = new DataTable();


            da.Fill(dt);

            List<GenderModel> GenderList = new List<GenderModel>();

            //Bind EmpModel generic list using dataRow     
            foreach (DataRow dr in dt.Rows)
            {

                GenderList.Add(

                    new GenderModel()
                    {
                        genderid = Convert.ToInt32(dr["genderid"]),
                        gender = Convert.ToString(dr["gender"])
                    }
                );
            }

            con.Close();
            return GenderList;
        }

        public List<BloodGroupModel> GetBloodGroup()
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

            List<BloodGroupModel> BloodGroupList = new List<BloodGroupModel>();

            //Bind EmpModel generic list using dataRow     
            foreach (DataRow dr in dt.Rows)
            {

                BloodGroupList.Add(

                    new BloodGroupModel
                    {
                        bloodgroupid = Convert.ToInt32(dr["bloodgroupid"]),
                        bloodgroup = Convert.ToString(dr["bloodgroup"])
                    }
                );
            }

            con.Close();
            return BloodGroupList;
        }

        public List<StateModel> GetStateList()
        {
            Connection();
            con.Open();

            NpgsqlTransaction tran = con.BeginTransaction();

            NpgsqlCommand com = new NpgsqlCommand("select * from state", con);
            com.ExecuteNonQuery();

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(com);

            DataTable dt = new DataTable();

            da.Fill(dt);

            List<StateModel> stateList = new List<StateModel>();

            //Bind state generic list using dataRow     
            foreach (DataRow dr in dt.Rows)
            {

                stateList.Add(

                    new StateModel
                    {
                        stateid = Convert.ToInt32(dr["stateid"]),
                        statename = Convert.ToString(dr["statename"])
                    }
                );
            }
            con.Dispose();
            con.Close();
            return stateList;
        }

        public List<CityModel> GetCities(int id)
        {
            Connection();
            con.Open();
            NpgsqlTransaction tran = con.BeginTransaction();
            List<CityModel> CityList = new List<CityModel>(id);
            NpgsqlCommand cmd = new NpgsqlCommand("Select * from City where stateid = " + id, con);
            cmd.ExecuteNonQuery();

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            //Bind city generic list using dataRow     
            foreach (DataRow dr in dt.Rows)     
            {

                CityList.Add(

                    new CityModel
                    {
                        cityid = Convert.ToInt32(dr["cityid"]),
                        cityname = Convert.ToString(dr["cityname"]),
                        stateid = Convert.ToInt32(dr["stateid"])
                    }
                );
            }
            con.Dispose();
            con.Close();
            return CityList;
        }


        public List<BloodBankModel> bloodbanklist(int id)
        {
            

            string constr = ConfigurationManager.ConnectionStrings["conn"].ToString();
            NpgsqlConnection con = new NpgsqlConnection(constr);
            con.Open();
            NpgsqlTransaction tran = con.BeginTransaction();

            List<BloodBankModel> bloodbanklist = new List<BloodBankModel>(id);
          

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
                bloodbanklist.Add(new BloodBankModel
                {
                    bloodbankid = Convert.ToInt32(dr["bloodbankid"]),
                    name = Convert.ToString(dr["name"])
                });
            }

            return bloodbanklist;

        }



    }
}