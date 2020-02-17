using System;
using System.Collections.Generic;
using System.Text;

namespace SharedTrip.ViewModels.Trips
{
    public class TripViewModel
    {
        public IEnumerable<TripViewDetailsModel> Trips { get; set; }
    }
}
