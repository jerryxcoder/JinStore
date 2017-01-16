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

                    model.TicketId = order.TicketId;
                    model.origin = order.origin;
                    model.destination = order.destination;
                    model.departureTime = order.departureTime;
                    model.arrivalTime = order.arrivalTime;
                    model.Id = order.Id;
                    model.saleTotal = order.saleTotal;
                    model.stops = order.stops;
                    model.carrier = order.carrier;
                    model.number = order.number;
                    model.adultCount = order.adultCount;
                }

            }
             return View(model);
        }
        
    }
}