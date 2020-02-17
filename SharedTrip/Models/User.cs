using SIS.MvcFramework;
using System;
using System.Collections;
using System.Collections.Generic;

namespace SharedTrip.Models
{
    public class User : IdentityUser<string>
    {
        public User()
        {
            this.Id = Guid.NewGuid().ToString();
            this.UserTrips = new HashSet<UserTrip>();
        }

        public virtual ICollection<UserTrip> UserTrips { get; set; }
    }
}
