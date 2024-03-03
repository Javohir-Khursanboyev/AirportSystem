using AirportSystem.Domain.Entities.Aircraft;
using AirportSystem.Service.Services;
using Spectre.Console;

namespace AirportSystem.UI.Display;

public class AircraftMenu
{
    private readonly AircraftService aircraftService;
    public AircraftMenu(AircraftService aircraftService)
    {
        this.aircraftService = aircraftService;
    }
    public async Task DisplayAsync()
    {
        bool circle = true;

        var options = new string[] { "Create", "GetById", "Update", "Delete", "GetAll", "[red]Back[/]" };
        var title = "-- AircraftMenu --";

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
        string name = AnsiConsole.Ask<string>("Aircraft name:");
        int totalNumberOfSeats = AnsiConsole.Ask<int>("Total Number Of Seats:");
        while (totalNumberOfSeats <= 0)
        {
            AnsiConsole.MarkupLine("[red]Was entered in the wrong format .Try again![/]");
            totalNumberOfSeats = AnsiConsole.Ask<int>("Total Number Of Seats:");
        }
        AircraftCreationModel aircraft = new()
        {
            Name = name,
            TotalNumberOfSeats = totalNumberOfSeats
        };
        try
        {
            var addedAircraft = await aircraftService.CreateAsync(aircraft);
            AnsiConsole.Markup("[orange3]Succesful created[/]\n");

            var table = Selection.DataTable("Aircraft", addedAircraft);
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
        long id = AnsiConsole.Ask<long>("Enter aircraft Id to update: ");
        while (id <= 0)
        {
            AnsiConsole.MarkupLine("[red]Was entered in the wrong format .Try again![/]");
            id = AnsiConsole.Ask<long>("Enter aircraft Id to update: ");
        }
        string name = AnsiConsole.Ask<string>("Car category name:");
        int totalNumberOfSeats = AnsiConsole.Ask<int>("Total Number Of Seats:");
        while (totalNumberOfSeats <= 0)
        {
            AnsiConsole.MarkupLine("[red]Was entered in the wrong format .Try again![/]");
            totalNumberOfSeats = AnsiConsole.Ask<int>("Total Number Of Seats:");
        }
        AircraftUpdateModel aircraft = new()
        {
            Name = name,
            TotalNumberOfSeats = totalNumberOfSeats
        };

        try
        {
            var updatedAircraft = await aircraftService.UpdateAsync(id, aircraft);
            AnsiConsole.Markup("[orange3]Succesful updated[/]\n");

            var table = Selection.DataTable("Aircraft", updatedAircraft);
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
        long id = AnsiConsole.Ask<long>("Enter aircraft Id: ");
        while (id <= 0)
        {
            AnsiConsole.MarkupLine("[red]Was entered in the wrong format .Try again![/]");
            id = AnsiConsole.Ask<long>("Enter aircraft Id: ");
        }

        try
        {
            var aircraft = await aircraftService.GetByIdAsync(id);
            var table = Selection.DataTable("Aircraft", aircraft);
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
        long id = AnsiConsole.Ask<long>("Enter aircraft Id to delete: ");
        while (id <= 0)
        {
            AnsiConsole.MarkupLine("[red]Was entered in the wrong format .Try again![/]");
            id = AnsiConsole.Ask<long>("Enter aircraft Id to delete: ");
        }

        try
        {
            await aircraftService.DeleteAsync(id);
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
        var aircrafts = await aircraftService.GetAllAsync();
        var table = Selection.DataTable("Aircrafts", aircrafts.ToArray());
        AnsiConsole.Write(table);
        Console.WriteLine("Enter any keyword to continue");
        Console.ReadKey();
        Console.Clear();
    }
}
