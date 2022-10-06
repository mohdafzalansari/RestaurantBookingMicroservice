using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Reservation.Commands.BookingTable;
using Reservation.Domain.Context;
using Reservation.Infrastructure.Exceptions;
using Reservation.Queries.BookingTable;
using Reservation.Queries.RestaurantTable;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Reservation.Handlers.BookingTable
{
    public class UpdateBookingTableHandler : IRequestHandler<UpdateBookingTableCommond, UpdateBookingTableResponse>
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

        public async Task<UpdateBookingTableResponse> Handle(UpdateBookingTableCommond request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                    throw new RestaurantBadRequestException();

                var existingTabl= await mediator.Send(new GetByIdBookingTableQuery() { Id = request.Id });
                existingTabl.Name = request.Name;
                existingTabl.BookingDateTime = request.BookingDateTime;
                existingTabl.RestaurantTableId = request.RestaurantTableId;
                existingTabl.Email = request.Email;
                existingTabl.NumberOfVisitors = request.NumberOfVisitors;

                var updateResult = await context
                            .BookingTables
                            .ReplaceOneAsync(filter: g => g.Id == request.Id, replacement: existingTabl);

                return new UpdateBookingTableResponse();
            }
            catch (Exception e)
            {
                this.logger.LogError(e.ToString());
                throw;
            }
        }
    }

}
