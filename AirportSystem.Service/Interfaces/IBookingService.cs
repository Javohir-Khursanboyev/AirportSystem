using AirportSystem.Domain.Entities.Booking;
using AirportSystem.Domain.Entities.FlightEmployee;

namespace AirportSystem.Service.Interfaces;

public interface IBookingService
{
    Task<BookingViewModel> CreateAsync(BookingCreationModel model);
    Task<bool> DeleteAsync(long id, long customerId);
    Task<IEnumerable<BookingViewModel>> GetAllAsync(long ? customerId);
    Task<BookingViewModel> GetByIdAsync(long id, long customerId);
}
