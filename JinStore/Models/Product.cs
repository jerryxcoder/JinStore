using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace JinStore.Models
{
    public class Product
    {
        public string id { get; set; }
        public string TicketID { get; set; }
        public string cabin { get; set; }
        public string origin { get; set; }
        public string destination { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime departureTime { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime arrivalTime { get; set; }
        public int duration { get; set; }
        public int adultCount { get; set; }
        public int childCount { get; set; }
        public decimal saleTotal { get; set; }
        public string carrier { get; set; }
        public string number { get; set; }

     }
}