using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Identity.Domain.Context
{
    public class IdentityContextSeed
    {

        public static void SeedUserData(IMongoCollection<User> UserCollection)
        {
            bool existProduct = UserCollection.Find(p => true).Any();
            if (!existProduct)
            {
                UserCollection.InsertManyAsync(GetPreconfiguredUser());
            }
        }

        private static IEnumerable<User> GetPreconfiguredUser()
        {
            return new List<User>()
            {
                new User()
                {
                    Id = Guid.NewGuid(),
                    UserName="admin1@gmail.com",
                    Email="admin1@gmail.com",
                    Age=27,
                    CreatedOn=DateTime.Now,
                    Gender="Male",
                    IsActive=true,
                    IsDeleted=false,
                    MobileNumber="1234567891",
                    Role=Role.Admin,
                    PasswordHash="123",
                    TenantId=1
                },
                new User()
                {
                    Id = Guid.NewGuid(),
                    UserName="user1@gmail.com",
                    Email="user1@gmail.com",
                    Age=27,
                    CreatedOn=DateTime.Now,
                    Gender="Male",
                    IsActive=true,
                    IsDeleted=false,
                    MobileNumber="1234567891",
                    Role=Role.User,
                    PasswordHash="123",
                    TenantId=1
                },
                new User()
                {
                    Id = Guid.NewGuid(),
                    UserName="customer@gmail.com",
                    Email="customer@gmail.com",
                    Age=27,
                    CreatedOn=DateTime.Now,
                    Gender="Male",
                    IsActive=true,
                    IsDeleted=false,
                    MobileNumber="1234567891",
                    Role=Role.Customer,
                    PasswordHash="123",
                    TenantId=1
                },
            };
        }
    }
}
