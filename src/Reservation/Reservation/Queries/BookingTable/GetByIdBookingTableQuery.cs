using MediatR;
using Reservation.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domains = Reservation.Domain;

namespace Reservation.Queries.BookingTable
{
    public class GetByIdBookingTableQuery : IRequest<Domains.BookingTable>
    {
        public Guid Id { get; set; }
    }
}
