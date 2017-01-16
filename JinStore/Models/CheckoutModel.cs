using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JinStore.Models
{
    public class CheckoutModel
    {
        public Guid id { get; set; }

        public CartModel[] CartItem { get; set; }

        public string ticketID { get; set; }
        public string origin { get; set; }
        public string destination { get; set; }
        public DateTime? departureTime { get; set; }
        public DateTime? arrivalTime { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [CreditCard]
        public string CreditCardNumber { get; set; }

        [Required]
        public string CreditCardName { get; set; }

        [Required]
        //[RegularExpression("/^[0-9]{3,4}$/", ErrorMessage = "Your CVV Is Invalid!")]
        public string CreditCardVerificationValue { get; set; }

        [Required]
        [Range(01, 12)]
        public int CreditCardExpirationMonth { get; set; }

        [Required]
        [Range(2000, 3000)]
        public int CreditCardExpirationYear { get; set; }

        [Required]
        public string BillingStreet1 { get; set; }

        public string BillingStreet2 { get; set; }

        [Required]
        public string BillingCity { get; set; }

        [Required]
        public string BillingState { get; set; }

        [Required]
        public string BillingPostalCode { get; set; }

        [Required]
        public string BillingReceipient { get; set; }
    }
}