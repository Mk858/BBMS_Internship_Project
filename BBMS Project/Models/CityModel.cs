using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace BBMS_Project.Models
{
    public class CityModel
    {
        [DisplayName("City Id")]
        public int cityid { get; set; }

        [DisplayName("State Id")]
        public int stateid { get; set; }

        [DisplayName("City")]
        public string cityname { get; set; }
    }
}