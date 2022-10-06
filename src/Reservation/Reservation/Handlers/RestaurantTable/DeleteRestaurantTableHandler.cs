using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Reservation.Commands.RestaurantTable;
using Reservation.Domain;
using Reservation.Domain.Context;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Reservation.Handlers.RestaurantTable
{
    public class DeleteRestaurantTableHandler : IRequestHandler<DeleteRestaurantTableCommond, DeleteRestaurantTableResponse>
    {
        readonly ILogger<DeleteRestaurantTableHandler> logger;
        private readonly IReservationContext context;
        readonly IMediator mediator;
        readonly IConfiguration configuration;


        public DeleteRestaurantTableHandler(IMediator mediator, IConfiguration configuration, ILogger<DeleteRestaurantTableHandler> logger, IReservationContext context)
        {
            this.mediator = mediator;
            this.configuration = configuration;
            this.logger = logger;
            this.context = context;
        }

        public async Task<DeleteRestaurantTableResponse> Handle(DeleteRestaurantTableCommond request, CancellationToken cancellationToken)
        {
            try
            {
                var filter = Builders<Reservation.Domain.RestaurantTable>.Filter.Eq(p => p.Id, request.Id);
                var deleteResult = await context
                                                .RestaurantTables
                                                .DeleteOneAsync(filter);
                return new DeleteRestaurantTableResponse();
            }
            catch (Exception e)
            {
                this.logger.LogError(e.ToString());
                throw;
            }
        }
    }

}
