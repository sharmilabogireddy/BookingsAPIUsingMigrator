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
    public class GetBookingByIdHandler : IRequestHandler<GetBookingByIdRequest, BookingDto>
    {
        private readonly IBookingRepositoryManager _bookingRepositoryManager;
        private readonly IMapper _mapper;

        public GetBookingByIdHandler(IBookingRepositoryManager bookingRepositoryManager,
                                                 IMapper mapper)
        {
            _bookingRepositoryManager = bookingRepositoryManager;
            _mapper = mapper;
        }


        public async Task<BookingDto> Handle(GetBookingByIdRequest request, CancellationToken cancellationToken)
        {

            var booking = await _bookingRepositoryManager.BookingRepository.FindById(request.Id);

            return _mapper.Map<BookingDto>(booking);
        }
    }
}
