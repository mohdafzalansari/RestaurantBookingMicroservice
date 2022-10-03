using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Restaurant.Commands;
using Restaurant.Domain.Context;
using Restaurant.Infrastructure.Exceptions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurant.Handlers
{
    public class CreateRestaurantHandler : IRequestHandler<CreateRestaurantCommond, CreateRestaurantResponse>
    {
        readonly ILogger<CreateRestaurantHandler> logger;
        private readonly IRestaurantContext context;
        readonly IMediator mediator;
        readonly IConfiguration configuration;


        public CreateRestaurantHandler(IMediator mediator, IConfiguration configuration, ILogger<CreateRestaurantHandler> logger, IRestaurantContext context)
        {
            this.mediator = mediator;
            this.configuration = configuration;
            this.logger = logger;
            this.context = context;
        }

        public async Task<CreateRestaurantResponse> Handle(CreateRestaurantCommond request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                    throw new RestaurantBadRequestException();

                await context.Restaurants.InsertOneAsync(new Domain.Restaurant
                {
                    Name = request.Name,
                    Address = request.Address,
                    City = request.City,
                    Country = request.Country,
                    PinCode = request.PinCode,
                    TenantId = request.TenantId
                });

                return new CreateRestaurantResponse();
            }
            catch (Exception e)
            {
                this.logger.LogError(e.ToString());
                throw;
            }
        }
    }

}
