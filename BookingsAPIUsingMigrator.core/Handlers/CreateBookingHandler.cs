using BookingsAPIUsingMigrator.core.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingsAPIUsingMigrator.core.Dtos;
using AutoMapper;
using BookingsAPIUsingMigrator.shared.Common;
using System.ComponentModel.DataAnnotations;
using BookingsAPIUsingMigrator.dataaccess.Entities;
using BookingsAPIUsingMigrator.dataaccess;


namespace BookingsAPIUsingMigrator.core.Handlers
{
    public class CreateBookingHandler : IRequestHandler<CreateBookingRequest, BookingDto>
    {
        private int _maxBookingCapacity = 4;
        private static Dictionary<int, int> currentBookings = new Dictionary<int, int>();
        private static TimeSpan bookingStartTime = new TimeSpan(9, 0, 0);
        private static TimeSpan bookingEndTime = new TimeSpan(16, 0, 0);
        private readonly IBookingRepositoryManager _bookingRepositoryManager;
        private readonly IMapper _mapper;

        public CreateBookingHandler(IBookingRepositoryManager bookingRepositoryManager, IMapper mapper) 
        {
            _bookingRepositoryManager = bookingRepositoryManager;
            _mapper = mapper;
        }
        public async Task<BookingDto> Handle(CreateBookingRequest request, CancellationToken cancellationToken)
        {
            var bookingToAdd = await ValidateRequestData(request);

            var result = await _bookingRepositoryManager.BookingRepository.Create(bookingToAdd);

            await _bookingRepositoryManager.Save();

            //BookingDto booking = new BookingDto() { name = request.name, bookingTime = request.bookingTime };

            return _mapper.Map<BookingDto>(result);
        }

        private async Task<Booking> ValidateRequestData(CreateBookingRequest request)
        {
            if (string.IsNullOrEmpty(request.name))
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(ConstantStrings.NAME_IVALID);
            }
            if (string.IsNullOrEmpty(request.bookingTime))
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(ConstantStrings.BOOKING_TIME_INVALID);
            }

            if (!TimeSpan.TryParse(request.bookingTime, out TimeSpan bookingTimeObject))
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(ConstantStrings.BOOKING_TIME_INVALID);

            }

            bool isValidBookingTime = bookingTimeObject >= bookingStartTime && bookingTimeObject <= bookingEndTime;

            if (!isValidBookingTime)
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(ConstantStrings.BOOKING_TIME_NOT_IN_BUSSINESS_HOURS);
            }

            int defaultCount = 0;
            if (!currentBookings.TryGetValue(bookingTimeObject.Hours, out int count))
            {
                count = defaultCount;
            }

            int bookingTimeCount = await _bookingRepositoryManager.BookingRepository.GetCountByBookingTime(request.bookingTime);
            //Check for max simultaneous bookings.
            if (count < _maxBookingCapacity && bookingTimeCount != _maxBookingCapacity)
            {
                count++;
                currentBookings[bookingTimeObject.Hours] = count;
            }
            else
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(ConstantStrings.BOOKING_TIME_INVALID);
            }

            return new Booking
            {
                bookingTime = request.bookingTime,
                name = request.name
            };
        }


    }
}
