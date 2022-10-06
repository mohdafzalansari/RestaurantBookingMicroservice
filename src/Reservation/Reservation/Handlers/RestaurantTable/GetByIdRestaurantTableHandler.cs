using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Reservation.Domain;
using Reservation.Domain.Context;
using Reservation.Infrastructure.Exceptions;
using Reservation.Queries.RestaurantTable;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Reservation.Handlers
{
    public class GetByIdRestaurantTableHandler : IRequestHandler<GetByIdRestaurantTableQuery, Reservation.Domain.RestaurantTable>
    {
        readonly ILogger<GetByIdRestaurantTableHandler> logger;
        private readonly IReservationContext context;
        readonly IMediator mediator;
        readonly IConfiguration configuration;


        public GetByIdRestaurantTableHandler(IMediator mediator, IConfiguration configuration, ILogger<GetByIdRestaurantTableHandler> logger, IReservationContext context)
        {
            this.mediator = mediator;
            this.configuration = configuration;
            this.logger = logger;
            this.context = context;
        }

        public async Task<Reservation.Domain.RestaurantTable> Handle(GetByIdRestaurantTableQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await context.RestaurantTables.Find(p => p.Id == request.Id)
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
