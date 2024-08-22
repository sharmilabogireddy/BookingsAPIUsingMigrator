using AutoMapper;
using BookingsAPIUsingMigrator.core.Dtos;
using BookingsAPIUsingMigrator.core.Requests;
using BookingsAPIUsingMigrator.dataaccess;
using BookingsAPIUsingMigrator.dataaccess.Entities;
using BookingsAPIUsingMigrator.shared.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingsAPIUsingMigrator.core.Handlers
{
    public class UpdateBookingHandler : IRequestHandler<UpdateBookingRequest, BookingDto>
    {
        private int _maxBookingCapacity = 4;
        private static Dictionary<int, int> currentBookings = new Dictionary<int, int>();
        private static TimeSpan bookingStartTime = new TimeSpan(9, 0, 0);
        private static TimeSpan bookingEndTime = new TimeSpan(16, 0, 0);
        private readonly IBookingRepositoryManager _bookingRepositoryManager;
        private readonly IMapper _mapper;

        public UpdateBookingHandler(IBookingRepositoryManager bookingRepositoryManager, IMapper mapper)
        {
            _bookingRepositoryManager = bookingRepositoryManager;
            _mapper = mapper;
        }
        public async Task<BookingDto> Handle(UpdateBookingRequest request, CancellationToken cancellationToken)
        {
            var recordToUdate = await ValidateRequestData(request);

            var result = await _bookingRepositoryManager.BookingRepository.Update(recordToUdate);

            await _bookingRepositoryManager.Save();

            return _mapper.Map<BookingDto>(recordToUdate);
        }
        private async Task<Booking> ValidateRequestData(UpdateBookingRequest request)
        {
            var recordFound = await _bookingRepositoryManager.BookingRepository.Find(request.Id);

            int bookingTimeCount = await _bookingRepositoryManager.BookingRepository.GetCountByBookingTime(request.bookingTime);

            if (recordFound == null)
            {
                throw new ValidationException(ConstantStrings.NO_REQUESTED_RECORD);
            }

            if (bookingTimeCount == _maxBookingCapacity)
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(ConstantStrings.BOOKING_TIME_INVALID);

            }
            
            recordFound.name = request.name;
            recordFound.bookingTime = request.bookingTime;

            return recordFound;
        }
    }
}
