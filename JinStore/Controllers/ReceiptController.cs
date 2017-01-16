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
            }

            return View(receipt);
        }
    }
}