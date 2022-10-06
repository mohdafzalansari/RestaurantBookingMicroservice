using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Reservation.Commands.RestaurantTable;
using Reservation.CommonUtility.Constants;
using Reservation.Domain;
using Reservation.Infrastructure.Exceptions;
using Reservation.Queries.RestaurantTable;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Reservation.Controllers
{
    [ApiController]
    [Authorize(Policy = AppConstants.AdminPolicy)]
    [Route("api/[controller]")]
    public class RestaurantTableController : BaseController
    {

        private readonly ILogger<RestaurantTableController> _logger;
        private readonly IMediator mediator;

        public RestaurantTableController(ILogger<RestaurantTableController> logger, IMediator mediator)
        {
            _logger = logger;
            this.mediator = mediator;
        }        

        [Description("Returns a list of RestaurantTables")]
        [HttpGet]
        public async Task<IEnumerable<RestaurantTable>> ListRestaurantTables()
        {
            var response = await mediator.Send(new GetAllBookingTableQuery());
            return response;
        }

        [Description("Returns a list of RestaurantTables")]
        [ProducesResponseType(typeof(ExceptionMessage), (int)HttpStatusCode.NotFound)]
        [HttpGet("{id}")]
        public async Task<Domain.RestaurantTable> GetRestaurantTableById(Guid id)
        {
            var response = await mediator.Send(new GetByIdRestaurantTableQuery() { Id = id });
            return response;
        }

        [Description("Create a RestaurantTables")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ExceptionMessage), (int)HttpStatusCode.BadRequest)]
        [HttpPost]
        public async Task<IActionResult> CreateRestaurantTables([FromBody] CreateRestaurantTableCommond commond)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.ToList());

            await mediator.Send(commond);
            return NoContent();
        }

        [Description("Updates an existing RestaurantTables")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ExceptionMessage), (int)HttpStatusCode.NotFound)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClient(Guid id, [FromBody] UpdateRestaurantgTableCommond request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.ToList());

            request.Id = id;
            await mediator.Send(request);
            return NoContent();
        }
    }
}
