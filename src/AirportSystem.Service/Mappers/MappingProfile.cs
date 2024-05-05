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
using AirportSystem.Domain.Entities.TicketStatuses;
using AirportSystem.Domain.Entities.Users;
using AirportSystem.Service.DTOs.Aircrafts;
using AirportSystem.Service.DTOs.Airports;
using AirportSystem.Service.DTOs.Assets;
using AirportSystem.Service.DTOs.Bookings;
using AirportSystem.Service.DTOs.Employees;
using AirportSystem.Service.DTOs.FlightEmployee;
using AirportSystem.Service.DTOs.Flights;
using AirportSystem.Service.DTOs.Payments;
using AirportSystem.Service.DTOs.Permissions;
using AirportSystem.Service.DTOs.Positions;
using AirportSystem.Service.DTOs.RateTickets;
using AirportSystem.Service.DTOs.RolePermission;
using AirportSystem.Service.DTOs.Tickets;
using AirportSystem.Service.DTOs.TicketStatuses;
using AirportSystem.Service.DTOs.UserRoles;
using AirportSystem.Service.DTOs.Users;
using AutoMapper;

namespace AirportSystem.Service.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserCreateModel, User>().ReverseMap();
        CreateMap<UserUpdateModel, User>().ReverseMap();
        CreateMap<User, UserViewModel>().ReverseMap();

        CreateMap<UserRolesCreateModel, UserRole>().ReverseMap();
        CreateMap<UserRolesUpdateModel, UserRole>().ReverseMap();
        CreateMap<UserRole, UserRolesViewModel>().ReverseMap();

        CreateMap<PermissionCreateModel, Permission>().ReverseMap();
        CreateMap<PermissionUpdateModel, Permission>().ReverseMap();
        CreateMap<Permission, PermissionViewModel>().ReverseMap();

        CreateMap<RolePermissionCreateModel, RolePermission>().ReverseMap();
        CreateMap<RolePermissionUpdateModel, RolePermission>().ReverseMap();
        CreateMap<RolePermission, RolePermissionViewModel>().ReverseMap();

        CreateMap<TicketStatusCreateModel, TicketStatus>().ReverseMap();
        CreateMap<TicketStatusUpdateModel, TicketStatus>().ReverseMap();
        CreateMap<TicketStatus, TicketStatusViewModel>().ReverseMap();

        CreateMap<TicketCreateModel, Ticket>().ReverseMap();
        CreateMap<TicketUpdateModel, Ticket>().ReverseMap();
        CreateMap<Ticket, TicketViewModel>().ReverseMap();

        CreateMap<RateTicketCreateModel, RateTicket>().ReverseMap();
        CreateMap<RateTicketUpdateModel, RateTicket>().ReverseMap();
        CreateMap<RateTicket, RateTicketViewModel>().ReverseMap();

        CreateMap<PositionCreateModel, Position>().ReverseMap();
        CreateMap<PositionUpdateModel, Position>().ReverseMap();
        CreateMap<Position, PositionViewModel>().ReverseMap();

        CreateMap<PaymentCreateModel, Payment>().ReverseMap();
        CreateMap<PaymentUpdateModel, Payment>().ReverseMap();
        CreateMap<Payment, PaymentViewModel>().ReverseMap();

        CreateMap<FlightCreateModel, Flight>().ReverseMap();
        CreateMap<FlightUpdateModel, Flight>().ReverseMap();
        CreateMap<Flight, FlightViewModel>().ReverseMap();

        CreateMap<FlightEmployeeCreateModel, FlightEmployee>().ReverseMap();
        CreateMap<FlightEmployeeUpdateModel, FlightEmployee>().ReverseMap();
        CreateMap<FlightEmployee, FlightEmployeeViewModel>().ReverseMap();
    
        CreateMap<EmployeeCreateModel, Employee>().ReverseMap();
        CreateMap<EmployeeUpdateModel, Employee>().ReverseMap();
        CreateMap<Employee, EmployeeViewModel>().ReverseMap();
    
        CreateMap<BookingCreateModel, Booking>().ReverseMap();
        CreateMap<BookingUpdateModel, Booking>().ReverseMap();
        CreateMap<Booking, BookingViewModel>().ReverseMap();
    
        CreateMap<AirportCreateModel, Airport>().ReverseMap();
        CreateMap<AirportUpdateModel, Airport>().ReverseMap();
        CreateMap<Airport, AirportViewModel>().ReverseMap();
    
        CreateMap<AircraftCreateModel, Aircraft>().ReverseMap();
        CreateMap<AircraftUpdateModel, Aircraft>().ReverseMap();
        CreateMap<Aircraft, AircraftViewModel>().ReverseMap();

        CreateMap<Asset, AssetViewModel>().ReverseMap();
    }
}
