using MediatR;
using Reservation.Dto;
using System;
using System.Collections.Generic;
using Domains = Reservation.Domain;

namespace Reservation.Queries.BookingTable
{
    public class GetAllBookingTableQuery : IRequest<IEnumerable<GetAllBookingListDto>>
    {
        public string Email { get; set; }
    }
}
