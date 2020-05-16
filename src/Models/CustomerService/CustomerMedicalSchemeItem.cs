using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedPark.API.Gateway.Models
{
    public class CustomerMedicalSchemeItem
    {
        public Guid CustomerId { get; set; }
        public Guid CustomerMedicalSchemeId { get; set; }
        public string MedicalSchemeName { get; set; }
        public string MembershipNo { get; set; }
        public bool Active { get; set; }
    }
}
