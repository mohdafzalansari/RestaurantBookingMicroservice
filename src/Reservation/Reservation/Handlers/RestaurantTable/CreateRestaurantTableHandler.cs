using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Reservation.Commands.RestaurantTable;
using Reservation.Domain;
using Reservation.Domain.Context;
using Reservation.Infrastructure.Exceptions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Reservation.Handlers.RestaurantTable
{
    public class CreateBookingTableHandler : IRequestHandler<CreateRestaurantTableCommond, CreateRestaurantTableResponse>
    {
        readonly ILogger<CreateBookingTableHandler> logger;
        private readonly IReservationContext context;
        readonly IMediator mediator;
        readonly IConfiguration configuration;


        public CreateBookingTableHandler(IMediator mediator, IConfiguration configuration, ILogger<CreateBookingTableHandler> logger, IReservationContext context)
        {
            this.mediator = mediator;
            this.configuration = configuration;
            this.logger = logger;
            this.context = context;
        }

        public async Task<CreateRestaurantTableResponse> Handle(CreateRestaurantTableCommond request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                    throw new RestaurantBadRequestException();

                await context.RestaurantTables.InsertOneAsync(new Reservation.Domain.RestaurantTable
                {
                    Name = request.Name,
                    Location = request.Location,
                    NumberOfSits = request.NumberOfSits,
                    TenantId = request.TenantId
                });

                return new CreateRestaurantTableResponse();
            }
            catch (Exception e)
            {
                this.logger.LogError(e.ToString());
                throw;
            }
        }
    }

}
