﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SharedTrip.ViewModels.Trips
{
    public class SelectedTripViewModel
    {
        public string Id { get; set; }
        public string StartPoint { get; set; }

        public string ImagePath { get; set; }
        public string EndPoint { get; set; }

        public string DepartureTime { get; set; }

        public int Seats { get; set; }

        public string Description { get; set; }
    }
}
