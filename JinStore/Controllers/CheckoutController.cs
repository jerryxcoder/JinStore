
using JinStore.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CodingTemple.ShoeStore.Mvc.Controllers
{
    public class CheckoutController : Controller
    {
        // GET: Checkout
        public ActionResult Index(Guid? id)
        {

            CheckoutModel model = new CheckoutModel();

            using (MemberEntities1 entities = new MemberEntities1())
            {
                //int orderId = int.Parse(Request.Cookies["TicketID"].Value);
                var order = entities.Carts.Single(x => x.Id == id);
                model.FirstName = order.FirstName;
                model.LastName = order.LastName;
                model.EmailAddress = order.EmailAddress;
                model.CreditCardVerificationValue = order.CVV;
                model.CreditCardExpirationMonth = order.CreditCardExpirationDate.HasValue ? order.CreditCardExpirationDate.Value.Month : 1;
                model.CreditCardExpirationYear = order.CreditCardExpirationDate.HasValue ? order.CreditCardExpirationDate.Value.Year : 1;
                model.CreditCardNumber = order.CreditCardNumber;
                model.CreditCardName = order.CreditCardName;
            
            }


            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(CheckoutModel model)
        {
            using (MemberEntities1 entities = new MemberEntities1())
            {
                //int orderId = int.Parse(Request.Cookies["OrderID"].Value);
                var o = entities.Carts.Single(x => x.EmailAddress == model.EmailAddress);
              
                if (ModelState.IsValid)
                {

                    bool addressValidationSuccessful = true;

                    string smartyStreetsAuthId = ConfigurationManager.AppSettings["SmartyStreets.AuthId"];
                    string smartyStreetsAuthToken = ConfigurationManager.AppSettings["SmartyStreets.AuthToken"];

                    Rentler.SmartyStreets.SmartyStreetsClient client = new Rentler.SmartyStreets.SmartyStreetsClient(smartyStreetsAuthId, smartyStreetsAuthToken);
                    var addresses = await client.GetStreetAddressAsync(model.BillingStreet1, null, model.BillingStreet2, model.BillingCity, model.BillingState, model.BillingPostalCode);
                    if (addresses.Count() == 0)
                    {
                        ModelState.AddModelError("BillingStreet1", "Could not find exact or similiar address");
                        addressValidationSuccessful = false;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(model.BillingStreet1) && addresses.First().delivery_line_1 != model.BillingStreet1)
                        {
                            ModelState.AddModelError("BillingStreet1", string.Format("Suggested Address: {0}", addresses.First().delivery_line_1));
                            addressValidationSuccessful = false;
                        }
                        if (!string.IsNullOrEmpty(model.BillingStreet2) && addresses.First().delivery_line_2 != model.BillingStreet2)
                        {
                            ModelState.AddModelError("BillingStreet2", string.Format("Suggested Address: {0}", addresses.First().delivery_line_2));
                            addressValidationSuccessful = false;
                        }
                        if (!string.IsNullOrEmpty(model.BillingCity) && addresses.First().components.city_name != model.BillingCity)
                        {
                            ModelState.AddModelError("BillingCity", string.Format("Suggested Address: {0}", addresses.First().components.city_name));
                            addressValidationSuccessful = false;
                        }
                        if (!string.IsNullOrEmpty(model.BillingPostalCode) && addresses.First().components.zipcode != model.BillingPostalCode)
                        {
                            ModelState.AddModelError("BillingPostalCode", string.Format("Suggested Address: {0}", addresses.First().components.zipcode));
                            addressValidationSuccessful = false;
                        }
                        if (!string.IsNullOrEmpty(model.BillingState) && addresses.First().components.state_abbreviation != model.BillingState)
                        {
                            ModelState.AddModelError("BillingState", string.Format("Suggested Address: {0}", addresses.First().components.state_abbreviation));
                            addressValidationSuccessful = false;
                        }
                    }
                    if (addressValidationSuccessful)
                    {
                        //TODO: Validate the credit card - if it errors out, add a model error and display it to the user
                        string publicKey = ConfigurationManager.AppSettings["Braintree.PublicKey"];
                        string privateKey = ConfigurationManager.AppSettings["Braintree.PrivateKey"];
                        string environment = ConfigurationManager.AppSettings["Braintree.Environment"];
                        string merchantId = ConfigurationManager.AppSettings["Braintree.MerchantId"];

                        Braintree.BraintreeGateway braintree = new Braintree.BraintreeGateway(environment, merchantId, publicKey, privateKey);
                        Braintree.CustomerRequest request = new Braintree.CustomerRequest();
                        request.Email = model.EmailAddress;
                        request.FirstName = model.FirstName;
                        request.LastName = model.LastName;
                        request.Phone = model.PhoneNumber;
                        request.CreditCard = new Braintree.CreditCardRequest();

                        request.CreditCard.Number = model.CreditCardNumber;
                        request.CreditCard.CardholderName = model.CreditCardName;
                        request.CreditCard.ExpirationMonth = (model.CreditCardExpirationMonth).ToString().PadLeft(2, '0');
                        request.CreditCard.ExpirationYear = model.CreditCardExpirationYear.ToString();


                        var customerResult = braintree.Customer.Create(request);
                        Braintree.TransactionRequest sale = new Braintree.TransactionRequest();
                        //sale.Amount = o.OrderVariants.Sum(x => x.Variant.Product.Price * x.Quantity);
                        sale.CustomerId = customerResult.Target.Id;
                        sale.PaymentMethodToken = customerResult.Target.DefaultPaymentMethod.Token;
                        braintree.Transaction.Sale(sale);


                        o.FirstName = model.FirstName;
                        o.LastName = model.LastName;
                        o.EmailAddress = model.EmailAddress;
                        o.PhoneNumber = model.PhoneNumber;

                        o.BillingCity = model.BillingCity;
                        o.BillingPostalCode = model.BillingPostalCode;
                        o.BillingReceipient = model.BillingReceipient;
                        o.BillingStreet1 = model.BillingStreet1;
                        o.BillingStreet2 = model.BillingStreet2;
                        o.BillingState = model.BillingState;
                        entities.SaveChanges();

                        return RedirectToAction("Index", "Receipt", null);
                    }

                }
            }
            return View(model);
        }
    }
}