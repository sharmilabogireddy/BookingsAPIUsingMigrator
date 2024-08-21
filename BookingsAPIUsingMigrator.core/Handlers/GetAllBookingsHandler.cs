using AutoMapper;
using BookingsAPIUsingMigrator.core.Dtos;
using BookingsAPIUsingMigrator.core.Requests;
using BookingsAPIUsingMigrator.dataaccess;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingsAPIUsingMigrator.core.Handlers
{
    public class GetAllBookingsHandler : IRequestHandler<GetAllBookingsRequest, IList<BookingDto>>
    {
        private readonly IBookingRepositoryManager _bookingRepositoryManager;
        private readonly IMapper _mapper;

        public GetAllBookingsHandler(IBookingRepositoryManager bookingRepositoryManager,
                                      IMapper mapper)
        {
            _bookingRepositoryManager = bookingRepositoryManager;
            _mapper = mapper;
        }

        public async Task<IList<BookingDto>> Handle(GetAllBookingsRequest request, CancellationToken cancellationToken)
        {
            var bookingList = await _bookingRepositoryManager.BookingRepository.FindAll();

            return bookingList.Select(_mapper.Map<BookingDto>).ToList();
        }
    }
}
