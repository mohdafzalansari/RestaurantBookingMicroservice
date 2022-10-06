using AutoMapper.Configuration;
using Identity.Domain.Context;
using Identity.Infrastructure.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.Handlers
{
    public class CreateUserRequest : IRequest<CreateUserResponse>
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [Required]
        public string MobileNumber { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        [Required]
        public string Role { get; set; }
        [Required]
        public int TenantId { get; set; }
    }
    public class CreateUserResponse
    {
    }
    public class CreateUserHandler : IRequestHandler<CreateUserRequest, CreateUserResponse>
    {
        readonly ILogger<CreateUserHandler> logger;
        private readonly IIdentityContext context;


        public CreateUserHandler(ILogger<CreateUserHandler> logger, IIdentityContext context)
        {
            this.logger = logger;
            this.context = context;
        }

        public async Task<CreateUserResponse> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                    throw new UserBadRequestException();

                await context.Users.InsertOneAsync(new Domain.User
                {
                    UserName = request.UserName,
                    Email = request.Email,
                    PasswordHash = request.PasswordHash,
                    Gender = request.Gender,
                    Age = request.Age,
                    MobileNumber = request.MobileNumber,
                    Role = request.Role,
                    CreatedOn = DateTime.Now,
                    IsActive = true,
                    IsDeleted = true,
                    TenantId = request.TenantId
                });

                return new CreateUserResponse();
            }
            catch (Exception e)
            {
                this.logger.LogError(e.ToString());
                throw;
            }
        }
    }

}
