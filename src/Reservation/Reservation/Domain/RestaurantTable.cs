using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reservation.Domain
{
    public class RestaurantTable : BaseEntity
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string NumberOfSits { get; set; }
    }
}
