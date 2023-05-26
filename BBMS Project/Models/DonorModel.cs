using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace BBMS_Project.Models
{
    public class DonorModel
    {
        [Key]
        [DisplayName("Donor Id")]
        public int donorid { get; set; }

        [DisplayName("Name")]
        [Required(ErrorMessage = " Name is Required")]
        [RegularExpression("^[a-zA-Z' ']{1,50}$", ErrorMessage = "Kindly enter only alphabet")]
        public string Fullname { get; set; }

        [DisplayName("Gender Id")]
        public int genderid { get; set; }

        [Required(ErrorMessage = " Gender is Required")]
        [DisplayName("Gender")]
        public string Gender { get; set; }

        [DisplayName("Age")]
        [Range(18, 70)]
        [Required(ErrorMessage = " Age is Required")]
        public int Age { get; set; }

        [DisplayName("BirthDate")]
        [Required(ErrorMessage = " Birthdate is Required")]
        [DataType(DataType.Date)]
        public DateTime? Birthdate { get; set; }

        [DisplayName("BloodGroup Id")]
        public int bloodgroupid { get; set; }

        [DisplayName("BloodGroup")]
        [Required(ErrorMessage = " BloodGroup is Required")]
        public string Bloodgroup { get; set; }

        [DisplayName("Address")]
        [Required(ErrorMessage = " Address is Required")]
        /*[StringLength(1, MinimumLength = 30, ErrorMessage = "This field must be 10 characters")]*/
        public string Address { get; set; }

        [DisplayName("PinCode")]
        [RegularExpression(@"^\d{6}(-\d{4})?$", ErrorMessage = "Please Enter Valid Postal Code.")]
        [Required(ErrorMessage = "Pincode is Required")]
        public string Pincode { get; set; }

        [DisplayName("City Id")]
        public int cityid { get; set; }

        [DisplayName("City")]
        [Required(ErrorMessage = " City is Required")]
        public string City { get; set; }

        [DisplayName("State Id")]
        public int stateid { get; set; }

        [DisplayName("State")]
        [Required(ErrorMessage = " State is Required")]
        public string State { get; set; }

        [DisplayName("Contact Number")]
        [RegularExpression(@"(^[6789][0-9]{9})$", ErrorMessage = "Entered format is not valid.")]
        [Required(ErrorMessage = "Phoneno is Required")]
        public string Phoneno { get; set; }

        [DisplayName("PAN Card No.")]
        [RegularExpression("[A-Z]{5}[0-9]{4}[A-Z]{1}", ErrorMessage = "Invalid PAN Card No.")]
        [Required(ErrorMessage = " PAN Card No. is Required")]
        public string Pancardno { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = " Email is Required")]
        [RegularExpression(@"^([a-zA-Z0-9.])*@(gmail|amnex|yahoo)\.com$", ErrorMessage = "Entered email is not valid.")]
        /*[RegularExpression(@"^[a-zA-Z0-9.!#$%&'+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)$", ErrorMessage = "Invalid Email Address.")]*/
        public string Email { get; set; }

        [DisplayName("Donor Request Id")]
        public int donorreqid { get; set; }

        [DisplayName("Volume")]
        [RegularExpression(@"^(250|500|750|1000)$", ErrorMessage = "Please enter volume in a multiple of 250ml.")]

        [Required(ErrorMessage = " Volume is Required")]
        public string Volume { get; set; }

        [DisplayName("RequestDate")]
        [Required(ErrorMessage = "Date is Required")]
        [DataType(DataType.Date)]
        public DateTime Requestdate { get; set; }

        [DisplayName("BloodBank")]
        [Required(ErrorMessage = "BloodBank is Required")]
        public string Bloodbank { get; set; }

        [DisplayName("BloodBank Id")]
        public int bloodbankid { get; set; }

        [DisplayName("Donor Count")]
        public int Donorcount { get; set; }

        [DisplayName("Status")]
        public string Reqstat { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Created On")]
        public DateTime Createdon { get; set; }

    }
}







