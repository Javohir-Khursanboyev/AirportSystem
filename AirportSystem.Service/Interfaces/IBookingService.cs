using AirportSystem.Domain.Entities.Booking;
using AirportSystem.Domain.Entities.FlightEmployee;

namespace AirportSystem.Service.Interfaces;

public interface IBookingService
{
    Task<BookingViewModel> CreateAsync(BookingCreationModel model);
    Task<bool> DeleteAsync(long id);
    Task<IEnumerable<BookingViewModel>> GetAllAsync();
    Task<BookingViewModel> GetByIdAsync(long id);
}
