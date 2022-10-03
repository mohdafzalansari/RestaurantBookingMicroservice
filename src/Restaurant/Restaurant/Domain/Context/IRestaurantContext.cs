using MongoDB.Driver;

namespace Restaurant.Domain.Context
{
    public interface IRestaurantContext
    {
        IMongoCollection<Restaurant> Restaurants { get; }
        IMongoCollection<Tenant> Tenants { get; }
    }
}
