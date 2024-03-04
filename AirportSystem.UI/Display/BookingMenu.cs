using AirportSystem.Domain.Entities.Booking;
using AirportSystem.Domain.Entities.FlightEmployee;
using AirportSystem.Service.Interfaces;
using AirportSystem.Service.Services;
using Spectre.Console;

namespace AirportSystem.UI.Display;

public class BookingMenu
{
    private readonly BookingService bookingService;
    private readonly FlightService flightService;
    private readonly TicketService ticketService;
    public BookingMenu(BookingService bookingService, TicketService ticketService, FlightService flightService)
    {
        this.bookingService = bookingService;
        this.ticketService = ticketService;
        this.flightService = flightService;
    }

    public async Task BookingAsync(long customerId)
    {
        Console.Clear();

        DateTime departureDate = AnsiConsole.Ask<DateTime>("Enter  departure date (mm.dd.yyyy): ");
       
        try
        {
            var flights = await flightService.GetAllAsync(departureDate);
            var flight = Selection.SelectionMenu2("Flights", flights.Select(f => $"{f.Id} {f.PlaceOfDeparture} -> {f.PlaceOfArrival} Time : {f.DepartureTime}").ToArray());
            var flightId = Convert.ToInt64(flight.Split()[0]);

            var tickets = await ticketService.GetAllAsync(flightId);
            var ticket = Selection.SelectionMenu2("Tickets", tickets.Select(t => $"{t.Id} Number :{t.TicketNumber} {t.TicketClass} Price : {t.Price}").ToArray());

            var ticketId = Convert.ToInt64(ticket.Split()[0]);

            BookingCreationModel booking = new()
            {
                TicketId = ticketId,
                CustomerId = customerId,
            };
            var addedBooking = await bookingService.CreateAsync(booking);
            AnsiConsole.Markup("[orange3]Succesful created[/]\n");

            var table = Selection.DataTable("Booking", addedBooking);
            AnsiConsole.Write(table);
        }
        catch (Exception ex)
        {
            AnsiConsole.Markup($"[red]{ex.Message}[/]\n");
        }
        Console.WriteLine("Enter any keyword to continue");
        Console.ReadKey();
        Console.Clear();
    }

    public async Task ReturnTicketAsync(long customerId)
    {
        Console.Clear();
        long id = AnsiConsole.Ask<long>("Enter booking Id : ");
        while (id <= 0)
        {
            AnsiConsole.MarkupLine("[red]Was entered in the wrong format .Try again![/]");
            id = AnsiConsole.Ask<long>("Enter booking Id : ");
        }

        try
        {
            await bookingService.DeleteAsync(id,customerId);
            AnsiConsole.Markup("[orange3]Succesful return [/]\n");
        }
        catch (Exception ex)
        {
            AnsiConsole.Markup($"[red]{ex.Message}[/]\n");
        }
        Console.WriteLine("Enter any keyword to continue");
        Console.ReadKey();
        Console.Clear();
    }
    public async Task GetByIdAsync(long customerId)
    {
        Console.Clear();
        long id = AnsiConsole.Ask<long>("Enter booking Id: ");
        while (id <= 0)
        {
            AnsiConsole.MarkupLine("[red]Was entered in the wrong format .Try again![/]");
            id = AnsiConsole.Ask<long>("Enter booking Id: ");
        }

        try
        {
            var booking = await bookingService.GetByIdAsync(id,customerId);
            var table = Selection.DataTable("Booking", booking);
            AnsiConsole.Write(table);
        }
        catch (Exception ex)
        {
            AnsiConsole.Markup($"[red]{ex.Message}[/]\n");
        }
        Console.WriteLine("Enter any keyword to continue");
        Console.ReadKey();
        Console.Clear();
    }
    public async Task GetAllAsync(long customerId)
    {
        Console.Clear();
        var bookings = await bookingService.GetAllAsync(customerId);
        var table = Selection.DataTable("Bookings", bookings.ToArray());
        AnsiConsole.Write(table);
        Console.WriteLine("Enter any keyword to continue");
        Console.ReadKey();
        Console.Clear();
    }
}