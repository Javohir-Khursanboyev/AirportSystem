using AirportSystem.Domain.Entities.Aircraft;
using AirportSystem.Domain.Entities.Ticket;

namespace AirportSystem.Service.Interfaces;

public interface ITicketService
{
    Task<TicketViewModel> CreateAsync(TicketCreationModel model);
    Task<TicketViewModel> UpdateAsync(long id, TicketUpdateModel model, bool isUsesDeleted);
    Task<IEnumerable<TicketViewModel>> GetAllAsync();
    Task<TicketViewModel> GetByIdAsync(long id);
    Task<bool> DeleteAsync(long id);
}