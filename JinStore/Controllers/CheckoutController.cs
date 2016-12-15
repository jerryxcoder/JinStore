using JinStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Mvc;

namespace JinStore.Controllers
{
    public class CheckoutController : Controller
    {
        public ActionResult Index()
        {
            CheckoutModel model = new CheckoutModel();
            return View(model);
        }
        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(CheckoutModel model)
        {
            if (ModelState.IsValid)
            {
                //TODO: Validate the credit card - if it errors out, add a model error and display it to the user
                //TODO: Persist this information to the database
                return RedirectToAction("Index", "Receipt", null);
            }
            return View(model);
        }
    }
}
