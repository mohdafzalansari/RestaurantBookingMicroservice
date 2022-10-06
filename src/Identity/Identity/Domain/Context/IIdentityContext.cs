using MongoDB.Driver;

namespace Identity.Domain.Context
{
    public interface IIdentityContext
    {
        IMongoCollection<User> Users { get; }

    }
}
