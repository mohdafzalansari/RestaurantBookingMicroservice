using Identity.Handlers;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using MediatR;
using System;
using System.Threading.Tasks;

namespace Identity.Services
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IMediator mediator;

        public ResourceOwnerPasswordValidator(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {

            try
            {
                var tenantId = context.Request.Raw.Get("TenantId");
                if (string.IsNullOrEmpty(tenantId))
                {
                    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid username , password or tenantId");
                    return;
                }

                var existingUser = await mediator.Send(new GetUserByUserNameQuery() { UserName = context.UserName, TenantId = Convert.ToInt32(tenantId) });
                if (existingUser == null)
                {
                    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid username or password");
                    return;
                }

                if (!await mediator.Send(new ValidateUserQuery() { UserName = context.UserName, Password = context.Password }))
                {
                    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid username or password");
                    return;
                }

                context.Result = new GrantValidationResult(existingUser.Id.ToString(), "password", existingUser.ToClaims());
            }
            catch (Exception)
            {
                //TODO
                throw;
            }
        }
    }
}
