using AirportSystem.Domain.Enums;

namespace AirportSystem.Service.DTOs.Bookings;

public class BookingCreateModel
{
    public long TickedId { get; set; }
    public long UserId { get; set; }
    public DateTime PurchaseDateTime { get; set; }
    public BookingStatus BookingStatus { get; set; }
}
