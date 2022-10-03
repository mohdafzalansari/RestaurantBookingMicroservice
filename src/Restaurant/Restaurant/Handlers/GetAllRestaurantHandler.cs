using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Restaurant.Domain.Context;
using Restaurant.Queries;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurant.Handlers
{
    public class GetAllRestaurantHandler : IRequestHandler<GetAllRestaurantQuery, IEnumerable<Domain.Restaurant>>
    {
        readonly ILogger<GetAllRestaurantHandler> logger;
        private readonly IRestaurantContext context;
        readonly IMediator mediator;
        readonly IConfiguration configuration;


        public GetAllRestaurantHandler(IMediator mediator, IConfiguration configuration, ILogger<GetAllRestaurantHandler> logger, IRestaurantContext context)
        {
            this.mediator = mediator;
            this.configuration = configuration;
            this.logger = logger;
            this.context = context;
        }

        public async Task<IEnumerable<Domain.Restaurant>> Handle(GetAllRestaurantQuery request, CancellationToken cancellationToken)
        {
            try
            {
                //logic to save the data into database

                //return the generated id
                return await context.Restaurants.Find(p => true)
                            .ToListAsync();
            }
            catch (Exception e)
            {
                this.logger.LogError(e.ToString());
                throw;
            }
        }
    }

}
