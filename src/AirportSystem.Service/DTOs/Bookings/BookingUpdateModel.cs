using AirportSystem.Domain.Enums;

namespace AirportSystem.Service.DTOs.Bookings;

public class BookingUpdateModel
{
    public long TickedId { get; set; }
    public DateTime PurchaseDateTime { get; set; }
    public BookingStatus BookingStatus { get; set; }
}
