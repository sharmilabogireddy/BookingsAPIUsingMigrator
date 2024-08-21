using System.ComponentModel.DataAnnotations;

namespace BookingsAPIUsingMigrator.api.Models.RequestModel
{
    public class BookingRequestModel
    {
        //[Required(ErrorMessage = "Name is required.")]
        public string name { get; set; }

        public string? bookingTime { get; set; }
    }
}
