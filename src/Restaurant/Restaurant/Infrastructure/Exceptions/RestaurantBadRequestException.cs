using System;

namespace Restaurant.Infrastructure.Exceptions
{
    public class RestaurantBadRequestException : Exception, ICustomException
    {
        public int StatusCode => 404;

        public RestaurantBadRequestException() : base("This Restaurant request is incorrect.")
        {
        }
    }
}
