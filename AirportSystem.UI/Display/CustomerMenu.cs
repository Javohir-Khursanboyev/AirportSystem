using AirportSystem.Domain.Entities.Customer;
using AirportSystem.Service.Services;
using Spectre.Console;
using System.Text.RegularExpressions;

namespace AirportSystem.UI.Display;

public class CustomerMenu
{
    private readonly CustomerService customerService;
    public CustomerMenu(CustomerService customerService)
    {
        this.customerService = customerService;
    }

    public async Task UpdateAsync(long customerId)
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

        string password = AnsiConsole.Prompt<string>(new TextPrompt<string>("Enter your password:").Secret());
        while (!Regex.IsMatch(password, @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$"))
        {
            AnsiConsole.MarkupLine("[red]Invalid input.[/]");
            password = AnsiConsole.Prompt<string>(new TextPrompt<string>("Enter your password:").Secret());
        }

        CustomerUpdateModel customer = new()
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            PhoneNumber = phone,
            PassportNumber = passportNumber,
            DateOfBirth = dateOfBirth,
            Password = password
        };
        try
        {
            var updatedCustomer = await customerService.UpdateAsync(customerId, customer);
            AnsiConsole.Markup("[orange3]Succesful updated[/]\n");

            var table = Selection.DataTable("Customer", updatedCustomer);
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

    public async Task DeleteAsync(long customerId)
    {
        try
        {
            await customerService.DeleteAsync(customerId);
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

    public async Task DepositAsync(long customerId)
    {
        Console.Clear();
        decimal amount = AnsiConsole.Ask<decimal>("Enter amount : ");
        while (amount <= 0)
        {
            AnsiConsole.MarkupLine("[red]Was entered in the wrong format .Try again![/]");
            amount = AnsiConsole.Ask<decimal>("Enter amount : ");
        }

        try
        {
            var customer = await customerService.DepositAsync(customerId, amount);
            var table = Selection.DataTable("Customer", customer);
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

    public async Task GetByIdAsync(long customerId)
    {
        Console.Clear();
        try
        {
            var customer = await customerService.GetByIdAsync(customerId);
            var table = Selection.DataTable("Customer", customer);
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
    public async Task GetAllAsync()
    {
        Console.Clear();
        var customers = await customerService.GetAllAsync();
        var table = Selection.DataTable("Customers", customers.ToArray());
        AnsiConsole.Write(table);
        Console.WriteLine("Enter any keyword to continue");
        Console.ReadKey();
        Console.Clear();
    }
}