using AirportSystem.Service.DTOs.RateTickets;

namespace AirportSystem.Service.DTOs.Tickets;

public class TicketViewModel
{
    public long Id { get; set; }
    public string SeatNumber { get; set; }
    public RateTicketViewModel RateTicket { get; set; }
}
