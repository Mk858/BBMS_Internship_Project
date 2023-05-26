
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace BBMS_Project.Models
{
    public class SeekerModel
    {
        [Key]

        [DisplayName("Seeker Id")]
        public int seekerid { get; set; }

        [DisplayName("Status")]
        public string requeststatus { get; set; }

        [DisplayName("TotalRequest")]
        public int TotalRequest { get; set; }


        [Required(ErrorMessage = "Name is required!")]
        [RegularExpression("^[a-zA-Z' ']{1,50}$", ErrorMessage = "Kindly enter only alphabet")]
        [DisplayName("Name")]
        public string Fullname { get; set; }

        [DisplayName("Gender Id")]
        public int genderid { get; set; }

        [Required(ErrorMessage = "Gender is required!")]
        [DisplayName("Gender")]
        public string Gender { get; set; }


        [Required(ErrorMessage = "Age is required!")]
        [Range(18, 90, ErrorMessage = "Age should be between 18 and 90")]
        [DisplayName("Age")]
        public int Age { get; set; }


        /*  [RegularExpression(@"(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)))$", ErrorMessage = "Invalid date format.")]*/
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Birthdate is required!")]
        [DisplayName("Birthdate")]
        public DateTime Birthdate { get; set; }


        public int bloodgroupid { get; set; }
        [Required(ErrorMessage = " Bloodgroup is required!")]
        [Display(Name = "Blood Group")]
        public string Bloodgroup { get; set; }


        [Required(ErrorMessage = " Address is required!")]
        [DisplayName("Address")]
        public string Address { get; set; }


        [Required(ErrorMessage = "Pincode is required!")]
        [RegularExpression(@"^[1-9][0-9]{5}$", ErrorMessage = "Please enter valid pin code.")]
        [DisplayName("Pincode")]
        public string Pincode { get; set; }


        [DisplayName("City Id")]
        public int cityid { get; set; }

        [Required(ErrorMessage = "City is required!")]
        [DisplayName("City")]
        public string City { get; set; }

        [DisplayName("State Id")]
        public int stateid { get; set; }

        [Required(ErrorMessage = "State is required!")]
        [DisplayName("State")]
        public string State { get; set; }


        [Required(ErrorMessage = "Email id is required!")]
        [Display(Name = "Email")]
        [EmailAddress]
        [RegularExpression(@"^([a-zA-Z0-9.])*@(gmail|amnex|yahoo)\.com$", ErrorMessage = "Entered email is not valid.")]
        public string Email { get; set; }


        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Contact Number")]
        [Required(ErrorMessage = "Contact number is required!")]
        [RegularExpression(@"(^[6789][0-9]{9})$", ErrorMessage = "Entered format is not valid.")]
        public string Phoneno { get; set; }

        [DisplayName("BloodBank Id")]
        public int bloodbankid { get; set; }

        [Required(ErrorMessage = " Blood Bank is required!")]
        [Display(Name = "Blood Bank")]
        public string Bloodbank { get; set; }


        /*        [RegularExpression(@"(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)))$", ErrorMessage = "Invalid date format.")]
        */
        [Required(ErrorMessage = "Requirement Date is required!")]
        [DataType(DataType.Date)]
        [Display(Name = " Requirement Date")]
        public DateTime RequirementDate { get; set; }


        [Display(Name = "Volume(ml)")]
        [RegularExpression(@"^(250|500|750|1000)$", ErrorMessage = "Please enter volume in a multiple of 250ml.")]
        [Required(ErrorMessage = "Volume is required!")]
        public decimal Volume { get; set; }


        [DataType(DataType.DateTime)]
        [Display(Name = "Created On")]
        public DateTime Createdon { get; set; }


        [DisplayName("Seeker Reuest Id")]
        public int seekerrequestid { get; set; }

        public bool status { get; set; }

        [DisplayName("Status")]
        public string ReqStat { get; set; }

    }
}

