using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Restaurant.Commands;
using Restaurant.Domain.Context;
using Restaurant.Infrastructure.Exceptions;
using Restaurant.Queries;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurant.Handlers
{
    public class UpdateRestaurantHandler : IRequestHandler<UpdateRestaurantCommond, UpdateRestaurantResponse>
    {
        readonly ILogger<UpdateRestaurantHandler> logger;
        private readonly IRestaurantContext context;
        readonly IMediator mediator;
        readonly IConfiguration configuration;


        public UpdateRestaurantHandler(IMediator mediator, IConfiguration configuration, ILogger<UpdateRestaurantHandler> logger, IRestaurantContext context)
        {
            this.mediator = mediator;
            this.configuration = configuration;
            this.logger = logger;
            this.context = context;
        }

        public async Task<UpdateRestaurantResponse> Handle(UpdateRestaurantCommond request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                    throw new RestaurantBadRequestException();
                var existingRestaurant = await mediator.Send(new GetByIdRestaurantQuery() { Id = request.Id });
                existingRestaurant.Name = request.Name;
                existingRestaurant.Address = request.Address;
                existingRestaurant.City = request.City;
                existingRestaurant.Country = request.Country;
                existingRestaurant.PinCode = request.PinCode;

                var updateResult = await context
                            .Restaurants
                            .ReplaceOneAsync(filter: g => g.Id == request.Id, replacement: existingRestaurant);

                return new UpdateRestaurantResponse();
            }
            catch (Exception e)
            {
                this.logger.LogError(e.ToString());
                throw;
            }
        }
    }

}
