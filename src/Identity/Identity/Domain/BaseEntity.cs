using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Identity.Domain
{
    public class BaseEntity
    {
        [BsonId]
        public Guid Id { get; set; }
        public int TenantId { get; set; }
    }
}
