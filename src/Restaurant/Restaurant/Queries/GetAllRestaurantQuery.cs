using MediatR;
using Restaurant.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Queries
{
    public class GetAllRestaurantQuery: IRequest<IEnumerable<Domain.Restaurant>>
    {
        public int Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PinCode { get; set; }
        public string Country { get; set; }
    }
}
