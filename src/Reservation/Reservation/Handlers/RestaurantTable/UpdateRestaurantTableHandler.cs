using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Reservation.Commands.RestaurantTable;
using Reservation.Domain.Context;
using Reservation.Infrastructure.Exceptions;
using Reservation.Queries.RestaurantTable;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Reservation.Handlers.RestaurantTable
{
    public class UpdateBookingTableHandler : IRequestHandler<UpdateRestaurantgTableCommond, UpdateRestaurantTableResponse>
    {
        readonly ILogger<UpdateBookingTableHandler> logger;
        private readonly IReservationContext context;
        readonly IMediator mediator;
        readonly IConfiguration configuration;


        public UpdateBookingTableHandler(IMediator mediator, IConfiguration configuration, ILogger<UpdateBookingTableHandler> logger, IReservationContext context)
        {
            this.mediator = mediator;
            this.configuration = configuration;
            this.logger = logger;
            this.context = context;
        }

        public async Task<UpdateRestaurantTableResponse> Handle(UpdateRestaurantgTableCommond request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                    throw new RestaurantBadRequestException();
                var existingRestaurant = await mediator.Send(new GetByIdRestaurantTableQuery() { Id = request.Id });

                existingRestaurant.Name = request.Name;
                existingRestaurant.Location = request.Location;
                existingRestaurant.NumberOfSits = request.NumberOfSits;

                var updateResult = await context
                            .RestaurantTables
                            .ReplaceOneAsync(filter: g => g.Id == request.Id, replacement: existingRestaurant);

                return new UpdateRestaurantTableResponse();
            }
            catch (Exception e)
            {
                this.logger.LogError(e.ToString());
                throw;
            }
        }
    }

}
