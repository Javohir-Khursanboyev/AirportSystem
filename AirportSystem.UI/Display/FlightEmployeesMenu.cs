using AirportSystem.Domain.Entities.Flight;
using AirportSystem.Domain.Entities.FlightEmployee;
using AirportSystem.Service.Services;
using Spectre.Console;

namespace AirportSystem.UI.Display;

public class FlightEmployeesMenu
{
    private readonly FlightEmployeeService flightEmployeeService;
    private readonly FlightService flightService;
    private readonly EmployeeService employeeService;
    public FlightEmployeesMenu(FlightEmployeeService flightEmployeeService, FlightService flightService, EmployeeService employeeService)
    {
        this.flightEmployeeService = flightEmployeeService;
        this.flightService = flightService;
        this.employeeService = employeeService;
    }
    public async Task DisplayAsync()
    {
        bool circle = true;

        var options = new string[] { "Create", "GetById", "Update", "Delete", "GetAll", "[red]Back[/]" };
        var title = "-- FlightEmployees --";

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
        var employees = await employeeService.GetAllAsync();

        var flight = Selection.SelectionMenu2("Flights", flights.Select(f => $"{f.Id} {f.PlaceOfArrival} -> {f.PlaceOfArrival} Time : {f.DepartureTime}").ToArray());
        var employee = Selection.SelectionMenu2("Employees", employees.Select(e => $"{e.Id} {e.FirstName} {e.LastName} {e.EmployeeType}").ToArray());
        
        var flightId = Convert.ToInt64(flight.Split()[0]);
        var employeeId = Convert.ToInt64(employee.Split()[0]);

        FlightEmployeeCreationModel flightEmployee = new()
        {
           FlightId = flightId,
           EmployeeId = employeeId,
        };
        try
        {
            var addedFlightEmployee = await flightEmployeeService.CreateAsync(flightEmployee);
            AnsiConsole.Markup("[orange3]Succesful created[/]\n");

            var table = Selection.DataTable("FlightEmployee", addedFlightEmployee);
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
        var employees = await employeeService.GetAllAsync();

        var flight = Selection.SelectionMenu2("Flights", flights.Select(f => $"{f.Id} {f.PlaceOfArrival} -> {f.PlaceOfArrival} Time : {f.DepartureTime}").ToArray());
        var employee = Selection.SelectionMenu2("Employees", employees.Select(e => $"{e.Id} {e.FirstName} {e.LastName} {e.EmployeeType}").ToArray());

        var flightId = Convert.ToInt64(flight.Split()[0]);
        var employeeId = Convert.ToInt64(employee.Split()[0]);

        FlightEmployeeUpdateModel flightEmployee = new()
        {
            FlightId = flightId,
            EmployeeId = employeeId,
        };
        try
        {
            var updatedFlightEmployee = await flightEmployeeService.UpdateAsync(id, flightEmployee);
            AnsiConsole.Markup("[orange3]Succesful updated[/]\n");

            var table = Selection.DataTable("FlightEmployee", updatedFlightEmployee);
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
        long id = AnsiConsole.Ask<long>("Enter FlightEmployee Id: ");
        while (id <= 0)
        {
            AnsiConsole.MarkupLine("[red]Was entered in the wrong format .Try again![/]");
            id = AnsiConsole.Ask<long>("Enter FlightEmployee Id: ");
        }

        try
        {
            var flightEmployee = await flightEmployeeService.GetByIdAsync(id);
            var table = Selection.DataTable("FlightEmployee", flightEmployee);
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
        long id = AnsiConsole.Ask<long>("Enter FlightEmployee Id to delete: ");
        while (id <= 0)
        {
            AnsiConsole.MarkupLine("[red]Was entered in the wrong format .Try again![/]");
            id = AnsiConsole.Ask<long>("Enter FlightEmployee Id to delete: ");
        }

        try
        {
            await flightEmployeeService.DeleteAsync(id);
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
        var flightEmployees = await flightEmployeeService.GetAllAsync();
        var table = Selection.DataTable("FlightEmployees", flightEmployees.ToArray());
        AnsiConsole.Write(table);
        Console.WriteLine("Enter any keyword to continue");
        Console.ReadKey();
        Console.Clear();
    }
}
