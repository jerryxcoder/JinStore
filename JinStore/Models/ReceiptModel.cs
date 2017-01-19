using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JinStore.Models
{
    public class ReceiptModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string saleTotal { get; set; }
        public string carrier { get; set; }
        public string number { get; set; }
        public string origin { get; set; }
        public string destination { get; set; }
        public DateTime? departureTime { get; set; }
        public DateTime? arrivalTime { get; set; }
        public string stops { get; set; }
        public string BillingStreet1 { get; set; }
        public string BillingStreet2 { get; set; }
        public string BillingCity { get; set; }
        public string BillingState { get; set; }
        public string BillingPostalCode { get; set; }
        public DateTime InvoiceDate { get; set; }
    }
}