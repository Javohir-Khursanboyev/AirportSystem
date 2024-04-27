using AirportSystem.Domain.Entities.Aircrafts;
using AirportSystem.Domain.Entities.Airports;
using AirportSystem.Domain.Entities.Assets;
using AirportSystem.Domain.Entities.Employees;
using AirportSystem.Domain.Entities.Flights;
using AirportSystem.Domain.Entities.Positions;
using AirportSystem.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace AirportSystem.Data.DbContexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }
    public DbSet<Position> Positions { get; set; }
    public DbSet<Flight> Flights { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Asset> Assets { get; set; }
    public DbSet<Airport> Airports { get; set; }
    public DbSet<Aircraft> Aircrafts { get; set;}
}
