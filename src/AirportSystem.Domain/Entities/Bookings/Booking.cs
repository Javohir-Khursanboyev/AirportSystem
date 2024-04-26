using AirportSystem.Domain.Commons;
using AirportSystem.Domain.Entities.Tickets;
using AirportSystem.Domain.Entities.Users;
using AirportSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportSystem.Domain.Entities.Bookings;

public class Booking : Auditable 
{
    public long TickedId { get; set; }
    public Ticket Ticket { get; set; }
    public long UserId { get; set; }
    public User User { get; set; }
    public DateTime PurchaseDateTime { get; set; }
    public BookingStatus BookingStatus { get; set; }
}
