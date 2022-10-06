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
    public class GetUserByUserIdQuery : IRequest<Domain.User>
    {
        public Guid Id { get; set; }
    }
    public class GetUserByUserIdHandler : IRequestHandler<GetUserByUserIdQuery, Domain.User>
    {
        readonly ILogger<GetUserByUserIdHandler> logger;
        private readonly IIdentityContext context;


        public GetUserByUserIdHandler(ILogger<GetUserByUserIdHandler> logger, IIdentityContext context)
        {
            this.logger = logger;
            this.context = context;
        }

        public async Task<Domain.User> Handle(GetUserByUserIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await context.Users.Find(p => p.Id == request.Id)
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
