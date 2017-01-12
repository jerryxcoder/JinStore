using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JinStore.Models;
using System.Threading.Tasks;

namespace JinStore.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Contact()
        {
            ContactModel model = new ContactModel();
            return View(model);
        }

        // POST: Contact
        
        
    }
}