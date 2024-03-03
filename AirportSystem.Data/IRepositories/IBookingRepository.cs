using AirportSystem.Domain.Entities.Aircraft;
using AirportSystem.Domain.Entities.Booking;

namespace AirportSystem.Data.IRepositories;

public interface IBookingRepository
{
    public Task<Bookings> InsertAsync(Bookings booking);
    public Task<Bookings> UpdateAsync(long id, Bookings booking);
    public Task<bool> DeleteAsync(long id);
    public Task<IEnumerable<Bookings>> GetAllAsync();
}
