using Google.Apis.QPXExpress.v1;
using Google.Apis.QPXExpress.v1.Data;
using Google.Apis.Services;
using JinStore.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace JinStore.Controllers
{
    public class ProductsController : Controller
    {
        public ActionResult Index()
        {           

            return View();
        }
        public ActionResult SearchResult()
        {
            //dynamic flightinfo = JsonConvert.DeserializeObject(System.IO.File.ReadAllText(@"C:\Users\Jerry\Desktop\CodingTemple\Capstone Project\PlanetExpress\JinStore\trips.json"));
            //List<Product> Flights = new List<Product>();
            //JsonTextReader reader=new JsonTextReader(new StringReader)

            return View();
        }
    }
}
