using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Exceptions
{
    public class ExceptionMessage
    {
        [Description("Detailed error message")]
        public string Message { get; set; }
    }
}
