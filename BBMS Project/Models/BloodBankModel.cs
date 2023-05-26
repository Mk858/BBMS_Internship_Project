using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace BBMS_Project.Models
{
    public class BloodBankModel
    {
        [Key]
        [DisplayName("BloodBank ID")]
        public int bloodbankid { get; set; }

        [DisplayName("BloodBank")]
        [RegularExpression("^[a-zA-Z' ']{1,50}$", ErrorMessage = "Kindly enter only alphabet")]
        [Required(ErrorMessage = "BloodBank Name is required.")]
        public string name { get; set; }

        [DisplayName("Address")]
        [Required(ErrorMessage = "Address is required.")]
        public string address { get; set; }

        [DataType(DataType.PhoneNumber)]
        [DisplayName("Phone No")]
        [Required(ErrorMessage = "Mobile Number is required.")]
        [RegularExpression(@"(^[6789][0-9]{9})$", ErrorMessage = "Entered format is not valid.")]
        public string phoneno { get; set; }

        [DisplayName("Website")]
        [Required(ErrorMessage = "Website is required.")]
        [StringLength(60)]
        /*[RegularExpression(@"((([A-Za-z]{3,9}:(?:\/\/)?)(?:[-;:&=\+\$,\w]+@)?[A-Za-z0-9.-]+|(?:www.|[-;:&=\+\$,\w]+@)[A-Za-z0-9.-]+)((?:\/[\+~%\/.\w-_]*)?\??(?:[-\+=&;%@.\w_]*)#?(?:[\w]*))?)", 
            ErrorMessage = "Not a valid website URL")]*/
        public string website { get; set; }

        [DisplayName("Email")]
        [RegularExpression(@"^([a-zA-Z0-9.])*@(gmail|amnex|yahoo)\.com$", ErrorMessage = "Entered email is not valid.")]
        [Required(ErrorMessage = "Email is required.")]
        /*        [RegularExpression(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$", ErrorMessage = "Invalid Email Address.")]
        */
        public string email { get; set; }

        [DisplayName("BloodGroup Id")]
        public int bloodgroupid { get; set; }

        [DisplayName("BloodGroup")]
        [Required(ErrorMessage = "BloodGroup is required.")]
        public string bloodgroup { get; set; }

        [DisplayName("State Id")]
        public int stateid { get; set; }

        [DisplayName("State")]
        [Required(ErrorMessage = "State Name required.")]
        public string statename { get; set; }

        [DisplayName("City Id")]
        public int cityid { get; set; }

        
        [DisplayName("City")]
        [Required(ErrorMessage = "City Name required.")]
        public string cityname { get; set; }

        [DisplayName("Status")]
        public string status { get; set; }
    }
}