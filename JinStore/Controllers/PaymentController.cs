using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JinStore.Models
{
    [Authorize]
    public class PaymentsController : Controller
    {
        // GET: Payments
        public ActionResult Index()
        {
            string publicKey = ConfigurationManager.AppSettings["Braintree.PublicKey"];
            string privateKey = ConfigurationManager.AppSettings["Braintree.PrivateKey"];
            string environment = ConfigurationManager.AppSettings["Braintree.Environment"];
            string merchantId = ConfigurationManager.AppSettings["Braintree.MerchantId"];

            Braintree.BraintreeGateway braintree = new Braintree.BraintreeGateway(environment, merchantId, publicKey, privateKey);
            int userId = -1;
            using (MemberEntities1 e = new MemberEntities1())
            {
                userId = e.CustomerLists.Single(x => x.EmailAddress == User.Identity.Name).ID;
            }
            var customer = braintree.Customer.Find(userId.ToString());
            var model = customer.CreditCards.Select(x => new Models.PaymentModel
            {
                CardType = x.CardType.ToString(),
                ExpirationDate = x.ExpirationDate,
                LastFour = x.LastFour,
                ID = x.Token
            }).ToArray();
            return View(model);
        }
    }
}