using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Domain
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public int TenantId { get; set; }
    }
}
