using BookingsAPIUsingMigrator.core.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingsAPIUsingMigrator.core.Requests
{
    public class GetAllBookingsRequest : IRequest<IList<BookingDto>>
    {
    }
}
