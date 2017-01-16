using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JinStore.Models
{
    public class CartModel
    {
        public Guid Id { get; set; }
        public string TicketId { get; set; }
        public string origin { get; set; }
        public string destination { get; set; }
        public DateTime departureTime { get; set; }
        public DateTime arrivalTime { get; set; }
        public string saleTotal { get; set; }
        public string stops { get; set; }
        public string carrier { get; set; }
        public string number { get; set; }
        public string adultCount { get; set; } 
    }
}