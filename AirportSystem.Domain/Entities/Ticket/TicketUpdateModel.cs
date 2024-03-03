using AirportSystem.Domain.Enums;

namespace AirportSystem.Domain.Entities.Ticket;

public class TicketUpdateModel
{
    public long FlightId { get; set; }
    public int TicketNumber { get; set; }
    public TicketClass TicketClass { get; set; }
    public decimal Price { get; set; }
}