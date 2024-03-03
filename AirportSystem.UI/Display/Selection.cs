using AirportSystem.Domain.Entities.Aircraft;
using AirportSystem.Domain.Entities.Customer;
using AirportSystem.Domain.Entities.Employee;
using AirportSystem.Domain.Entities.Flight;
using AirportSystem.Domain.Entities.FlightEmployee;
using Spectre.Console;

namespace AirportSystem.UI.Display;

public static class Selection
{
    public static string SelectionMenu(string title, string[] options)
    {
        var selection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title($"[darkorange3_1]{title}[/]")
                .PageSize(5) 
                .AddChoices(options)
                .HighlightStyle(new Style(foreground: Color.Cyan1, background: Color.Blue))
        );

        return selection;
    }

    public static string SelectionMenu2(string title, string[] options)
    {
        var selection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title($"[darkorange3_1]{title}[/]")
                .PageSize(3)
                .AddChoices(options)
                .HighlightStyle(new Style(foreground: Color.Cyan1, background: Color.Blue))
        );

        return selection;
    }

    public static Table DataTable(string title, params CustomerViewModel[] customers)
    {
        var table = new Table();

        table.AddColumn("[slateblue1]Id[/]");
        table.AddColumn("[slateblue1]FirstName[/]");
        table.AddColumn("[slateblue1]LastName[/]");
        table.AddColumn("[slateblue1]Email[/]");
        table.AddColumn("[slateblue1]PhoneNumber[/]");
        table.AddColumn("[slateblue1]DateOfBirth[/]");
        table.AddColumn("[slateblue1]PassportNumber[/]");
        table.AddColumn("[slateblue1]Balance[/]");

        foreach (var customer in customers)
            table.AddRow(customer.Id.ToString(), customer.FirstName, customer.LastName, customer.Email,customer.PhoneNumber, 
                customer.DateOfBirth.ToString(),customer.PassportNumber,customer.Balance.ToString());

        return table;
    }

    public static Table DataTable(string title, params EmployeeViewModel[] employees)
    {
        var table = new Table();

        table.AddColumn("[slateblue1]Id[/]");
        table.AddColumn("[slateblue1]FirstName[/]");
        table.AddColumn("[slateblue1]LastName[/]");
        table.AddColumn("[slateblue1]Email[/]");
        table.AddColumn("[slateblue1]PhoneNumber[/]");
        table.AddColumn("[slateblue1]DateOfBirth[/]");
        table.AddColumn("[slateblue1]PassportNumber[/]");
        table.AddColumn("[slateblue1]Address[/]");
        table.AddColumn("[slateblue1]EmployeeType[/]");

        foreach (var employee in employees)
            table.AddRow(employee.Id.ToString(), employee.FirstName, employee.LastName, employee.Email, employee.PhoneNumber,
                employee.DateOfBirth.ToString(), employee.PassportNumber,employee.Address, employee.EmployeeType.ToString());

        return table;
    }

    public static Table DataTable(string title, params AircraftViewModel[] aircrafts)
    {
        var table = new Table();

        table.AddColumn("[slateblue1]Id[/]");
        table.AddColumn("[slateblue1]Name[/]");
        table.AddColumn("[slateblue1]TotalNumberOfSeats[/]");

        foreach (var aircraft in aircrafts)
            table.AddRow(aircraft.Id.ToString(), aircraft.Name, aircraft.TotalNumberOfSeats.ToString());
             
        return table;
    }

    public static Table DataTable(string title, params FlightEmployeeViewModel[] flightEmployees)
    {
        var table = new Table();

        table.AddColumn("[slateblue1]Id[/]");
        table.AddColumn("[slateblue1]FlightId[/]");
        table.AddColumn("[slateblue1]EmployeeId[/]");

        foreach (var flightEmployee in flightEmployees)
            table.AddRow(flightEmployee.Id.ToString(), flightEmployee.FlightId.ToString(), flightEmployee.EmployeeId.ToString());

        return table;
    }

    public static Table DataTable(string title, params FlightViewModel[] flights)
    {
        var table = new Table();

        table.AddColumn("[slateblue1]Id[/]");
        table.AddColumn("[slateblue1]AircraftId[/]");
        table.AddColumn("[slateblue1]PlaceOfDeparture[/]");
        table.AddColumn("[slateblue1]PlaceOfArrival[/]");
        table.AddColumn("[slateblue1]DepartureTime[/]");
        table.AddColumn("[slateblue1]ArrivalTime[/]");

        foreach (var flight in flights)
            table.AddRow(flight.Id.ToString(), flight.AircraftId.ToString(), flight.PlaceOfDeparture, flight.PlaceOfArrival,
                flight.DepartureTime.ToString(), flight.ArrivalTime.ToString());

        return table;
    }
}