/*using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace BBMS_Project.Models
{
    public class DonorModel
    {
        [Key]
        [DisplayName("Donor ID")]
        public int donorid { get; set; }

        [DisplayName("Name")]
        [Required(ErrorMessage = " Name is Required")]
        [RegularExpression("^[a-zA-Z' ']{1,50}$", ErrorMessage = "Kindly enter only alphabet")]
        public string fullname { get; set; }

        [DisplayName("Gender ID")]
        public int genderid { get; set; }

        [Required(ErrorMessage = " Gender is Required")]
        [DisplayName("Gender")]
        public string gender { get; set; }

        [DisplayName("Age")]
        [Range(18, 70)]
        [Required(ErrorMessage = " Age is Required")]
        public int age { get; set; }

        [DisplayName("Birthdate")]
        [Required(ErrorMessage = " Birthdate is Required")]
        [DataType(DataType.Date)]
        public DateTime? birthdate { get; set; }

        [DisplayName("BloodGroup ID")]
        public int bloodgroupid { get; set; }

        [DisplayName("BloodGroup")]
        [Required(ErrorMessage = " BloodGroup is Required")]
        public string bloodgroup { get; set; }

        [DisplayName("Address")]
        [Required(ErrorMessage = " Address is Required")]
        *//*[StringLength(1, MinimumLength = 30, ErrorMessage = "This field must be 10 characters")]*//*
        public string address { get; set; }

        [DisplayName("Pincode")]
        [RegularExpression(@"^\d{6}(-\d{4})?$", ErrorMessage = "Please Enter Valid Postal Code.")]
        [Required(ErrorMessage = "Pincode is Required")]
        public string pincode { get; set; }

        [DisplayName("City Id")]
        public int cityid { get; set; }

        [DisplayName("City")]
        [Required(ErrorMessage = " City is Required")]
        public string cityname { get; set; }

        [DisplayName("State Id")]
        public int stateid { get; set; }

        [DisplayName("State")]
        [Required(ErrorMessage = " State is Required")]
        public string statename { get; set; }

        [DisplayName("Phone Number")]
        [RegularExpression(@"(^[6789][0-9]{9})$", ErrorMessage = "Entered format is not valid.")]
        [Required(ErrorMessage = "Phoneno is Required")]
        public string phoneno { get; set; }

        [DisplayName("PAN Card No.")]
        [RegularExpression("[A-Z]{5}[0-9]{4}[A-Z]{1}", ErrorMessage = "Invalid PAN Card No.")]
        [Required(ErrorMessage = " PAN Card No. is Required")]
        public string pancardno { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = " Email is Required")]
        [RegularExpression(@"^([a-zA-Z0-9.])*@(gmail|amnex|yahoo)\.com$", ErrorMessage = "Entered email is not valid.")]
        *//*[RegularExpression(@"^[a-zA-Z0-9.!#$%&'+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)$", ErrorMessage = "Invalid Email Address.")]*//*
        public string email { get; set; }

        [DisplayName("Password")]
        [Required(ErrorMessage = " Password is Required")]
        public string password { get; set; }

        [DisplayName("Donor Request Id ")]
        public int donorreqid { get; set; }

        [DisplayName("Volume")]
        [Range(1, 2000)]
        [Required(ErrorMessage = " Volume is Required")]
        public string volume { get; set; }

        [DisplayName("Request Date")]
        [Required(ErrorMessage = "Date is Required")]
        [DataType(DataType.Date)]
        public DateTime requestdate { get; set; }

        [DisplayName("BloodBank")]
        [Required(ErrorMessage = "BloodBank is Required")]
        public string bloodbankname { get; set; }

        [DisplayName("BloodBank Id")]
        public int bloodbankid { get; set; }

        [DisplayName("Donor Count")]
        public int Donorcount { get; set; }

        [DisplayName("Status")]
        public string ReqStat { get; set; }

    }
}*/