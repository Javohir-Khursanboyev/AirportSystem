using AirportSystem.Domain.Commons;
using AirportSystem.Domain.Entities.TicketStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportSystem.Domain.Entities.RateTickets;

public class RateTickets : Auditable
{
    public long TickedStatusId { get; set; }
    public TicketStatus.TicketStatus TickedStatus { get; set; }
    public long FlightId { get; set; }
    
    public Decimal Price { get; set; }
}
