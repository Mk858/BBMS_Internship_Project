using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace BBMS_Project.Models
{
    public class BloodGroupModel
    {
        [DisplayName("BloodGroup Id")]
        public int bloodgroupid { get; set; }
        [DisplayName("BloodGroup")]
        public string bloodgroup { get; set; }
    }
}