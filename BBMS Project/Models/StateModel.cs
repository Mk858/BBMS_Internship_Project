using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace BBMS_Project.Models
{
    public class StateModel
    {
        [DisplayName("State Id")]
        public int stateid { get; set; }

        [DisplayName("State")]
        public string statename { get; set; }
    }
}