using AirportSystem.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportSystem.Domain.Entities.TicketStatus;

public class TicketStatus : Auditable
{
    public String Name { get; set; }
}
