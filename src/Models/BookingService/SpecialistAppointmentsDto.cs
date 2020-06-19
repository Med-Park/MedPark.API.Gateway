using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedPark.API.Gateway.Models.BookingService
{
    public class SpecialistAppointmentsDto
    {
        public IEnumerable<SpecialistAppointmentDto> BookingDetails { get; set; }

        public SpecialistAppointmentsDto()
        {
        }
    }
}
