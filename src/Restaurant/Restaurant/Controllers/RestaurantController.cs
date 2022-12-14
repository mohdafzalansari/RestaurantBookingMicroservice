using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Restaurant.Commands;
using Restaurant.CommonUtility.Constants;
using Restaurant.Domain;
using Restaurant.Infrastructure.Exceptions;
using Restaurant.Queries;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Restaurant.Controllers
{
    [ApiController]
    [Authorize(Policy = AppConstants.AdminPolicy)]
    [Route("api/[controller]")]
    public class RestaurantController : ControllerBase
    {

        private readonly ILogger<RestaurantController> _logger;
        private readonly IMediator mediator;

        public RestaurantController(ILogger<RestaurantController> logger, IMediator mediator)
        {
            _logger = logger;
            this.mediator = mediator;
        }        

        [Description("Returns a list of restaurants")]
        [HttpGet]
        public async Task<IEnumerable<Domain.Restaurant>> ListRestaurants()
        {
            var response = await mediator.Send(new GetAllRestaurantQuery());
            return response;
        }

        [Description("Returns a list of restaurants")]
        [ProducesResponseType(typeof(ExceptionMessage), (int)HttpStatusCode.NotFound)]
        [HttpGet("{id}")]
        public async Task<Domain.Restaurant> GetRestaurantById(Guid id)
        {
            var response = await mediator.Send(new GetByIdRestaurantQuery() { Id = id });
            return response;
        }

        [Description("Create a restaurants")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ExceptionMessage), (int)HttpStatusCode.BadRequest)]
        [HttpPost]
        public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantCommond commond)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.ToList());

            await mediator.Send(commond);
            return NoContent();
        }

        [Description("Updates an existing restaurants")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ExceptionMessage), (int)HttpStatusCode.NotFound)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRestaurant(Guid id, [FromBody] UpdateRestaurantCommond request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.ToList());

            request.Id = id;
            await mediator.Send(request);
            return NoContent();
        }
    }
}
