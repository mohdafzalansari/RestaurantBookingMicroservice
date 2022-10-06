using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reservation.Controllers
{
    public class BaseController : ControllerBase
    {
        protected int GetTenantId()
        {
            return int.Parse(this.User.Claims.First(i => i.Type == "TenantId").Value);
        }
    }
}
