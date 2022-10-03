using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Infrastructure.Exceptions
{
    public interface IStatusCode
    {
        int StatusCode { get; }
    }
}
