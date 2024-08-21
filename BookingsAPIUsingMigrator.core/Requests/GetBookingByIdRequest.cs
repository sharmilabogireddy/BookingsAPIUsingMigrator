using BookingsAPIUsingMigrator.core.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingsAPIUsingMigrator.core.Requests
{
    public class GetBookingByIdRequest : IRequest<BookingDto>
    {
        public GetBookingByIdRequest(int Id)
        {
            this.Id = Id;
        }

        public int Id { get; set; }
    }
}
