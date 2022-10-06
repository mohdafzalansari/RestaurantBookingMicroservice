using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Reservation.Domain.Context;
using Reservation.Infrastructure.Common.Helpers;
using Reservation.Infrastructure.Exceptions;
using Reservation.Queries.BookingTable;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Reservation.Handlers.BookingTable
{
    public class GetByIdBookingTableHandler : IRequestHandler<GetByIdBookingTableQuery, Reservation.Domain.BookingTable>
    {
        readonly ILogger<GetByIdBookingTableHandler> logger;
        private readonly IReservationContext context;
        private readonly ICustomClaimHelpers customClaim;
        readonly IMediator mediator;
        readonly IConfiguration configuration;


        public GetByIdBookingTableHandler(IMediator mediator, IConfiguration configuration, ILogger<GetByIdBookingTableHandler> logger, IReservationContext context, ICustomClaimHelpers customClaim)
        {
            this.mediator = mediator;
            this.configuration = configuration;
            this.logger = logger;
            this.context = context;
            this.customClaim = customClaim;
        }

        public async Task<Reservation.Domain.BookingTable> Handle(GetByIdBookingTableQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await context.BookingTables.Find(p => p.Id == request.Id && p.TenantId == customClaim.TenantId)
                            .SingleOrDefaultAsync();
                if (result == null)
                {
                    throw new RestaurantNotFoundException(request.Id);
                }
                return result;
            }
            catch (Exception e)
            {
                this.logger.LogError(e.ToString());
                throw;
            }
        }
    }

}
