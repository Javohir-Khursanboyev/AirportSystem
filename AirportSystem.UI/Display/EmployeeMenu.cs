using AirportSystem.Domain.Entities.Employee;
using AirportSystem.Domain.Enums;
using AirportSystem.Service.Services;
using Spectre.Console;
using System.Net;
using System.Text.RegularExpressions;

namespace AirportSystem.UI.Display;

public class EmployeeMenu
{
    private readonly EmployeeService employeeService;
    public EmployeeMenu(EmployeeService employeeService)
    {
        this.employeeService = employeeService; 
    }

    public async Task DisplayAsync()
    {
        bool circle = true;
        var options = new string[] { "Create", "GetById", "Update", "Delete", "GetAll", "[red]Back[/]" };
        var title = "-- EmployeeMenu --";

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
        string firstName = AnsiConsole.Ask<string>("FirstName:");
        string lastName = AnsiConsole.Ask<string>("LastName:");
        DateTime dateOfBirth = AnsiConsole.Ask<DateTime>("Enter dateOfBirth [blue]mm.dd.year:[/]");

        string email = AnsiConsole.Ask<string>("Email [blue](email@gmail.com):[/]");
        while (!Regex.IsMatch(email, @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]{3,}$"))
        {
            AnsiConsole.MarkupLine("[red]Was entered in the wrong format .Try again![/]");
            email = AnsiConsole.Ask<string>("Email [blue](email@gmail.com):[/]");
        }

        string passportNumber = AnsiConsole.Ask<string>("Passport number [blue]AC XXX xx xx[/]:");
        while (!Regex.IsMatch(passportNumber, @"^((AC\s)?\w{2})?\s\d{3}\s\d{2}\s\d{2}$"))
        {
            AnsiConsole.MarkupLine("[red]Invalid input.[/]");
            passportNumber = AnsiConsole.Ask<string>("Passport number [blue]AC XXX xx xx[/]:");
        }

        string phone = AnsiConsole.Ask<string>("Phone [blue](+998XXxxxxxxx):[/]");
        while (!Regex.IsMatch(phone, @"^\+998\d{9}$"))
        {
            AnsiConsole.MarkupLine("[red]Invalid input.[/]");
            phone = AnsiConsole.Ask<string>("Phone [blue](+998XXxxxxxxx):[/]");
        }
        string address = AnsiConsole.Ask<string>("Address:");
        var options = new string[] { "Pilot", "FlightAttendant", "AircraftMechanic", "AviationEngineer", "Dispatcher" };
        var selection = Selection.SelectionMenu2("EmployeeType", options);

        EmployeeCreationModel employee = new()
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            PhoneNumber = phone,
            PassportNumber = passportNumber,
            DateOfBirth = dateOfBirth,
            Address = address,
            EmployeeType = (EmployeeType)Enum.Parse(typeof(EmployeeType), selection)
        };
        try
        {
            var addedEmployee = await employeeService.CreateAsync(employee);
            AnsiConsole.Markup("[orange3]Succesful created[/]\n");

            var table = Selection.DataTable("Employee", addedEmployee);
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
        long id = AnsiConsole.Ask<long>("Enter customer Id to update: ");
        while (id <= 0)
        {
            AnsiConsole.MarkupLine("[red]Was entered in the wrong format .Try again![/]");
            id = AnsiConsole.Ask<long>("Enter customer Id to update: ");
        }
        string firstName = AnsiConsole.Ask<string>("FirstName:");
        string lastName = AnsiConsole.Ask<string>("LastName:");
        DateTime dateOfBirth = AnsiConsole.Ask<DateTime>("Enter dateOfBirth [blue]mm.dd.year:[/]");

        string email = AnsiConsole.Ask<string>("Email [blue](email@gmail.com):[/]");
        while (!Regex.IsMatch(email, @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]{3,}$"))
        {
            AnsiConsole.MarkupLine("[red]Was entered in the wrong format .Try again![/]");
            email = AnsiConsole.Ask<string>("Email [blue](email@gmail.com):[/]");
        }
        string passportNumber = AnsiConsole.Ask<string>("Passport number [blue]AC XXX xx xx[/]:");
        while (!Regex.IsMatch(passportNumber, @"^((AC\s)?\w{2})?\s\d{3}\s\d{2}\s\d{2}$"))
        {
            AnsiConsole.MarkupLine("[red]Invalid input.[/]");
            passportNumber = AnsiConsole.Ask<string>("Passport number [blue]AC XXX xx xx[/]:");
        }

        string phone = AnsiConsole.Ask<string>("Phone [blue](+998XXxxxxxxx):[/]");
        while (!Regex.IsMatch(phone, @"^\+998\d{9}$"))
        {
            AnsiConsole.MarkupLine("[red]Invalid input.[/]");
            phone = AnsiConsole.Ask<string>("Phone [blue](+998XXxxxxxxx):[/]");
        }

        string address = AnsiConsole.Ask<string>("Address:");
        var options = new string[] { "Pilot", "FlightAttendant", "AircraftMechanic", "AviationEngineer", "Dispatcher" };
        var selection = Selection.SelectionMenu2("EmployeeType", options);

        EmployeeUpdateModel employee = new()
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            PhoneNumber = phone,
            PassportNumber = passportNumber,
            DateOfBirth = dateOfBirth,
            Address = address,
            EmployeeType = (EmployeeType)Enum.Parse(typeof(EmployeeType), selection)
        };
        try
        {
            var updatedEmployee = await employeeService.UpdateAsync(id, employee);
            AnsiConsole.Markup("[orange3]Succesful updated[/]\n");

            var table = Selection.DataTable("Employee", updatedEmployee);
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

    async Task GetAllAsync()
    {
        Console.Clear();
        var employees = await employeeService.GetAllAsync();
        var table = Selection.DataTable("Customers", employees.ToArray());
        AnsiConsole.Write(table);
        Console.WriteLine("Enter any keyword to continue");
        Console.ReadKey();
        Console.Clear();
    }

    async Task DeleteAsync()
    {
        Console.Clear();
        long id = AnsiConsole.Ask<long>("Enter employee Id to delete: ");
        while (id <= 0)
        {
            AnsiConsole.MarkupLine("[red]Was entered in the wrong format .Try again![/]");
            id = AnsiConsole.Ask<long>("Enter employee Id to delete: ");
        }

        try
        {
            await employeeService.DeleteAsync(id);
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

    async ValueTask GetByIdAsync()
    {
        Console.Clear();
        long id = AnsiConsole.Ask<long>("Enter employee Id: ");
        while (id <= 0)
        {
            AnsiConsole.MarkupLine("[red]Was entered in the wrong format .Try again![/]");
            id = AnsiConsole.Ask<long>("Enter employee Id: ");
        }

        try
        {
            var employee = await employeeService.GetByIdAsync(id);
            var table = Selection.DataTable("Employee", employee);
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
}
