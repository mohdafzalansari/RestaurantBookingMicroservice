using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Restaurant.Domain.Context
{
    public class RestaurantContext : IRestaurantContext
    {
        public RestaurantContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));
            Tenants = database.GetCollection<Tenant>("Tenants");
            RestaurantContextSeed.SeedTenantData(Tenants);
            Restaurants = database.GetCollection<Restaurant>("Restaurants");
            RestaurantContextSeed.SeedRestaurantData(Restaurants);
        }

        public IMongoCollection<Restaurant> Restaurants { get; }
        public IMongoCollection<Tenant> Tenants { get; }

    }
}
