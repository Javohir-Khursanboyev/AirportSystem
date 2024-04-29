using AirportSystem.Domain.Enums;
using AirportSystem.Service.DTOs.Bookings;

namespace AirportSystem.Service.DTOs.Payments;

public class PaymentViewModel
{
    public long Id { get; set; }
    public BookingViewModel Booking { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
}
