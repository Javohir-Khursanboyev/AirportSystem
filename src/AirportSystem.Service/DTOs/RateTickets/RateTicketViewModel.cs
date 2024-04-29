using AirportSystem.Service.DTOs.Flights;
using AirportSystem.Service.DTOs.TicketStatuses;

namespace AirportSystem.Service.DTOs.RateTickets;

public class RateTicketViewModel
{
    public long Id { get; set; }
    public TicketStatusViewModel TicketStatus { get; set; }
    public FlightViewModel Flight { get; set; }
    public Decimal Price { get; set; }
}
