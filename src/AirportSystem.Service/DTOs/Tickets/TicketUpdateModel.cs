namespace AirportSystem.Service.DTOs.Tickets;

public class TicketUpdateModel
{
    public string SeatNumber { get; set; }
    public long RateTicketsId { get; set; }
    public bool IsSold { get; set; }
}
