using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JinStore.Models;
 

namespace JinStore.Controllers
{
    public class MemberController : Controller
    {
        public List<Members> MemberList = new List<Members>();
        // GET: Member
        public ActionResult Index(int? id)
        {
            using (JinStore.Models.MemberEntities1 entities = new MemberEntities1())
            {
                var Member = entities.CustomerLists.Single(x => x.ID == id);

                Members MemberModel = new Members();
                MemberModel.id = Member.ID;
                MemberModel.FirstName = Member.FirstName;
                MemberModel.LastName = Member.LastName;
                return View(MemberModel);
            }
        }
    }
}