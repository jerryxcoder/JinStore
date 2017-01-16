
using JinStore.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace JinStore.Controllers
{
    public class CheckoutController : Controller
    {
        // GET: Checkout
        public ActionResult Index(Guid? id)
        {

            CheckoutModel model2 = new CheckoutModel();

            using (MemberEntities1 entities = new MemberEntities1())
            {
                 
                var order = entities.Carts.Single(x => x.Id == id);

                //model2.FirstName = order.FirstName;
                // model2.LastName = order.LastName;
                //model2.EmailAddress = order.EmailAddress;
                //model2.CreditCardVerificationValue = order.CVV;
                //model2.CreditCardExpirationMonth = order.CreditCardExpirationDate.HasValue ? order.CreditCardExpirationDate.Value.Month : 1;
                //model2.CreditCardExpirationYear = order.CreditCardExpirationDate.HasValue ? order.CreditCardExpirationDate.Value.Year : 1;
                //model2.CreditCardNumber = order.CreditCardNumber;
                //model2.CreditCardName = order.CreditCardName;
                //model2.ticketID = order.TicketId;
                model2.origin = order.origin;
                model2.destination = order.destination;
                model2.departureTime = order.departureTime;
                model2.arrivalTime = order.arrivalTime;
                //model2.CartItem = order.Cart.se(x => new CartModel
                //{
                //    origin = x.origin,
                //    destination = x.destination,
                //    departureTime = x.departureTime,
                //    arrivalTime = x.arrivalTime

                //}).ToArray();
            }


            return View(model2);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(CheckoutModel model2)
        {
            using (MemberEntities1 entities = new MemberEntities1())
            {
                //int orderId = int.Parse(Request.Cookies["OrderID"].Value);
                var cart = entities.Carts.Single(x => x.Id == model2.id);
                Order o = new Order();
                cart.Orders.Add(o);
              
                if (ModelState.IsValid)
                {

                    bool addressValidationSuccessful = true;
                    bool validateAddress = false;

                    string smartyStreetsAuthId = ConfigurationManager.AppSettings["SmartyStreets.AuthId"];
                    string smartyStreetsAuthToken = ConfigurationManager.AppSettings["SmartyStreets.AuthToken"];

                    Rentler.SmartyStreets.SmartyStreetsClient client = new Rentler.SmartyStreets.SmartyStreetsClient(smartyStreetsAuthId, smartyStreetsAuthToken);
                    var addresses = await client.GetStreetAddressAsync(model2.BillingStreet1, null, model2.BillingStreet2, model2.BillingCity, model2.BillingState, model2.BillingPostalCode);
                    if (addresses.Count() == 0)
                    {
                        ModelState.AddModelError("BillingStreet1", "Could not find exact or similiar address");
                        addressValidationSuccessful = false;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(model2.BillingStreet1) && addresses.First().delivery_line_1 != model2.BillingStreet1)
                        {
                            ModelState.AddModelError("BillingStreet1", string.Format("Suggested Address: {0}", addresses.First().delivery_line_1));
                            addressValidationSuccessful = false;
                        }
                        if (!string.IsNullOrEmpty(model2.BillingStreet2) && addresses.First().delivery_line_2 != model2.BillingStreet2)
                        {
                            ModelState.AddModelError("BillingStreet2", string.Format("Suggested Address: {0}", addresses.First().delivery_line_2));
                            addressValidationSuccessful = false;
                        }
                        if (!string.IsNullOrEmpty(model2.BillingCity) && addresses.First().components.city_name != model2.BillingCity)
                        {
                            ModelState.AddModelError("BillingCity", string.Format("Suggested Address: {0}", addresses.First().components.city_name));
                            addressValidationSuccessful = false;
                        }
                        if (!string.IsNullOrEmpty(model2.BillingPostalCode) && addresses.First().components.zipcode != model2.BillingPostalCode)
                        {
                            ModelState.AddModelError("BillingPostalCode", string.Format("Suggested Address: {0}", addresses.First().components.zipcode));
                            addressValidationSuccessful = false;
                        }
                        if (!string.IsNullOrEmpty(model2.BillingState) && addresses.First().components.state_abbreviation != model2.BillingState)
                        {
                            ModelState.AddModelError("BillingState", string.Format("Suggested Address: {0}", addresses.First().components.state_abbreviation));
                            addressValidationSuccessful = false;
                        }
                    }
                    if (addressValidationSuccessful || !validateAddress)
                    {
                        //TODO: Validate the credit card - if it errors out, add a model error and display it to the user
                        string publicKey = ConfigurationManager.AppSettings["Braintree.PublicKey"];
                        string privateKey = ConfigurationManager.AppSettings["Braintree.PrivateKey"];
                        string environment = ConfigurationManager.AppSettings["Braintree.Environment"];
                        string merchantId = ConfigurationManager.AppSettings["Braintree.MerchantId"];

                        Braintree.BraintreeGateway braintree = new Braintree.BraintreeGateway(environment, merchantId, publicKey, privateKey);
                        Braintree.CustomerRequest request = new Braintree.CustomerRequest();
                        request.Email = model2.EmailAddress;
                        request.FirstName = model2.FirstName;
                        request.LastName = model2.LastName;
                        request.Phone = model2.PhoneNumber;
                        request.CreditCard = new Braintree.CreditCardRequest();

                        request.CreditCard.Number = model2.CreditCardNumber;
                        request.CreditCard.CardholderName = model2.CreditCardName;
                        request.CreditCard.ExpirationMonth = (model2.CreditCardExpirationMonth).ToString().PadLeft(2, '0');
                        request.CreditCard.ExpirationYear = model2.CreditCardExpirationYear.ToString();


                        var customerResult = braintree.Customer.Create(request);
                        Braintree.TransactionRequest sale = new Braintree.TransactionRequest();
                        sale.Amount = decimal.Parse(cart.saleTotal.Replace("USD", string.Empty));
                        sale.CustomerId = customerResult.Target.Id;
                        sale.PaymentMethodToken = customerResult.Target.DefaultPaymentMethod.Token;
                        braintree.Transaction.Sale(sale);


                        o.FirstName = model2.FirstName;
                        o.LastName = model2.LastName;
                        o.EmailAddress = model2.EmailAddress;
                        o.PhoneNumber = model2.PhoneNumber;

                        o.BillingCity = model2.BillingCity;
                        o.BillingPostalCode = model2.BillingPostalCode;
                        o.BillingReceipient = model2.BillingReceipient;
                        o.BillingStreet1 = model2.BillingStreet1;
                        o.BillingStreet2 = model2.BillingStreet2;
                        o.BillingState = model2.BillingState;
                        o.DateCreated = DateTime.UtcNow;
                        o.DateLastModified = DateTime.UtcNow;
                        entities.SaveChanges();

                        return RedirectToAction("Index", "Receipt", new { id = o.OrderId });
                    }

                }
            }
            return View(model2);
        }
    }
}