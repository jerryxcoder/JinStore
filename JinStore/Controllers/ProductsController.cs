using JinStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;


namespace JinStore.Controllers
{
    public class ProductsController : Controller
    {

        public ActionResult Index()
        {
            if (products.Count == 0)
            {
                products.Add(new Product { TicketID = 1, Class = "First", Region = "Midwest"});
                products.Add(new Product { TicketID = 2, Class = "First", Region = "Northeast" });
                products.Add(new Product { TicketID = 3, Class = "Second", Region = "Southeast" });
                products.Add(new Product { TicketID = 4, Class = "Third", Region = "Southwest" });
                products.Add(new Product { TicketID = 5, Class = "Second", Region = "Northeast" });
            }
            return View(products);
        }

        private static List<Product> products = new List<Product>();

        public ActionResult Edit(int? id)
        {
            if (id.HasValue && products.Any(x => x.TicketID == id.Value))
            {
                return View(products.Single(x => x.TicketID == id.Value));
            }

            return HttpNotFound("No product found");
        }
        [System.Web.Mvc.HttpPost]
        public ActionResult Edit(int id, Product product)
        {
            Product p = products.First(x => x.TicketID == id);
            p.TicketID = product.TicketID;
            p.Class = product.Class;
            p.Region = product.Region;
            return View(p);
        }
    }
}
