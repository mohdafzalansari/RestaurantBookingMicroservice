using MediatR;
using System.Collections.Generic;
using Domains = Reservation.Domain;

namespace Reservation.Queries.RestaurantTable
{
    public class GetAllBookingTableQuery : IRequest<IEnumerable<Domains.RestaurantTable>>
    {
    }
}
