using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reservation.Commands.RestaurantTable
{
    public class DeleteRestaurantTableCommond : IRequest<DeleteRestaurantTableResponse>
    {
        public Guid Id { get; set; }
    }
    public class DeleteRestaurantTableResponse { }
}
