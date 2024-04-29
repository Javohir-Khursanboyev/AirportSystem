namespace AirportSystem.Service.DTOs.RateTickets;

public class RateTicketCreateModel
{
    public long TickedStatusId { get; set; }
    public long FlightId { get; set; }
    public Decimal Price { get; set; }
}
