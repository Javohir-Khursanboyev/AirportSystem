using AirportSystem.Data.Repositories;
using AirportSystem.Domain.Entities.Booking;
using AirportSystem.Domain.Entities.Ticket;
using AirportSystem.Service.Extensions;
using AirportSystem.Service.Interfaces;

namespace AirportSystem.Service.Services;

public class BookingService : IBookingService
{
    private readonly BookingRepository  bookingRepository;
    private readonly TicketService ticketService;
    private readonly CustomerService customerService;
    public BookingService(BookingRepository bookingRepository, TicketService ticketService, CustomerService customerService)
    {
        this.bookingRepository = bookingRepository;
        this.ticketService = ticketService;
        this.customerService = customerService;
    }
    public async Task<BookingViewModel> CreateAsync(BookingCreationModel model)
    {
        var existTicket = await ticketService.GetByIdAsync(model.TicketId);
        var existCustomer = await customerService.GetByIdAsync(model.CustomerId);

        if (existTicket.IsSold)
            throw new Exception($"This ticket is already booking");

        if (existCustomer.Balance < existTicket.Price)
            throw new Exception($"Customer balance not enough this ticket price : {existTicket.Price} customer balance : {existCustomer.Balance}");

        var createdBooking = await bookingRepository.InsertAsync(model.MapTo<Bookings>());
        existTicket.IsSold = true;
        await ticketService.UpdateAsync(existTicket.Id,existTicket.MapTo<TicketUpdateModel> ());
        await customerService.DepositAsync(existCustomer.Id,-existTicket.Price);

        return createdBooking.MapTo<BookingViewModel> ();
    }

    public async Task<bool> DeleteAsync(long id, long customerId)
    {
        var bookings = await bookingRepository.GetAllAsync();
        var existBooking = bookings.FirstOrDefault(b => b.Id == id && !b.IsDeleted && b.CustomerId == customerId)
            ?? throw new Exception($"This booking is not found With this id {id}");

        await bookingRepository.DeleteAsync(id);
        var existTicket = await ticketService.GetByIdAsync(existBooking.TicketId);
        existTicket.IsSold = false;
        await customerService.DepositAsync(existBooking.CustomerId,existTicket.Price);
        return true;
    }

    public async Task<IEnumerable<BookingViewModel>> GetAllAsync(long? customerId = null)
    {
        var bookings = await bookingRepository.GetAllAsync();
        if(customerId is not null) 
            return bookings.Where(b => !b.IsDeleted && b.CustomerId == customerId).MapTo<BookingViewModel>();

        return bookings.Where(b => !b.IsDeleted).MapTo<BookingViewModel>();
    }

    public async Task<BookingViewModel> GetByIdAsync(long id, long customerId)
    {
        var bookings = await bookingRepository.GetAllAsync();
        var existBooking = bookings.FirstOrDefault(b => b.Id == id && !b.IsDeleted && b.CustomerId == customerId)
            ?? throw new Exception($"This booking is not found With this id {id}");

        return existBooking.MapTo<BookingViewModel> ();
    }
}