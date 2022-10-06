using System;

namespace Identity.Infrastructure.Exceptions
{
    public class UserBadRequestException : Exception, ICustomException
    {
        public int StatusCode => 404;

        public UserBadRequestException() : base("This User request is incorrect.")
        {
        }
    }
}
