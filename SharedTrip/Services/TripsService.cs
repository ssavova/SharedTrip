using SharedTrip.Models;
using SharedTrip.ViewModels.Trips;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SharedTrip.Services
{
    public class TripsService : ITripsService
    {
        private readonly ApplicationDbContext db;
        public TripsService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public void AddTrip(string startPoint, string endPoint, string departureTime,string imagePath,string seats,string description)
        {
            string s = departureTime;

            DateTime dt =
                DateTime.ParseExact(s, "MM/dd/yyyy HH:mm", CultureInfo.InvariantCulture);

            int parsedSeats = int.Parse(seats);

            Trip trip = new Trip
            {
                StartPoint = startPoint,
                EndPoint = endPoint,
                DepartureTime = dt,
                ImagePath = imagePath,
                Seats = parsedSeats,
                Description = description,
            };

            this.db.Trips.Add(trip);
            this.db.SaveChanges();
        }

        
        public ICollection<TripViewDetailsModel> GetAllTrips()
        {
            var trips = this.db.Trips.Select(p => new TripViewDetailsModel
            {
                Id = p.Id,
                StartPoint = p.StartPoint,
                EndPoint = p.EndPoint,
                DepartureTime = p.DepartureTime,
                Seats = p.Seats
            }).ToList();

            return trips;
        }

        public Trip DetailsSelectTrip(string tripId)
        {
            return this.db.Trips.Where(t => t.Id == tripId).FirstOrDefault();
        }

        public void AddUserToTheTrip(string userId, string tripId)
        {
            var selectedTrip = this.DetailsSelectTrip(tripId);
            selectedTrip.UserTrips.Add(new UserTrip
            {
                UserId = userId,
                TripId = tripId
            });

            var currentSeats = selectedTrip.Seats;

            if (currentSeats != 0)
            {
                selectedTrip.Seats = currentSeats - 1;
            }

            this.db.Trips.Update(selectedTrip);
            this.db.SaveChanges();
            
        }
       
    }
}
