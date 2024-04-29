namespace AirportSystem.Service.DTOs.Tickets;

public class TicketCreateModel
{
    public string SeatNumber { get; set; }
    public long RateTicketId { get; set; }
    public bool IsSold { get; set; }
}
