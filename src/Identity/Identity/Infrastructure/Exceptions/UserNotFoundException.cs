using System;

namespace Identity.Infrastructure.Exceptions
{
    public class UserNotFoundException : Exception, ICustomException
    {
        public int StatusCode => 404;

        public UserNotFoundException() : base("This User does not exist.")
        {
        }

        public UserNotFoundException(int id) : base($"No User found with id '{id}'")
        {
        }

        public UserNotFoundException(Guid id) : base($"No User found with id '{id}'")
        {
        }

        public UserNotFoundException(string id) : base($"No User found with id '{id}'")
        {
        }
    }
}
