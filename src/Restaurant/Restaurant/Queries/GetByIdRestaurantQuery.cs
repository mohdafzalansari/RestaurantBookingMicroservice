using MediatR;
using Restaurant.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Queries
{
    public class GetByIdRestaurantQuery : IRequest<Domain.Restaurant>
    {
        public Guid Id { get; set; }
    }
}
