using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Restaurant.Domain
{
    public class BaseEntity
    {
        [BsonId]
        public Guid Id { get; set; }
        public int TenantId { get; set; }
    }
}
