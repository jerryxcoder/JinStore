using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JinStore.Models
{
    public class CartModel
    {
        public string ticketID { get; set; }
        public string origin { get; set; }
        public string destination { get; set; }
        public string departureTime { get; set; }
        public string arrivalTime { get; set; }
    }
}