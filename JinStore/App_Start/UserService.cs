using JinStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JinStore.Models
{
    public class UserService
    {
        public static void AssignUserToRoles(string email, IEnumerable<string> selectedRoles)
        {
            int id = -1;
            using (MemberEntities1 e = new MemberEntities1())
            {
                id = e.CustomerLists.Single(u => u.EmailAddress == email).ID;
            }
            AssignUserToRoles(id, selectedRoles);
        }

        public static void AssignUserToRoles(int userId, IEnumerable<string> selectedRoles)
        {
            using (MemberEntities1 e = new MemberEntities1())
            {


                var user = e.CustomerLists.Single(u => u.ID == userId);
                foreach (string role in selectedRoles)
                {
                    if (!user.webpages_Roles.Any(x => x.RoleName == role))
                    {
                        user.webpages_Roles.Add(e.webpages_Roles.Single(x => x.RoleName == role));
                    }
                }
                string[] currentRoles = user.webpages_Roles.Select(x => x.RoleName).ToArray();
                foreach (var role in currentRoles)
                {
                    if (!selectedRoles.Contains(role))
                    {
                        user.webpages_Roles.Remove(user.webpages_Roles.Single(x => x.RoleName == role));
                    }
                }
                e.SaveChanges();


            }
        }
    }
}