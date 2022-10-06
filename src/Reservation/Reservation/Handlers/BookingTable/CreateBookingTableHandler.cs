using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Reservation.Commands.BookingTable;
using Reservation.Domain;
using Reservation.Domain.Context;
using Reservation.Infrastructure.Exceptions;
using Reservation.Queries.RestaurantTable;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Reservation.Handlers.BookingTable
{
    public class CreateBookingTableHandler : IRequestHandler<CreateBookingTableCommond, CreateBookingTableResponse>
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

        public async Task<CreateBookingTableResponse> Handle(CreateBookingTableCommond request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                    throw new RestaurantBadRequestException();

                await this.mediator.Send(new GetByIdRestaurantTableQuery() { Id = request.RestaurantTableId });

                await context.BookingTables.InsertOneAsync(new Reservation.Domain.BookingTable
                {
                    Name = request.Name,
                    Email = request.Email,
                    BookingDateTime = request.BookingDateTime,
                    NumberOfVisitors = request.NumberOfVisitors,
                    RestaurantTableId = request.RestaurantTableId,
                    TenantId = request.TenantId
                });

                return new CreateBookingTableResponse();
            }
            catch (Exception e)
            {
                this.logger.LogError(e.ToString());
                throw;
            }
        }
    }

}
