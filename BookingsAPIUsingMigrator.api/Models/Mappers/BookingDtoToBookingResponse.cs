using AutoMapper;
using BookingsAPIUsingMigrator.core.Dtos;
using BookingsAPIUsingMigrator.dataaccess.Entities;

namespace BookingsAPIUsingMigrator.api.Models.Mappers
{
    public class BookingDtoToBookingResponse : Profile
    {
        public BookingDtoToBookingResponse()
        {
            CreateMap<BookingDto, BookingsResponse>().ReverseMap();
        }

    }
}
