using Identity.Domain;
using Identity.Handlers;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;
using MediatR;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Identity.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IMediator mediator;

        public ProfileService(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            try
            {
                //This will use for role based authentication.
                //you have to add the roles in claim
                var claims = context.Subject.Claims.ToList();
                claims.Add(new Claim(context.Subject.Identities.First().RoleClaimType, Role.Admin));
                claims.Add(new Claim(context.Subject.Identities.First().RoleClaimType, Role.User));
                claims.Add(new Claim(context.Subject.Identities.First().RoleClaimType, Role.Customer));
                context.IssuedClaims = claims;
            }
            catch (Exception ex)
            {
                //ToDo
            }
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            try
            {
                var userIdClaim = context.Subject.Claims.FirstOrDefault(x => x.Type == JwtClaimTypes.Subject);

                if (string.IsNullOrEmpty(userIdClaim?.Value))
                    return;

                var userId = Convert.ToString(userIdClaim.Value);

                var user = await mediator.Send(new GetUserByUserIdQuery { Id = new Guid(userId) });
                context.IsActive = user != null;
            }
            catch (Exception ex)
            {
                //ToDo
            }
        }
    }
}
