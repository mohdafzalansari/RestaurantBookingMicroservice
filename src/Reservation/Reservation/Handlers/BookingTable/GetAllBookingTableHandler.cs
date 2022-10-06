using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Reservation.Domain.Context;
using Reservation.Dto;
using Reservation.Infrastructure.Common.Helpers;
using Reservation.Queries.BookingTable;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Reservation.Handlers.BookingTable
{
    public class GetAllBookingTableHandler : IRequestHandler<GetAllBookingTableQuery, IEnumerable<GetAllBookingListDto>>
    {
        readonly ILogger<GetAllBookingTableHandler> logger;
        private readonly IReservationContext context;
        private readonly ICustomClaimHelpers customClaim;
        readonly IMediator mediator;
        readonly IConfiguration configuration;


        public GetAllBookingTableHandler(IMediator mediator, IConfiguration configuration, ILogger<GetAllBookingTableHandler> logger, IReservationContext context, ICustomClaimHelpers customClaim)
        {
            this.mediator = mediator;
            this.configuration = configuration;
            this.logger = logger;
            this.context = context;
            this.customClaim = customClaim;
        }

        public async Task<IEnumerable<GetAllBookingListDto>> Handle(GetAllBookingTableQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var getAllBookingListDto = new List<GetAllBookingListDto>();
                var list = new List<Domain.BookingTable>();
                if (!string.IsNullOrEmpty(request.Email))
                {
                    list = await context.BookingTables.Find(p => p.Email == request.Email && p.TenantId == customClaim.TenantId)
                         .ToListAsync();
                }
                else
                {
                    list = await context.BookingTables.Find(p => p.TenantId == customClaim.TenantId).ToListAsync();
                }

                foreach (var item in list)
                {
                    getAllBookingListDto.Add(new GetAllBookingListDto
                    {
                        BookingTables = new Domain.BookingTable { Id = item.Id, BookingDateTime = item.BookingDateTime, Email = item.Email, Name = item.Name, NumberOfVisitors = item.NumberOfVisitors, RestaurantTableId = item.RestaurantTableId, TenantId = item.TenantId },
                        RestaurantTables = await context.RestaurantTables.Find(p => p.Id == item.RestaurantTableId && p.TenantId== customClaim.TenantId).SingleOrDefaultAsync()
                    });
                }

                return getAllBookingListDto;
            }
            catch (Exception e)
            {
                this.logger.LogError(e.ToString());
                throw;
            }
        }


    }
}
