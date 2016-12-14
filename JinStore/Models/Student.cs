using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace JinStore.Models
{
    public class Student
    {
        [Display(Name ="ID")]
        public int ID { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Birthday")]
        public DateTime? DateOfBirth { get; set; }         
    }
}