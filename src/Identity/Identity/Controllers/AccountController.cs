using System;
using System.ComponentModel;
using System.Net;
using System.Threading.Tasks;
using Identity.Domain;
using Identity.Handlers;
using Identity.Infrastructure.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IMediator mediator;
        public AccountController(ILogger<AccountController> logger, IMediator mediator)
        {
            _logger = logger;
            this.mediator = mediator;
        }

        [Description("Register user")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ExceptionMessage), (int)HttpStatusCode.BadRequest)]
        [HttpPost("RegisterUser")]
        public async Task<ActionResult> RegisterUser(string userName, string password, int tenantId, string role = Role.User)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password) || tenantId <= 0)
                return BadRequest("UserName , Password and tenantId can not be emplty");

            var user = await mediator.Send(new GetUserByUserNameQuery { UserName = userName });
            if (user != null)
            {
                return BadRequest($"Email: '{userName}' is already in use.");
            }
            if (string.IsNullOrWhiteSpace(role))
            {
                role = Role.User;
            }

            var request = new CreateUserRequest()
            {
                Email = userName,
                UserName = userName,
                Role = role,
                PasswordHash = password,
                TenantId= tenantId
            };
            await mediator.Send(request);

            return NoContent();
        }

        [Description("Register customer")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ExceptionMessage), (int)HttpStatusCode.BadRequest)]
        [HttpPost("RegisterCustomer")]
        [AllowAnonymous]
        public async Task<ActionResult> RegisterCustomer(string userName, string password, int tenantId)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password) || tenantId <= 0)
                return BadRequest("UserName , Password and tenantId can not be emplty");

            var user = await mediator.Send(new GetUserByUserNameQuery { UserName = userName });
            if (user != null)
            {
                return BadRequest($"Email: '{userName}' is already in use.");
            }

            var request = new CreateUserRequest()
            {
                Email = userName,
                UserName = userName,
                Role = Role.Customer,
                PasswordHash = password,
                TenantId= tenantId
            };
            await mediator.Send(request);

            return NoContent();
        }

    }
}