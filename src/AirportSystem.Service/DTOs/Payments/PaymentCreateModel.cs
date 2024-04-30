using AirportSystem.Domain.Enums;

namespace AirportSystem.Service.DTOs.Payments;

public class PaymentCreateModel
{
    public long BookingId { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
}
