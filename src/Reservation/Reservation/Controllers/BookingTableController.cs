using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Reservation.Commands.BookingTable;
using Reservation.CommonUtility.Constants;
using Reservation.Domain;
using Reservation.Dto;
using Reservation.Infrastructure.Exceptions;
using Reservation.Queries.BookingTable;
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
    public class BookingTableController : BaseController
    {

        private readonly ILogger<BookingTableController> _logger;
        private readonly IMediator mediator;

        public BookingTableController(ILogger<BookingTableController> logger, IMediator mediator)
        {
            _logger = logger;
            this.mediator = mediator;
        }        

        [Description("Returns a list of Booking tables")]
        [HttpGet]
        public async Task<IEnumerable<GetAllBookingListDto>> ListBookingTablesBy([FromQuery] GetAllBookingTableQuery request)
        {
            var response = await mediator.Send(request);
            return response;
        }

        [Description("Returns a list of Booking tables")]
        [ProducesResponseType(typeof(ExceptionMessage), (int)HttpStatusCode.NotFound)]
        [HttpGet("{id}")]
        public async Task<Domain.BookingTable> GetBookingTableById(Guid id)
        {
            var response = await mediator.Send(new GetByIdBookingTableQuery() { Id = id });
            return response;
        }

        [Description("Create a Booking tables")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ExceptionMessage), (int)HttpStatusCode.BadRequest)]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> BookTable([FromBody] CreateBookingTableCommond commond)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.ToList());

            await mediator.Send(commond);
            return NoContent();
        }

        [Description("Updates an existing BookingTables")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ExceptionMessage), (int)HttpStatusCode.NotFound)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTable(Guid id, [FromBody] UpdateBookingTableCommond request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.ToList());

            request.Id = id; 
            await mediator.Send(request);
            return NoContent();
        }
    }
}
