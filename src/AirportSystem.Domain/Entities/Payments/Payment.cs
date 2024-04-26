using AirportSystem.Domain.Commons;
using AirportSystem.Domain.Entities.Bookings;
using AirportSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportSystem.Domain.Entities.Payments;

public class Payment : Auditable
{
    public long BookingId { get; set; }
    public Booking Booking { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
}
