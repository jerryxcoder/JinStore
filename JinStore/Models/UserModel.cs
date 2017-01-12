using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JinStore.Models
{
    public class UserModel
    {
        public int ID { get; set; }
        public string EmailAddress { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public IEnumerable<string> AvailableRoles { get; set; }
    }
}