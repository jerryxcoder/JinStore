using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace JinStore.Models
{
    public class Product
    {
        public int TicketID { get; set; }
        public string TicketClass { get; set; }
        public string FlightType { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public string DePartureDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public string ReturnDate { get; set; }
        public int AdultNum { get; set; }
        public int ChildNum { get; set; }
        public string Price { get; set; }
        public string Carrier { get; set; }
        public string FlightNumber { get; set; }
    }
}