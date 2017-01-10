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
        public ActionResult Index()
        {

            CheckoutModel model = new CheckoutModel();

            //using (MemberEntities1 entities = new MemberEntities1())
            //{
            //    int orderId = int.Parse(Request.Cookies["OrderID"].Value);
            //    var order = entities.Orders.Single(x => x.Id == orderId);
            //    model.FirstName = order.FirstName;
            //    model.LastName = order.LastName;
            //    model.EmailAddress = order.EmailAddress;
            //    model.CreditCardVerificationValue = order.CVV;
            //    model.CreditCardExpirationMonth = order.CreditCardExpirationDate.HasValue ? order.CreditCardExpirationDate.Value.Month : 1;
            //    model.CreditCardExpirationYear = order.CreditCardExpirationDate.HasValue ? order.CreditCardExpirationDate.Value.Year : 1;
            //    model.CreditCardNumber = order.CreditCardNumber;
            //    model.CreditCardName = order.CreditCardName;
            //    model.LineItems = order.OrderVariants.Select(x => new LineItemModel
            //    {
            //        Color = x.Color,
            //        Size = x.Size,
            //        Name = x.Variant.Product.ProductName,
            //        Quantity = x.Quantity

            //    }).ToArray();
            //}


            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(CheckoutModel model)
        {
            using (MemberEntities1 entities = new MemberEntities1())
            {
                int orderId = int.Parse(Request.Cookies["OrderID"].Value);
                var o = entities.Orders.Single(x => x.Id == orderId);
                //model.LineItems = o.OrderVariants.Select(x => new LineItemModel
                //{
                //    Color = x.Color,
                //    Size = x.Size,
                //    Name = x.Variant.Product.ProductName,
                //    Quantity = x.Quantity

                //}).ToArray();
                if (ModelState.IsValid)
                {
                    //TODO: Validate the address
                    bool addressValidationSuccessful = true;

                    string smartyStreetsAuthId = ConfigurationManager.AppSettings["SmartyStreets.AuthId"];
                    string smartyStreetsAuthToken = ConfigurationManager.AppSettings["SmartyStreets.AuthToken"];

                    Rentler.SmartyStreets.SmartyStreetsClient client = new Rentler.SmartyStreets.SmartyStreetsClient(smartyStreetsAuthId, smartyStreetsAuthToken);
                    var addresses = await client.GetStreetAddressAsync(model.BillingStreet1, null, model.BillingStreet2, model.BillingCity, model.BillingState, model.BillingPostalCode);
                    if (addresses.Count() == 0)
                    {
                        ModelState.AddModelError("ShippingStreet1", "Could not find exact or similiar address");
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

                        o.FirstName = model.FirstName;
                        o.LastName = model.LastName;
                        o.EmailAddress = model.EmailAddress;
                        o.PhoneNumber = model.PhoneNumber;
                        o.CreditCardName = model.CreditCardName;
                        o.CVV = model.CreditCardVerificationValue;
                        o.CreditCardExpirationDate = new DateTime(model.CreditCardExpirationYear, model.CreditCardExpirationMonth, 1);
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