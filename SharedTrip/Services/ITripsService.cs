using SharedTrip.Models;
using SharedTrip.ViewModels.Trips;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedTrip.Services
{
    public interface ITripsService
    {
        void AddTrip(string startPoint, string endPoint, string departureTime, string imagePath, string seats, string description);

        ICollection<TripViewDetailsModel> GetAllTrips();

        Trip DetailsSelectTrip(string id);

        void AddUserToTheTrip(string userId, string tripId);
    }
}
