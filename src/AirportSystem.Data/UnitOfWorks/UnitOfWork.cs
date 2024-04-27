using AirportSystem.Data.DbContexts;
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

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    public IRepository<User> Users { get; }
    public IRepository<Asset> Assets { get; }
    public IRepository<Flight> Flights { get; }
    public IRepository<Ticket> Tickets { get; }
    public IRepository<Booking> Bookings { get; }
    public IRepository<Airport> Airports { get; }
    public IRepository<Payment> Payments { get; }
    public IRepository<Aircraft> Aircrafts { get; }
    public IRepository<UserRole> UserRoles { get; }
    public IRepository<Position> Positions { get; }
    public IRepository<Employee> Employees { get; }
    public IRepository<Permission> Permissions { get; }
    public IRepository<RateTicket> RateTickets { get; }
    public IRepository<TicketStatus> TicketStatuses { get; }
    public IRepository<FlightEmployee> FlightsEmployee { get; }
    public IRepository<RolePermission> RolePermissions { get; }

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Users = new Repository<User>(_context);
        Assets = new Repository<Asset>(_context);
        Flights = new Repository<Flight>(_context);
        Tickets = new Repository<Ticket>(_context);
        Bookings = new Repository<Booking>(_context);
        Airports = new Repository<Airport>(_context);
        Payments = new Repository<Payment>(_context);
        Aircrafts = new Repository<Aircraft>(_context);
        UserRoles = new Repository<UserRole>(_context);
        Positions = new Repository<Position>(_context);
        Employees = new Repository<Employee>(_context);
        Permissions = new Repository<Permission>(_context);
        RateTickets = new Repository<RateTicket>(_context);
        TicketStatuses = new Repository<TicketStatus>(_context);
        FlightsEmployee = new Repository<FlightEmployee>(_context);
        RolePermissions = new Repository<RolePermission>(_context);
}

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public async ValueTask<bool> SaveAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
