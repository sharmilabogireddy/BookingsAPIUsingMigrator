using AutoMapper;
using BookingsAPIUsingMigrator.core.Dtos;
using BookingsAPIUsingMigrator.core.Requests;
using BookingsAPIUsingMigrator.dataaccess;
using BookingsAPIUsingMigrator.shared.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingsAPIUsingMigrator.core.Handlers
{
    public class PatchBookingHandler : IRequestHandler<PatchBookingRequest, BookingDto>
    {
        private readonly IBookingRepositoryManager _bookingRepositoryManager;
        private readonly IMapper _mapper;


        public PatchBookingHandler(IBookingRepositoryManager bookingRepositoryManager,
                                       IMapper mapper)
        {
            _bookingRepositoryManager = bookingRepositoryManager;
            _mapper = mapper;

        }

        public async Task<BookingDto> Handle(PatchBookingRequest request, CancellationToken cancellationToken)
        {
            if (request.PatchData == null)
            {
                throw new ValidationException(ConstantStrings.INVALID_REQUEST_DATA);
            }

            var entity = await _bookingRepositoryManager.BookingRepository.Find(request.Id) ?? throw new ValidationException(ConstantStrings.NO_REQUESTED_RECORD);

            var entityToPatch = _mapper.Map<BookingDto>(entity);
            request.PatchData.ApplyTo(entityToPatch);

            _mapper.Map(entityToPatch, entity);

            await _bookingRepositoryManager.Save();

            return _mapper.Map<BookingDto>(entity);
        }
    }
}
