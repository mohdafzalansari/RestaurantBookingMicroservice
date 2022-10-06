using System;

namespace Reservation.Infrastructure.Exceptions
{
    public class RestaurantNotFoundException : Exception, ICustomException
    {
        public int StatusCode => 404;

        public RestaurantNotFoundException() : base("This Restaurant does not exist.")
        {
        }

        public RestaurantNotFoundException(int id) : base($"No Restaurant found with id '{id}'")
        {
        }

        public RestaurantNotFoundException(Guid id) : base($"No Restaurant found with id '{id}'")
        {
        }

        public RestaurantNotFoundException(string id) : base($"No Restaurant found with id '{id}'")
        {
        }
    }
}
