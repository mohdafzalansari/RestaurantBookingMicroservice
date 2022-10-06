using MongoDB.Driver;

namespace Reservation.Domain.Context
{
    public interface IReservationContext
    {
        IMongoCollection<RestaurantTable> RestaurantTables { get; }
        IMongoCollection<BookingTable> BookingTables { get; }

    }
}
