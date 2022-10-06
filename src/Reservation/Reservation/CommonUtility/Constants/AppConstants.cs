using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reservation.CommonUtility.Constants
{
    public class AppConstants
    {
        public const string RoleType = "role";

        //User roles
        public const string Customer = "customer";
        public const string User = "user";
        public const string Admin = "admin";

        //User roles policy
        public const string UserPolicy = "UserPolicy";
        public const string CustomerPolicy = "CustomerPolicy";
        public const string AdminPolicy = "AdminPolicy";
    }
}
