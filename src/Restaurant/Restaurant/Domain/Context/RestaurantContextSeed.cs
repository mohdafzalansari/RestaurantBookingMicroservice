using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Restaurant.Domain.Context
{
    public class RestaurantContextSeed
    {
        public static void SeedTenantData(IMongoCollection<Tenant> restaurantCollection)
        {
            bool existProduct = restaurantCollection.Find(p => true).Any();
            if (!existProduct)
            {
                restaurantCollection.InsertManyAsync(GetPreconfiguredTenants());
            }
        }

        private static IEnumerable<Tenant> GetPreconfiguredTenants()
        {
            return new List<Tenant>()
            {
                new Tenant()
                {
                    Id=1,
                    Name = "Tenant A",
                },
                new Tenant()
                {
                    Id=2,
                    Name = "Tenant B",
                },
            };
        }

        public static void SeedRestaurantData(IMongoCollection<Restaurant> restaurantCollection)
        {
            bool existProduct = restaurantCollection.Find(p => true).Any();
            if (!existProduct)
            {
                restaurantCollection.InsertManyAsync(GetPreconfiguredRestaurant());
            }
        }

        private static IEnumerable<Restaurant> GetPreconfiguredRestaurant()
        {
            return new List<Restaurant>()
            {
                new Restaurant()
                {
                    Id = Guid.NewGuid(),
                    Name = "Restaurant A",
                    Address="Restaurant A Address",
                    City="Restaurant A Address",
                    Country = "Restaurant A Address",
                    PinCode="123456",
                    TenantId=1
                },
                new Restaurant()
                {
                    Id = Guid.NewGuid(),
                    Name = "Restaurant B",
                    Address="Restaurant B Address",
                    City="Restaurant B Address",
                    Country = "Restaurant B Address",
                    PinCode="123453",
                    TenantId=2
                },
            };
        }
    }
}
