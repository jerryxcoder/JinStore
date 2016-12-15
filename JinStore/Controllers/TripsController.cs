using Google.Apis.QPXExpress.v1;
using Google.Apis.QPXExpress.v1.Data;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JinStore.Controllers
{
    public class TripsController : ApiController
    {
        public TripsSearchResponse Get()
        {
            DateTime? departureDate = null;
            string origin = "ORD";
            string destination = "SFO";
            int passengerCount = 1;
            if (!departureDate.HasValue)
            {
                departureDate = DateTime.Now.AddDays(30);
            }
            BaseClientService.Initializer initializer = new BaseClientService.Initializer();
            initializer.ApiKey = "";
            initializer.ApplicationName = "JinFlightSearch";
            QPXExpressService service = new QPXExpressService(initializer);
            SliceInput slice = new SliceInput
            {
                Date = departureDate.Value.ToString("yyyy-MM-dd"),
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
                        AdultCount = passengerCount,
                    },
                    Slice = slices


                }
            };
            TripsResource.SearchRequest r = new TripsResource.SearchRequest(service, request);
            return r.Execute();
        }
    }
}
