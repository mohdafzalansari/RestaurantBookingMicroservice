using System;

namespace Reservation.Domain
{
    public class BookingTable: BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime BookingDateTime { get; set; }
        public int NumberOfVisitors { get; set; }
        public Guid RestaurantTableId { get; set; }
    }
}
