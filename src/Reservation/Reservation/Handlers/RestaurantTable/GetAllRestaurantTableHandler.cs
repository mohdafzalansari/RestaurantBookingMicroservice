using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Reservation.Domain.Context;
using Reservation.Queries.RestaurantTable;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Reservation.Handlers.RestaurantTable
{
    public class GetAllBookingTableHandler : IRequestHandler<GetAllBookingTableQuery, IEnumerable<Reservation.Domain.RestaurantTable>>
    {
        readonly ILogger<GetAllBookingTableHandler> logger;
        private readonly IReservationContext context;
        readonly IMediator mediator;
        readonly IConfiguration configuration;


        public GetAllBookingTableHandler(IMediator mediator, IConfiguration configuration, ILogger<GetAllBookingTableHandler> logger, IReservationContext context)
        {
            this.mediator = mediator;
            this.configuration = configuration;
            this.logger = logger;
            this.context = context;
        }

        public async Task<IEnumerable<Reservation.Domain.RestaurantTable>> Handle(GetAllBookingTableQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await context.RestaurantTables.Find(p => true)
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
