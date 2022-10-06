using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Reservation.Domain.Context
{
    public class ReservationContext : IReservationContext
    {
        public ReservationContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));
            RestaurantTables = database.GetCollection<RestaurantTable>("RestaurantTables");
            BookingTables = database.GetCollection<BookingTable>("BookingTables");
            ReservationContextSeed.SeedRestaurantTableData(RestaurantTables);
        }

        public IMongoCollection<RestaurantTable> RestaurantTables { get; }
        public IMongoCollection<BookingTable> BookingTables { get; }
    }
}
