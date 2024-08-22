using AutoMapper;
using BookingsAPIUsingMigrator.core.Dtos;
using BookingsAPIUsingMigrator.core.Requests;
using BookingsAPIUsingMigrator.dataaccess;
using BookingsAPIUsingMigrator.dataaccess.Repositories.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingsAPIUsingMigrator.core.Handlers
{
    public class RemoveBookingHandler : IRequestHandler<RemoveBookingRequest, BookingDto>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IBookingRepositoryManager _bookingRepositoryManager;
        private readonly IMapper _mapper;

        public RemoveBookingHandler(IBookingRepositoryManager bookingRepositoryManager, IMapper mapper)
        {
            _bookingRepositoryManager = bookingRepositoryManager;
            _mapper = mapper;
        }
        public async Task<BookingDto> Handle(RemoveBookingRequest request, CancellationToken cancellationToken)
        {
            var booking = await _bookingRepositoryManager.BookingRepository.Find(request.id);

            if (booking != null)
            {
                await _bookingRepositoryManager.BookingRepository.Delete(booking);
                await _bookingRepositoryManager.Save();

            }

            return _mapper.Map<BookingDto>(booking);
        }
    }
}
