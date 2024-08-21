using BookingsAPIUsingMigrator.core.Dtos;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingsAPIUsingMigrator.core.Requests
{
    public class PatchBookingRequest : IRequest<BookingDto>
    {
        public int Id { get; set; }

        public required JsonPatchDocument<BookingDto> PatchData { get; set; }
    }
}
