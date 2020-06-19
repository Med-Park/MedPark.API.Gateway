using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedPark.API.Gateway.Models.BookingService
{
    public class CustomerAppointmentsDto
    {
        public IEnumerable<AppointmentDto> BookingDetails { get; set; }

        public CustomerAppointmentsDto()
        {

        }
    }
}
