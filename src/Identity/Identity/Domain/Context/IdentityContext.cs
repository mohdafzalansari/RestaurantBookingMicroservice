using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Identity.Domain.Context
{
    public class IdentityContext : IIdentityContext
    {
        public IdentityContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));
            Users = database.GetCollection<User>("Users");
            IdentityContextSeed.SeedUserData(Users);
        }

        public IMongoCollection<User> Users { get; }
    }
}
