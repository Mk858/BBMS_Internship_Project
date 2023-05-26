using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace BBMS_Project.Models
{
    public class BloodStockModel
    {
        [DisplayName("BloodStock Id")]
        public int bloodstockid { get; set; }
        
        [DisplayName("BloodGroup Id")]
        public int bloodgroupid { get; set; }

        [DisplayName("BloodBank Id")]
        public int bloodbankid { get; set; }

        [DisplayName("StateId ")]
        public int stateid { get; set; }

        [DisplayName("CityId")]
        public int cityid { get; set; }

        [DisplayName("Quantity")]
        public int quantity { get; set; }

        [DisplayName("BloodBank")]
        public string bloodbankname { get; set; }

        [DisplayName("State")]
        public string statename { get; set; }

        [DisplayName("City")]
        public string cityname { get; set; }

        [DisplayName("BloodGroup")]
        public string bloodgroup { get; set; }
    }
}