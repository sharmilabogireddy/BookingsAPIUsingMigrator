using AutoMapper;
using BookingsAPIUsingMigrator.core.Dtos;
using BookingsAPIUsingMigrator.dataaccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingsAPIUsingMigrator.core.Mappers
{
    public class BookingDtoToDomainMapper : Profile
    {
        public BookingDtoToDomainMapper()
        {
            CreateMap<Booking, BookingDto>().ReverseMap();

        }
    }
}
