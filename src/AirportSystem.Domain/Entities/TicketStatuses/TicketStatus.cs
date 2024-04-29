using AirportSystem.Domain.Commons;

namespace AirportSystem.Domain.Entities.TicketStatuses;

public class TicketStatus : Auditable
{
    public string Name { get; set; }  
}
