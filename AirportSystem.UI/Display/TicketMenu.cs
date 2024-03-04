using AirportSystem.Domain.Entities.Flight;
using AirportSystem.Domain.Entities.Ticket;
using AirportSystem.Domain.Enums;
using AirportSystem.Service.Services;
using Spectre.Console;

namespace AirportSystem.UI.Display;

public class TicketMenu
{
    private readonly TicketService ticketService;
    private readonly FlightService flightService;
    public TicketMenu(TicketService ticketService, FlightService flightService)
    {
        this.ticketService = ticketService;
        this.flightService = flightService;
    }
    public async Task DisplayAsync()
    {
        bool circle = true;

        var options = new string[] { "Create", "GetById", "Update", "Delete", "GetAll", "[red]Back[/]" };
        var title = "-- TicketMenu --";

        while (circle)
        {
            AnsiConsole.Clear();
            var selection = Selection.SelectionMenu(title, options);
            switch (selection)
            {
                case "Create":
                    await CreateAsync();
                    break;
                case "GetById":
                    await GetByIdAsync();
                    break;
                case "Update":
                    await UpdateAsync();
                    break;
                case "Delete":
                    await DeleteAsync();
                    break;
                case "GetAll":
                    await GetAllAsync();
                    break;
                case "[red]Back[/]":
                    circle = false;
                    break;
            }
        }
    }

    async Task CreateAsync()
    {
        Console.Clear();
        var flights = await flightService.GetAllAsync();
        var flight = Selection.SelectionMenu2("Flights", flights.Select(f => $"{f.Id} {f.PlaceOfDeparture} -> {f.PlaceOfArrival} Time : {f.DepartureTime}").ToArray());
        int ticketNumber = AnsiConsole.Ask<int>("Enter ticket number : ");
        while (ticketNumber <= 0)
        {
            AnsiConsole.MarkupLine("[red]Was entered in the wrong format .Try again![/]");
            ticketNumber = AnsiConsole.Ask<int>("Enter ticket number : ");
        }

        var options = new string[] { "Economy", "Premium", "Business" };
        var ticketClass = Selection.SelectionMenu2("TicketClass", options);
        var flightId = Convert.ToInt64(flight.Split()[0]);

        int price = AnsiConsole.Ask<int>("Enter ticket price : ");
        while (price <= 0)
        {
            AnsiConsole.MarkupLine("[red]Was entered in the wrong format .Try again![/]");
            price = AnsiConsole.Ask<int>("Enter ticket price : ");
        }

        TicketCreationModel ticket = new()
        {
            FlightId = flightId,
            TicketNumber = ticketNumber,
            TicketClass = (TicketClass)Enum.Parse(typeof(TicketClass), ticketClass),
            Price = price
        };
        try
        {
            var addedTicket = await ticketService.CreateAsync(ticket);
            AnsiConsole.Markup("[orange3]Succesful created[/]\n");

            var table = Selection.DataTable("Ticket", addedTicket);
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

    async Task UpdateAsync()
    {
        Console.Clear();
        long id = AnsiConsole.Ask<long>("Enter flight Id to update: ");
        while (id <= 0)
        {
            AnsiConsole.MarkupLine("[red]Was entered in the wrong format .Try again![/]");
            id = AnsiConsole.Ask<long>("Enter flight Id to update: ");
        }
        var flights = await flightService.GetAllAsync();
        var flight = Selection.SelectionMenu2("Flights", flights.Select(f => $"{f.Id} {f.PlaceOfDeparture} -> {f.PlaceOfArrival} Time : {f.DepartureTime}").ToArray());
        int ticketNumber = AnsiConsole.Ask<int>("Enter ticket number : ");
        while (ticketNumber <= 0)
        {
            AnsiConsole.MarkupLine("[red]Was entered in the wrong format .Try again![/]");
            ticketNumber = AnsiConsole.Ask<int>("Enter ticket number : ");
        }

        var options = new string[] { "Economy", "Premium", "Business" };
        var ticketClass = Selection.SelectionMenu2("TicketClass", options);
        var flightId = Convert.ToInt64(flight.Split()[0]);

        int price = AnsiConsole.Ask<int>("Enter ticket price : ");
        while (price <= 0)
        {
            AnsiConsole.MarkupLine("[red]Was entered in the wrong format .Try again![/]");
            price = AnsiConsole.Ask<int>("Enter ticket price : ");
        }

        TicketUpdateModel ticket = new()
        {
            FlightId = flightId,
            TicketNumber = ticketNumber,
            TicketClass = (TicketClass)Enum.Parse(typeof(TicketClass), ticketClass),
            Price = price
        };

        try
        {
            var updatedTicket = await ticketService.UpdateAsync(id, ticket);
            AnsiConsole.Markup("[orange3]Succesful updated[/]\n");

            var table = Selection.DataTable("Ticket", updatedTicket);
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

    async Task GetByIdAsync()
    {
        Console.Clear();
        long id = AnsiConsole.Ask<long>("Enter ticket Id: ");
        while (id <= 0)
        {
            AnsiConsole.MarkupLine("[red]Was entered in the wrong format .Try again![/]");
            id = AnsiConsole.Ask<long>("Enter ticket Id: ");
        }

        try
        {
            var ticket = await ticketService.GetByIdAsync(id);
            var table = Selection.DataTable("Ticket", ticket);
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

    async Task DeleteAsync()
    {
        Console.Clear();
        long id = AnsiConsole.Ask<long>("Enter ticket Id to delete: ");
        while (id <= 0)
        {
            AnsiConsole.MarkupLine("[red]Was entered in the wrong format .Try again![/]");
            id = AnsiConsole.Ask<long>("Enter ticket Id to delete: ");
        }

        try
        {
            await ticketService.DeleteAsync(id);
            AnsiConsole.Markup("[orange3]Succesful deleted[/]\n");
        }
        catch (Exception ex)
        {
            AnsiConsole.Markup($"[red]{ex.Message}[/]\n");
        }
        Console.WriteLine("Enter any keyword to continue");
        Console.ReadKey();
        Console.Clear();
    }

    async Task GetAllAsync()
    {
        Console.Clear();
        var tickets = await ticketService.GetAllAsync();
        var table = Selection.DataTable("Tickets", tickets.ToArray());
        AnsiConsole.Write(table);
        Console.WriteLine("Enter any keyword to continue");
        Console.ReadKey();
        Console.Clear();
    }
}
