using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Reservation.Domain.Context
{
    public class ReservationContextSeed
    {

        public static void SeedRestaurantTableData(IMongoCollection<RestaurantTable> RestaurantTableCollection)
        {
            bool existProduct = RestaurantTableCollection.Find(p => true).Any();
            if (!existProduct)
            {
                RestaurantTableCollection.InsertManyAsync(GetPreconfiguredRestaurantTable());
            }
        }

        private static IEnumerable<RestaurantTable> GetPreconfiguredRestaurantTable()
        {
            return new List<RestaurantTable>()
            {
                new RestaurantTable()
                {
                    Id = Guid.NewGuid(),
                    Name="RestaurantTable A",
                    Location="RestaurantTable Location A",
                    TenantId=1
                },
            };
        }
    }
}
