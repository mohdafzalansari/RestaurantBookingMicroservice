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
    public class GetUserByUserNameQuery : IRequest<Domain.User>
    {
        public string UserName { get; set; }
        public int TenantId { get; set; }
    }
    public class GetUserByUserNameHandler : IRequestHandler<GetUserByUserNameQuery, Domain.User>
    {
        readonly ILogger<GetUserByUserNameHandler> logger;
        private readonly IIdentityContext context;


        public GetUserByUserNameHandler(ILogger<GetUserByUserNameHandler> logger, IIdentityContext context)
        {
            this.logger = logger;
            this.context = context;
        }

        public async Task<Domain.User> Handle(GetUserByUserNameQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await context.Users.Find(p => p.UserName == request.UserName && p.TenantId == request.TenantId)
                            .SingleOrDefaultAsync();
                if (result == null)
                {
                    throw new UserNotFoundException();
                }
                return result;
            }
            catch (Exception e)
            {
                this.logger.LogError(e.ToString());
                throw;
            }
        }
    }

}
