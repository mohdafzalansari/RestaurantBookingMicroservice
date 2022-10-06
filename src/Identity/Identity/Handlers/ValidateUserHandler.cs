using AutoMapper.Configuration;
using Identity.Domain.Context;
using Identity.Infrastructure.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.Handlers
{
    public class ValidateUserQuery : IRequest<bool>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class ValidateUserHandler : IRequestHandler<ValidateUserQuery, bool>
    {
        readonly ILogger<ValidateUserHandler> logger;
        private readonly IIdentityContext context;


        public ValidateUserHandler(ILogger<ValidateUserHandler> logger, IIdentityContext context)
        {
            this.logger = logger;
            this.context = context;
        }

        public async Task<bool> Handle(ValidateUserQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await context.Users.Find(u => u.UserName == request.UserName && u.PasswordHash == request.Password && u.IsActive == true && u.IsDeleted == false)
                            .SingleOrDefaultAsync();
                if (result == null)
                {
                   return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                this.logger.LogError(e.ToString());
                throw;
            }
        }
    }

}
