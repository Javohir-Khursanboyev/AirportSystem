using AirportSystem.Data.Repositories;
using AirportSystem.Service.Services;
using Spectre.Console;

namespace AirportSystem.UI.Display;

public class MainMenu
{
    private readonly CustomerRepository customerRepository;
    private readonly EmployeeRepository employeeRepository;
    private readonly AircraftRepository aircraftRepository;
    private readonly FlightRepository flightRepository;
    private readonly TicketRepository ticketRepository;
    private readonly FlightEmployeeRepository flightEmployeeRepository;
    private readonly BookingRepository bookingRepository;

    private readonly CustomerService customerService;
    private readonly EmployeeService employeeService;
    private readonly AircraftService aircraftService;
    private readonly FlightService flightService;
    private readonly TicketService ticketService;
    private readonly FlightEmployeeService flightEmployeeService;
    private readonly BookingService bookingService;

    private readonly SignInMenu signInMenu;
    private readonly SignUpMenu signUpMenu;

    public MainMenu()
    {
        customerRepository = new CustomerRepository();
        employeeRepository = new EmployeeRepository();
        aircraftRepository = new AircraftRepository();
        flightRepository = new FlightRepository();
        ticketRepository = new TicketRepository();
        flightEmployeeRepository = new FlightEmployeeRepository();
        bookingRepository = new BookingRepository();

        customerService = new CustomerService(customerRepository);
        employeeService = new EmployeeService(employeeRepository);
        aircraftService = new AircraftService(aircraftRepository);
        flightService = new FlightService(flightRepository,aircraftService);
        ticketService = new TicketService(ticketRepository,flightService);
        flightEmployeeService = new FlightEmployeeService(flightEmployeeRepository, flightService, employeeService);
        bookingService = new BookingService(bookingRepository, ticketService, customerService);

        signInMenu = new SignInMenu(employeeRepository,customerRepository,aircraftRepository, flightRepository, flightEmployeeRepository, ticketRepository, 
            bookingRepository, customerService, employeeService, aircraftService, flightService, flightEmployeeService, ticketService, bookingService);
        signUpMenu = new SignUpMenu(customerService);
    }

    public async Task Run()
    {
        var circle = true;

        while (circle)
        {
            AnsiConsole.Clear();
            var selection = Selection.SelectionMenu(
                "Airport system",
                new string[] { "Sign in", "Sign up", "[red]Exit[/]" });

            switch (selection)
            {
                case "Sign in":
                    await signInMenu.SignInAsync();
                    break;
                case "Sign up":
                    await signUpMenu.SignUpAsync();
                    break;
                case "[red]Exit[/]":
                    circle = false;
                    break;
            }
        }
    }
}
