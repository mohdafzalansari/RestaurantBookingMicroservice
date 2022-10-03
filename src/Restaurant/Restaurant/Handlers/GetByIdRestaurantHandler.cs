using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Restaurant.Domain.Context;
using Restaurant.Infrastructure.Exceptions;
using Restaurant.Queries;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurant.Handlers
{
    public class GetByIdRestaurantHandler : IRequestHandler<GetByIdRestaurantQuery, Domain.Restaurant>
    {
        readonly ILogger<GetByIdRestaurantHandler> logger;
        private readonly IRestaurantContext context;
        readonly IMediator mediator;
        readonly IConfiguration configuration;


        public GetByIdRestaurantHandler(IMediator mediator, IConfiguration configuration, ILogger<GetByIdRestaurantHandler> logger, IRestaurantContext context)
        {
            this.mediator = mediator;
            this.configuration = configuration;
            this.logger = logger;
            this.context = context;
        }

        public async Task<Domain.Restaurant> Handle(GetByIdRestaurantQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await context.Restaurants.Find(p => p.Id == request.Id)
                            .SingleOrDefaultAsync();
                if (result == null)
                {
                    throw new RestaurantNotFoundException(request.Id);
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
