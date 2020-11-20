using System;
using System.Collections.Generic;

#nullable disable

namespace AuthService
{
    public partial class TestAppointment
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string PaymentType { get; set; }
        public int BookingId { get; set; }
    }
}
