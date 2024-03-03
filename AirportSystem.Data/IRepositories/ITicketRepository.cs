using AirportSystem.Domain.Entities.Customer;
using AirportSystem.Domain.Entities.Ticket;

namespace AirportSystem.Data.IRepositories;

public interface ITicketRepository
{
    public Task<Tickets> InsertAsync(Tickets ticket);
    public Task<Tickets> UpdateAsync(long id, Tickets ticket);
    public Task<bool> DeleteAsync(long id);
    public Task<IEnumerable<Tickets>> GetAllAsync();
}
