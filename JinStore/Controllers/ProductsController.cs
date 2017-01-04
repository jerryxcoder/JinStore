using Google.Apis.QPXExpress.v1;
using Google.Apis.QPXExpress.v1.Data;
using Google.Apis.Services;
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
            return View();
        }
    }
}
