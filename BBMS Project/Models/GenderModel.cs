using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace BBMS_Project.Models
{
    public class GenderModel
    {
        [DisplayName("Gender Id")]
        public int genderid { get; set; }

        [DisplayName("Gender")]
        public string gender { get; set; }
    }
}