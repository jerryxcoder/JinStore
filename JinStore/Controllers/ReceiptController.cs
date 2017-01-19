using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JinStore.Models;

namespace JinStore.Controllers
{
    public class ReceiptController : Controller
    {
        // GET: Receipt
        
        public ActionResult Index(Guid? id)
        {
            ReceiptModel receipt = new ReceiptModel();
            using (MemberEntities1 entities = new MemberEntities1())
            {
                var r = entities.Orders.Single(x => x.OrderId == id);
                receipt.FirstName = r.FirstName;
                receipt.LastName = r.LastName;
                receipt.saleTotal = r.Cart.saleTotal;
                receipt.arrivalTime = r.Cart.arrivalTime;
                receipt.departureTime = r.Cart.departureTime;
                receipt.origin = r.Cart.origin;
                receipt.destination = r.Cart.destination;
                receipt.carrier = r.Cart.carrier;
                receipt.number = r.Cart.number;
                receipt.stops = r.Cart.stops;
                receipt.BillingStreet1 = r.BillingStreet1;
                receipt.BillingStreet2 = r.BillingStreet2;
                receipt.BillingCity = r.BillingCity;
                receipt.BillingState = r.BillingState;
                receipt.BillingPostalCode = r.BillingPostalCode;
                receipt.InvoiceDate = r.DateLastModified;
            }

            return View(receipt);
        }
    }
}