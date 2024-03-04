using AirportSystem.Domain.Configurations;
using AirportSystem.Domain.Entities.Flight;
using AirportSystem.Service.Services;
using Spectre.Console;
using System.Reflection.Metadata;

namespace AirportSystem.UI.Display;

public class FlightMenu
{
    private readonly FlightService flightService;
    private readonly AircraftService aircraftService;
    public FlightMenu(FlightService flightService , AircraftService aircraftService)
    {
        this.flightService = flightService;
        this.aircraftService = aircraftService;
    }
    public async Task DisplayAsync()
    {
        bool circle = true;

        var options = new string[] { "Create", "GetById", "Update", "Delete", "GetAll", "[red]Back[/]" };
        var title = "-- FlightMenu --";

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
        var aircrafts = await aircraftService.GetAllAsync();
        var placeOfDeparture = Selection.SelectionMenu2("From :", Constants.optionPlaces);
        var placeOfArrival = Selection.SelectionMenu2("To :", Constants.optionPlaces);

        DateTime departureTime = AnsiConsole.Ask<DateTime>("Enter  departureTime (mm.dd.yyyy.hh:mm): ");
        while (departureTime < DateTime.Now)
        {
            AnsiConsole.MarkupLine("[red]Was entered in the wrong format .Try again![/]");
            departureTime = AnsiConsole.Ask<DateTime>("Enter  departureTime (mm.dd.yyyy.hh:mm): ");
        }

        DateTime arrivalTime = AnsiConsole.Ask<DateTime>("Enter  arrivalTime (mm.dd.yyyy.hh:mm): ");
        while (arrivalTime < departureTime)
        {
            AnsiConsole.MarkupLine("[red]Was entered in the wrong format .Try again![/]");
            arrivalTime = AnsiConsole.Ask<DateTime>("Enter  arrivalTime (mm.dd.yyyy.hh:mm): ");
        }
        
        var selection = Selection.SelectionMenu2("Aircrafts",aircrafts.Select(a => $"{a.Id} {a.Name}").ToArray());
        var aircraftId = Convert.ToInt64(selection.Split()[0]);
        FlightCreationModel flight = new()
        {
            AircraftId = aircraftId,    
            PlaceOfDeparture =  placeOfDeparture,
            PlaceOfArrival = placeOfArrival,
            DepartureTime = departureTime,
            ArrivalTime = arrivalTime
        };
        try
        {
            var addedFlight = await flightService.CreateAsync(flight);
            AnsiConsole.Markup("[orange3]Succesful created[/]\n");

            var table = Selection.DataTable("Flight", addedFlight);
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
        var aircrafts = await aircraftService.GetAllAsync();
        var placeOfDeparture = Selection.SelectionMenu2("From :", Constants.optionPlaces);
        var placeOfArrival = Selection.SelectionMenu2("To :", Constants.optionPlaces);

        DateTime departureTime = AnsiConsole.Ask<DateTime>("Enter  departureTime (mm.dd.yyyy.hh:mm): ");
        while (departureTime < DateTime.Now)
        {
            AnsiConsole.MarkupLine("[red]Was entered in the wrong format .Try again![/]");
            departureTime = AnsiConsole.Ask<DateTime>("Enter  departureTime (mm.dd.yyyy.hh:mm): ");
        }

        DateTime arrivalTime = AnsiConsole.Ask<DateTime>("Enter  arrivalTime (mm.dd.yyyy.hh:mm): ");
        while (arrivalTime < departureTime)
        {
            AnsiConsole.MarkupLine("[red]Was entered in the wrong format .Try again![/]");
            arrivalTime = AnsiConsole.Ask<DateTime>("Enter  arrivalTime (mm.dd.yyyy.hh:mm): ");
        }

        var selection = Selection.SelectionMenu2("Aircrafts", aircrafts.Select(a => $"{a.Id} {a.Name}").ToArray());
        var aircraftId = Convert.ToInt64(selection.Split()[0]);

        FlightUpdateModel flight = new()
        {
            AircraftId = aircraftId,
            PlaceOfDeparture = placeOfDeparture,
            PlaceOfArrival = placeOfArrival,
            DepartureTime = departureTime,
            ArrivalTime = arrivalTime
        };

        try
        {
            var updatedFlight = await flightService.UpdateAsync(id, flight);
            AnsiConsole.Markup("[orange3]Succesful updated[/]\n");

            var table = Selection.DataTable("Flight", updatedFlight);
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
        long id = AnsiConsole.Ask<long>("Enter flight Id: ");
        while (id <= 0)
        {
            AnsiConsole.MarkupLine("[red]Was entered in the wrong format .Try again![/]");
            id = AnsiConsole.Ask<long>("Enter flight Id: ");
        }

        try
        {
            var flight = await flightService.GetByIdAsync(id);
            var table = Selection.DataTable("Flight", flight);
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
        long id = AnsiConsole.Ask<long>("Enter flight Id to delete: ");
        while (id <= 0)
        {
            AnsiConsole.MarkupLine("[red]Was entered in the wrong format .Try again![/]");
            id = AnsiConsole.Ask<long>("Enter flight Id to delete: ");
        }

        try
        {
            await flightService.DeleteAsync(id);
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
        var flights = await flightService.GetAllAsync();
        var table = Selection.DataTable("Flights", flights.ToArray());
        AnsiConsole.Write(table);
        Console.WriteLine("Enter any keyword to continue");
        Console.ReadKey();
        Console.Clear();
    }
}