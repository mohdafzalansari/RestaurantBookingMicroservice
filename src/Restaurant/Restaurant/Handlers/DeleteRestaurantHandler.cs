using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Restaurant.Commands;
using Restaurant.Domain.Context;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurant.Handlers
{
    public class DeleteRestaurantHandler : IRequestHandler<DeleteRestaurantCommond, DeleteRestaurantResponse>
    {
        readonly ILogger<DeleteRestaurantHandler> logger;
        private readonly IRestaurantContext context;
        readonly IMediator mediator;
        readonly IConfiguration configuration;


        public DeleteRestaurantHandler(IMediator mediator, IConfiguration configuration, ILogger<DeleteRestaurantHandler> logger, IRestaurantContext context)
        {
            this.mediator = mediator;
            this.configuration = configuration;
            this.logger = logger;
            this.context = context;
        }

        public async Task<DeleteRestaurantResponse> Handle(DeleteRestaurantCommond request, CancellationToken cancellationToken)
        {
            try
            {
                var filter = Builders<Domain.Restaurant>.Filter.Eq(p => p.Id, request.Id);
                var deleteResult = await context
                                                .Restaurants
                                                .DeleteOneAsync(filter);
                return new DeleteRestaurantResponse();
            }
            catch (Exception e)
            {
                this.logger.LogError(e.ToString());
                throw;
            }
        }
    }

}
