using Microsoft.AspNetCore.Authorization;
using Reservation.authPolicy.requirement;
using Reservation.CommonUtility.Constants;
using System.Linq;
using System.Threading.Tasks;

namespace Reservation.authPolicy.handler
{
    public class PermissionHandler : IAuthorizationHandler
    {
        private string role = string.Empty;
        private string[] roles;
        public Task HandleAsync(AuthorizationHandlerContext context)
        {
            if (context.User.HasClaim(c => c.Type == AppConstants.RoleType))
            {
                role = context.User.Claims.FirstOrDefault(d => d.Type == AppConstants.RoleType).Value;
            }

            if (string.IsNullOrEmpty(role))
                return Task.CompletedTask;


            roles = role.Split(',');

            var pendingRequirements = context.PendingRequirements.ToList();

            foreach (var requirement in pendingRequirements)
            {
                switch (requirement)
                {
                    case CustomerRolePermission customerRole:
                        if (IsInRole(AppConstants.Customer))
                        {
                            context.Succeed(customerRole);
                        }
                        break;
                    case UserRolePermission userRole:
                        if (IsInRole(AppConstants.User))
                        {
                            context.Succeed(userRole);
                        }
                        break;
                    case AdminRolePermission adminRole:
                        if (IsInRole(AppConstants.Admin))
                        {
                            context.Succeed(adminRole);
                        }
                        break;
                    default:
                        return Task.CompletedTask;
                }

            }
            return Task.CompletedTask;
        }

        private bool IsInRole(params string[] role)
        {
            return roles.Intersect(role).Count() > 0;
        }
    }
}
