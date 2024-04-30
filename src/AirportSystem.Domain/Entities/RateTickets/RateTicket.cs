using AirportSystem.Domain.Commons;
using AirportSystem.Domain.Entities.Flights;
using AirportSystem.Domain.Entities.Tickets;
using AirportSystem.Domain.Entities.TicketStatuses;

namespace AirportSystem.Domain.Entities.RateTickets;

public class RateTicket : Auditable
{
    public long TickedStatusId { get; set; }
    public TicketStatus TicketStatus { get; set; }
    public long FlightId { get; set; }
    public Flight Flight { get; set; }
    public Decimal Price { get; set; }

    public IEnumerable<Ticket> Tickets { get; set; }
}
