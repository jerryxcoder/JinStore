//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace JinStore.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Cart
    {
        public System.Guid Id { get; set; }
        public string TicketID { get; set; }
        public string origin { get; set; }
        public string destination { get; set; }
        public string departureTime { get; set; }
        public string arrivalTime { get; set; }
        public Nullable<decimal> saleTotal { get; set; }
        public string carrier { get; set; }
        public string number { get; set; }
        public Nullable<int> NumStops { get; set; }
    }
}
