using AirportSystem.Domain.Commons;
using AirportSystem.Domain.Enums;

namespace AirportSystem.Domain.Entities.Ticket;

public class Tickets:Auditable
{
    public long FlightId { get; set; }
    public int TicketNumber { get; set; }
    public TicketClass TicketClass { get; set; }
    public decimal Price { get; set; }
}