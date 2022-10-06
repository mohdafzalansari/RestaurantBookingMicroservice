using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reservation.Domain
{
    public class BaseEntity
    {
        [BsonId]
        public Guid Id { get; set; }
        public int TenantId { get; set; }
    }
}
