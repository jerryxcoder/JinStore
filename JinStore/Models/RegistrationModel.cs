using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JinStore.Models
{

    public class RegistrationModel
    {
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class ForgotPasswordModel
    {
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

    }
    public class ResetPasswordModel
    {

        [Required]
        public string Password { get; set; }
        public string ResetToken { get; set; }
    }
}