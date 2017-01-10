using Google.Apis.QPXExpress.v1;
using Google.Apis.QPXExpress.v1.Data;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JinStore.Models;
using System.Web.Mvc;
using System.Configuration;

namespace JinStore.Controllers
{
    public class TripsController : ApiController
    {
        public TripsSearchResponse Get(string origin = "", string destination = "", DateTime? departureTime = null, int? adultCount = null)
        {
            if (string.IsNullOrEmpty(origin))
            {
                origin = "ORD";
            }
            if (string.IsNullOrEmpty(destination))
            {
                destination = "SFO";
            }

            if (departureTime == null)
            {
                departureTime = DateTime.Now.AddDays(30);
            }
            if(adultCount == null)
            {
                adultCount = 1;
            }


            BaseClientService.Initializer initializer = new BaseClientService.Initializer();
            initializer.ApiKey = ConfigurationManager.AppSettings["QPX.Key"];
            initializer.ApplicationName = "JinFlightSearch";
            QPXExpressService service = new QPXExpressService(initializer);
            SliceInput slice = new SliceInput
            {
                Date = departureTime.Value.ToString("yyyy-MM-dd"),
                Origin = origin,
                Destination = destination,
            };
            List<SliceInput> slices = new List<SliceInput>();
            slices.Add(slice);
            TripsSearchRequest request = new TripsSearchRequest
            {
                Request = new TripOptionsRequest
                {
                    Passengers = new PassengerCounts
                    {
                        AdultCount = adultCount,
                    },
                    Slice = slices


                }
            };
            TripsResource.SearchRequest r = new TripsResource.SearchRequest(service, request);
            return r.Execute();
        }
        
        [System.Web.Mvc.HttpPut]
        public Guid PutTrip(string id = "", string from = "")
        {
            Cart ticket = new Cart();
            ticket.origin = from;
            ticket.TicketID = id;
            ticket.Id = Guid.NewGuid();
             //TODO: save selected trip details to cart
            using (MemberEntities1 entities = new MemberEntities1())
            {
                entities.Carts.Add(ticket);
                entities.SaveChanges();
            }
            


            return ticket.Id;
        }

   
    }
}
