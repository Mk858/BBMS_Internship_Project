using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace BBMS_Project.Models
{
    public class Camp
    {
        [Display(Name = "Camp Id")]
        public int campid { get; set; }

        [DataType(DataType.Text), Required(ErrorMessage = "Name Must be Required.")]
        [Display(Name = "Camp")]
        [RegularExpression("^[a-zA-Z' ']{1,50}$", ErrorMessage = "Kindly enter only alphabet")]
        public string name { get; set; }

        [DataType(DataType.Text), Required(ErrorMessage = "Address Must be Required.")]
        [Display(Name = "Camp Address")]
        public string address { get; set; }

        public int stateid { get; set; }
        [DataType(DataType.Text) ,Required(ErrorMessage = "State Must be Required.")]

        [Display(Name = "Camp State")] 
        public string state { get; set; }
        public int cityid { get; set; }

        [DataType(DataType.Text) ,Required(ErrorMessage = "City Must be Required.")]
        [Display(Name = "Camp City")] 
        public string city { get; set; }

        [DataType(DataType.PhoneNumber), Required(ErrorMessage = "Contact No. Must be Required.")]
        [RegularExpression(@"(^[6789][0-9]{9})$", ErrorMessage = "Entered format is not valid.")]
        [Display(Name = "Organizer Contact No.")]
        public string contactno { get; set; }

        [DataType(DataType.Text), Required(ErrorMessage = "Conducted by whom?")]
        [Display(Name = "Camp Conducted By")]
        public string conductedby { get; set; }

        [DataType(DataType.Text), Required(ErrorMessage = "Organizer Name Must be Required.")]
        [Display(Name = "Camp Organized By")] 
        public string organizedby { get; set; }

        [DataType(DataType.Date), Required(ErrorMessage = "Schedule Start Time / Date Must be Required.")]
        [Display(Name = "Camp Start Schedule")] 
        public DateTime schedstart { get; set; }

        [DataType(DataType.Date), Required(ErrorMessage     = "Schedule End Time / Date Must be Required.")]
        [Display(Name = "Camp End Schedule")] 
        public DateTime schedend { get; set; }
    }
}