/*using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace BBMS_Project.Models
{
    public class SeekerModel
    {
        [Key]

        [DisplayName("Seeker Id")]
        public int seekerid { get; set; }

        [DisplayName("Status")]
        public string requeststatus { get; set; }

        [DisplayName("TotalRequest")]
        public int TotalRequest { get; set; } 


        [Required(ErrorMessage = "Name is required!")]
        [RegularExpression("^[a-zA-Z' ']{1,50}$", ErrorMessage = "Kindly enter only alphabet")]
        [DisplayName("Name")]
        public string Fullname { get; set; }

        [DisplayName("Gender Id")]
        public int genderid { get; set; }

        [Required(ErrorMessage = "Gender is required!")]
        [DisplayName("Gender")]
        public string Gender { get; set; }


        [Required(ErrorMessage = "Age is required!")]
        [Range(18, 90, ErrorMessage = "Age should be between 18 and 90")]
        [DisplayName("Age")]
        public int Age { get; set; }


      *//*  [RegularExpression(@"(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)))$", ErrorMessage = "Invalid date format.")]*//*
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Birthdate is required!")]
        [DisplayName("Birthdate")]
        public DateTime Birthdate { get; set; }


        public int bloodgroupid { get; set; }
        [Required(ErrorMessage = " Bloodgroup is required!")]
        [Display(Name = "Blood Group")]
        public string Bloodgroup { get; set; }


        [Required(ErrorMessage = " Address is required!")]
        [DisplayName("Address")]
        public string Address { get; set; }


        [Required(ErrorMessage = "Pincode is required!")]
        [RegularExpression(@"^[1-9][0-9]{5}$", ErrorMessage = "Please enter valid pin code.")]
        [DisplayName("Pincode")]
        public string Pincode { get; set; }


        [DisplayName("City Id")]
        public int cityid { get; set; }

        [Required(ErrorMessage = "City is required!")]
        [DisplayName("City")]
        public string City { get; set; }

        [DisplayName("State Id")]
        public int stateid { get; set; }

        [Required(ErrorMessage = "State is required!")]
        [DisplayName("State")]
        public string State { get; set; }


        [Required(ErrorMessage = "Email id is required!")]
        [Display(Name = "Email")]
        [EmailAddress]
        [RegularExpression(@"^([a-zA-Z0-9.])*@(gmail|amnex|yahoo)\.com$", ErrorMessage = "Entered email is not valid.")]
        public string Email { get; set; }


        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Contact Number")]
        [Required(ErrorMessage = "Contact number is required!")]
        [RegularExpression(@"(^[6789][0-9]{9})$", ErrorMessage = "Entered format is not valid.")]
        public string Phoneno { get; set; }

        [DisplayName("BloodBank Id")]
        public int bloodbankid { get; set; }

        [Required(ErrorMessage = " Blood Bank is required!")]
        [Display(Name = "Blood Bank")]
        public string Bloodbank { get; set; }


        *//*        [RegularExpression(@"(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)))$", ErrorMessage = "Invalid date format.")]
        *//*
        [Required(ErrorMessage = "Requirement Date is required!")]
        [DataType(DataType.Date)]
        [Display(Name = " Requirement Date")]
        public DateTime RequirementDate { get; set; }


        [Display(Name = "Volume(ml)")]
        [RegularExpression(@"^(250|500|750|1000)$", ErrorMessage = "Please enter volume in a multiple of 250ml.")]
        [Required(ErrorMessage = "Volume is required!")]
        public decimal Volume { get; set; }


        [DataType(DataType.DateTime)]
        [Display(Name = "Created On")]
        public DateTime Createdon { get; set; }


        [DisplayName("Seeker Reuest Id")]
        public int seekerrequestid { get; set; }

        public bool status { get; set; }





        *//*
                [Key]
                public int seekerid { get; set; }
                [Required(ErrorMessage = " Name is Required")]
                [RegularExpression("^[A-Za-z' ']+[A-Za-z]*$", ErrorMessage = "Kindly Enter only Alphabet")]

                public string Fullname { get; set; }
                [Required(ErrorMessage = "Gender is Required")]
                public int genderid { get; set; }
                public string Gender { get; set; }
                [Required(ErrorMessage = " Age is Required"), Range(10, 70)]
                public int Age { get; set; }

                [DataType(DataType.Date)]
                //[RegularExpression(@"^(0[1-9]|1[012])[-/](0[1-9]|[12][0-9]|3[01])[-/](19|20)\d\d$", ErrorMessage = "End Date should be in MM/dd/yyyy format")]
                public DateTime Birthdate { get; set; }
                [Required(ErrorMessage = " BloodGroup is Required")]
                public int bloodgroupid { get; set; }
                [Display(Name = "Blood Group")]

                public string Bloodgroup { get; set; }
                [Required(ErrorMessage = " Address is Required")]
                public string Address { get; set; }
                [Required(ErrorMessage = "Pincode is Required")]
                [RegularExpression(@"^\d{6}(-\d{4})?$", ErrorMessage = "Please Enter Valid Pin Code.")]


                public string Pincode { get; set; }
                public int cityid { get; set; }
                public string City { get; set; }
                public int stateid { get; set; }
                public string State { get; set; }
                [Required]
                [Display(Name = "Email")]
                [EmailAddress]
                [RegularExpression(@"^([a-zA-Z\d\.]+)@([a-zA-Z\d-]+)\.([a-zA-Z]{3})?$",
                           ErrorMessage = "Entered email format is not valid.")]

                public string Email { get; set; }

                [DataType(DataType.PhoneNumber)]
                [Display(Name = "Contact Number")]
                [Required(ErrorMessage = "Phone Number Required!")]
                [RegularExpression(@"^\(?([6-9]{1})\)?[-. ]?([0-9]{5})[-. ]?([0-9]{4})$",
                           ErrorMessage = "Entered phone format is not valid.")]
                public string Phoneno { get; set; }
                public int bloodbankid { get; set; }
                [Display(Name = "Blood Bank")]
                public string Bloodbank { get; set; }
                [DataType(DataType.Date)]
                [Display(Name = " Requirement Date")]

                public DateTime RequirementDate { get; set; }

                [Display(Name = "Volume(ml)")]
                [Required(ErrorMessage = "Volume is Required")]

                public decimal Volume { get; set; }

                [DataType(DataType.DateTime)]
                [Display(Name = "Created On")]
                public DateTime Createdon { get; set; }

                public int seekerrequestid { get; set; }*//*


        [DisplayName("Status")]
        public string ReqStat { get; set; }

    }
}*/