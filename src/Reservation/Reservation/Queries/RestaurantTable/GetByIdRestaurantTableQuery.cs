using MediatR;
using Reservation.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domains = Reservation.Domain;

namespace Reservation.Queries.RestaurantTable
{
    public class GetByIdRestaurantTableQuery : IRequest<Domains.RestaurantTable>
    {
        public Guid Id { get; set; }
    }
}
