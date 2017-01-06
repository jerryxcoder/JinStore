using JinStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(CheckoutModel model)
        {
            if (ModelState.IsValid)
            { 
                using (Models.MemberEntities1 entities = new Models.MemberEntities1())
                {
                    Order o = new Order();
                    o.FirstName = model.FirstName;
                    o.LastName = model.LastName;
                    o.EmailAddress = model.EmailAddress;
                    o.PhoneNumber = model.PhoneNumber;
                    o.CreditCardName = model.CreditCardName;
                    o.CreditCardNumber = model.CreditCardNumber;
                    o.CreditCardExpirationDate = new DateTime(model.CreditCardExpirationYear,model.CreditCardExpirationMonth,1);
                    o.CVV = model.CreditCardVerificationValue;
                    entities.Orders.Add(o);
                    entities.SaveChanges();
                }
                    //TODO: Validate the credit card - if it errors out, add a model error and display it to the user
                    //TODO: Persist this information to the database
                    return RedirectToAction("Index", "Receipt", null);
            }
            return View(model);
        }
    }
}
