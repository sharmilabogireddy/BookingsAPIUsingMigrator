using AutoMapper;
using BookingsAPIUsingMigrator.api.Models;
using BookingsAPIUsingMigrator.api.Models.RequestModel;
using BookingsAPIUsingMigrator.core.Dtos;
using BookingsAPIUsingMigrator.core.Requests;
using BookingsAPIUsingMigrator.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingsAPIUsingMigrator.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingsAPIUsingMigratorController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public BookingsAPIUsingMigratorController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Get All of the Bookings
        /// </summary>
        /// <returns>The list of the Bookings</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IList<BookingsResponse>>> GetAllBookings()
        {
            var response = await _mediator.Send(new GetAllBookingsRequest());

              return Ok(response.Select(_mapper.Map<BookingsResponse>).ToList());

        }

        /// <summary>
        /// Get a specific Booking by Id
        /// </summary>
        /// <returns>The Booking</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BookingsResponse>> GetPaymentById(int id)
        {
            var payment = await _mediator.Send(new GetBookingByIdRequest(id));

            return payment == null ? NotFound() : Ok(_mapper.Map<BookingsResponse>(payment));
        }

        /// <summary>
        /// Create a new booking
        /// </summary>
        /// <param name="requestModel">The data to create booking.</param>
        /// <returns>The bookingId</returns>
        [HttpPost()]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BookingResponse>> CreateBooking([FromBody] BookingRequestModel requestModel)
        {
            try
            {
                var result = await _mediator.Send(new CreateBookingRequest()
                {
                    name = requestModel.name,
                    bookingTime = requestModel.bookingTime
                });
                BookingResponse response = new BookingResponse()
                {
                    bookingId = Guid.NewGuid()
                };
                return Ok(response);
            }
            catch(Exception ex) {
                return BadRequest(ex.Message);
            }
            
        }

        /// <summary>
        /// Patch an existing Booking
        /// </summary>
        /// <param name="id"></param>
        /// <param name="requestModel">The data describing the Booking details to Patch</param>
        /// <returns>The updated Booking object</returns>
        [HttpPatch("bookings/{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BookingsResponse>> PatchBooking(int id, [FromBody] PatchBookingRequestModel requestModel)
        {
            var formDto = await _mediator.Send(new PatchBookingRequest()
            {
                Id = id,
                PatchData = requestModel.PatchData
            });

            return Ok(_mapper.Map<BookingsResponse>(formDto));
        }

        /// <summary>
        /// Update an existing Booking
        /// </summary>
        /// <param name="id">The ID of the Booking</param>
        /// <param name="form">The data describing the Booking details to update</param>
        /// <returns>The updated Booking object</returns>
        [HttpPut]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BookingsResponse>> UpdateBooking([FromBody] UpdateBookingRequest requestModel)
        {
            try
            {
                var bookingDto = await _mediator.Send(new UpdateBookingRequest()
                {
                    Id = requestModel.Id,
                    name = requestModel.name,
                    bookingTime = requestModel.bookingTime
                });

                return Ok(_mapper.Map<BookingsResponse>(bookingDto));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
