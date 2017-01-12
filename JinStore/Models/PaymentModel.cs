using System;
using System.Collections.Generic;
using System.Linq;
using Braintree;

namespace JinStore.Models
{
    public class PaymentModel
    {
        public string ID { get; set; }
        public string CardType { get; set; }
        public string ExpirationDate { get; set; }
        public string LastFour { get; set; }
    }
}
