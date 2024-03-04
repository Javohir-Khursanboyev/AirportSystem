using AirportSystem.Data.Repositories;
using AirportSystem.Domain.Entities.Ticket;
using AirportSystem.Service.Extensions;
using AirportSystem.Service.Interfaces;

namespace AirportSystem.Service.Services;

public class TicketService : ITicketService
{
    private readonly TicketRepository ticketRepository;
    private readonly FlightService flightService;
    public TicketService(TicketRepository ticketRepository, FlightService flightService)
    {
        this.ticketRepository = ticketRepository;
        this.flightService = flightService;
    }
    public async Task<TicketViewModel> CreateAsync(TicketCreationModel model)
    {
        var existFlight = await flightService.GetByIdAsync(model.FlightId);

        var tickets = await ticketRepository.GetAllAsync();
        var existTicket = tickets.FirstOrDefault(t => t.TicketNumber == model.TicketNumber);
        if (existTicket != null)
        {
            if (existTicket.IsDeleted)
                return await UpdateAsync(existTicket.Id, model.MapTo<TicketUpdateModel>(), true);

            throw new Exception($"This ticket is already exist With this ticketNumber : {model.TicketNumber}");
        }

        var createdTicket = await ticketRepository.InsertAsync(model.MapTo<Tickets>());
        return createdTicket.MapTo<TicketViewModel>();
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var tickets = await ticketRepository.GetAllAsync();
        var existTicket = tickets.FirstOrDefault(t => t.Id == id && !t.IsDeleted)
            ?? throw new Exception($"This ticket is not found With this id : {id}");

        await ticketRepository.DeleteAsync(id);
        return true;
    }

    public async Task<IEnumerable<TicketViewModel>> GetAllAsync(long? flightId = null)
    {
        var tickets = await ticketRepository.GetAllAsync();
        if (flightId is not null)
        {
            var result = tickets.Where(t => !t.IsDeleted && t.FlightId == flightId);
            if (!result.Any())
                throw new Exception($"There are no tickets on this flight");
        }

        return tickets.Where(t => !t.IsDeleted).MapTo<TicketViewModel>();
    }

    public async Task<TicketViewModel> GetByIdAsync(long id)
    {
        var tickets = await ticketRepository.GetAllAsync();
        var existTicket = tickets.FirstOrDefault(t => t.Id == id && !t.IsDeleted)
            ?? throw new Exception($"This ticket is not found With this id : {id}");

        return existTicket.MapTo<TicketViewModel>();
    }

    public async Task<TicketViewModel> UpdateAsync(long id, TicketUpdateModel model, bool isUsesDeleted = false)
    {
        var existFlight = await flightService.GetByIdAsync(model.FlightId);

        var tickets = await ticketRepository.GetAllAsync();
        var existTicket = new Tickets();
        if (isUsesDeleted)
            existTicket = tickets.FirstOrDefault(t => t.Id == id);
        else
            existTicket = tickets.FirstOrDefault(t => t.Id == id && !t.IsDeleted)
                ?? throw new Exception($"This ticket is not found With this id : {id}");

        var updatedTicket = ticketRepository.UpdateAsync(id, model.MapTo<Tickets>());
        return updatedTicket.MapTo<TicketViewModel>();
    }
}