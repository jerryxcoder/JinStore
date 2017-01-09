using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JinStore.Models;

namespace JinStore.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            Models.LoginModel model = new Models.LoginModel();

            return View(model);
        }
        [HttpPost]
        public ActionResult Index(Models.LoginModel model)
        {
            if(ModelState.IsValid)
            {
                if(WebMatrix.WebData.WebSecurity.Login(model.EmailAddress, model.Password, true))
                {
                    HttpCookie authCookie = System.Web.Security.FormsAuthentication.GetAuthCookie(model.EmailAddress, true);
                    Response.Cookies.Add(authCookie);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("EmailAddress", "username does not exist");
            }
            return View(model);
        }
    }
}