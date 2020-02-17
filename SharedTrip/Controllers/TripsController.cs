using SharedTrip.Services;
using SharedTrip.ViewModels.Trips;
using SIS.HTTP;
using SIS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedTrip.Controllers
{
    public class TripsController:Controller
    {
        private readonly ITripsService tripsService;
        public TripsController(ITripsService service)
        {
            this.tripsService = service;
        }
        public HttpResponse All()
        {
            var collection = this.tripsService.GetAllTrips();

            var viewModel = new TripViewModel
            {
                Trips = collection
            };

            return this.View(viewModel,"All");
        }


        public HttpResponse Add()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(CreateTripInputModel input)
        {
            if (!this.IsUserLoggedIn())
            {
                return Redirect("/Users/Login");
            }

            if (int.Parse(input.Seats) < 2 || int.Parse(input.Seats) > 6)
            {
                return this.Error("Seats must be in range 2-6");
            }


            if (input.Description.Length > 80)
            {
                return this.Error("Description must be at most 80 characters");
            }

            this.tripsService.AddTrip(input.StartPoint, input.EndPoint, input.DepartureTime, input.ImagePath, input.Seats, input.Description);

            return this.Redirect("/Trips/All");
        }

        public HttpResponse Details(string tripId)
        {
            var selectedTrip = this.tripsService.DetailsSelectTrip(tripId);

            string dateTime = selectedTrip.DepartureTime.ToString("MM/dd/yyyy HH:mm");

            var viewModel = new SelectedTripViewModel()
            {
                Id = selectedTrip.Id,
                ImagePath = selectedTrip.ImagePath,
                StartPoint = selectedTrip.StartPoint,
                EndPoint = selectedTrip.EndPoint,
                DepartureTime = dateTime,
                Seats = selectedTrip.Seats,
                Description = selectedTrip.Description,

            };
            return this.View(viewModel);
        }

        public HttpResponse AddUserToTrip(string tripId)
        {
            var userId = this.User;
            this.tripsService.AddUserToTheTrip(this.User, tripId);

            return this.Redirect("/");
        }

    }
}
