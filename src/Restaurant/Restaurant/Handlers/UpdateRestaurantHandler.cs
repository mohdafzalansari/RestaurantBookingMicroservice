using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Restaurant.Commands;
using Restaurant.Domain.Context;
using Restaurant.Infrastructure.Exceptions;
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

                var updateResult = await context
                            .Restaurants
                            .ReplaceOneAsync(filter: g => g.Id == request.Id, replacement: new Domain.Restaurant
                            {
                                Name = request.Name,
                                Address = request.Address,
                                City = request.City,
                                Country = request.Country,
                                PinCode = request.PinCode,
                            });

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
