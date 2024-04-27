using AirportSystem.Data.Repositories;
using AirportSystem.Domain.Entities.Aircrafts;
using AirportSystem.Domain.Entities.Airports;
using AirportSystem.Domain.Entities.Assets;
using AirportSystem.Domain.Entities.Bookings;
using AirportSystem.Domain.Entities.Employees;
using AirportSystem.Domain.Entities.Flights;
using AirportSystem.Domain.Entities.Payments;
using AirportSystem.Domain.Entities.Positions;
using AirportSystem.Domain.Entities.RateTickets;
using AirportSystem.Domain.Entities.Tickets;
using AirportSystem.Domain.Entities.TicketSituations;
using AirportSystem.Domain.Entities.Users;

namespace AirportSystem.Data.UnitOfWorks;

public interface IUnitOfWork : IDisposable
{
    IRepository<User> Users {  get; }
    IRepository<Asset> Assets { get; }
    IRepository<Flight> Flights { get; }
    IRepository<Ticket> Tickets { get;   }
    IRepository<Booking> Bookings { get; }
    IRepository<Airport> Airports { get; }
    IRepository<Payment> Payments { get; }
    IRepository<Aircraft> Aircrafts { get; }
    IRepository<UserRole> UserRoles { get; }
    IRepository<Position> Positions { get; }
    IRepository<Employee> Employees { get; }
    IRepository<Permission> Permissions { get; }
    IRepository<RateTicket> RateTickets { get; }
    IRepository<TicketStatus> TicketStatuses { get; }
    IRepository<FlightEmployee> FlightsEmployee { get; }
    IRepository<RolePermission> RolePermissions { get; }

    ValueTask<bool> SaveAsync ();
}
