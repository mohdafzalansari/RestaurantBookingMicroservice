using Reservation.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reservation.Dto
{
    public class GetAllBookingListDto
    {
        public BookingTable BookingTables { get; set; }
        public RestaurantTable RestaurantTables { get; set; }
    }
}
