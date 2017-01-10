using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JinStore.Models;

namespace JinStore.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index(Guid? id)
        {
            Cart model = new Cart();
            if (id.HasValue)
            {
                using (MemberEntities1 entities = new MemberEntities1())
                {

                    var order = entities.Carts.Single(x => x.Id == id);

                    model.TicketID = order.TicketID;
                    model.origin = order.origin;
                }

            }
            return View(model);
        }
    }
}