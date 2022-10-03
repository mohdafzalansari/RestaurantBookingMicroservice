using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Commands
{
    public class DeleteRestaurantCommond : IRequest<DeleteRestaurantResponse>
    {
        public Guid Id { get; set; }
    }
    public class DeleteRestaurantResponse { }
}
