using AirportSystem.Domain.Commons;
using AirportSystem.Domain.Entities.RateTickets;

namespace AirportSystem.Domain.Entities.TicketSituations;

public class TicketStatus : Auditable
{
    public string Name { get; set; }
    
    public IEnumerable<RateTicket> RateTickets { get; set; }
}
