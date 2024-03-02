using AirportSystem.Data.IRepositories;
using AirportSystem.Data.Repositories;
using AirportSystem.Domain.Entities.Employee;
using AirportSystem.Domain.Entities.Flight;
using AirportSystem.Service.Extensions;
using AirportSystem.Service.Interfaces;

namespace AirportSystem.Service.Services;

public class FlightService : IFlightService
{
    private readonly FlightRepository flightRepository;
    public FlightService(FlightRepository flightRepository)
    {
        this.flightRepository = flightRepository;
    }
    public async Task<FlightViewModel> CreateAsync(FlightCreationModel model)
    {
        var createdFlight = await flightRepository.InsertAsync(model.MapTo<Flights>());
        return createdFlight.MapTo<FlightViewModel> ();
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var flights = await flightRepository.GetAllAsync();
        var existFlight = flights.FirstOrDefault(f => f.Id == id && !f.IsDeleted)
            ?? throw new Exception($"This flight is not found With this id {id}");

        await flightRepository.DeleteAsync(id);
        return true;
    }

    public async Task<IEnumerable<FlightViewModel>> GetAllAsync()
    {
        var flights = await flightRepository.GetAllAsync();
        return flights.Where(f => !f.IsDeleted).MapTo<FlightViewModel> ();
    }

    public async Task<FlightViewModel> GetByIdAsync(long id)
    {
        var flights = await flightRepository.GetAllAsync();
        var existFlight = flights.FirstOrDefault(f => f.Id == id && !f.IsDeleted)
            ?? throw new Exception($"This flight is not found With this id {id}");

        return existFlight.MapTo<FlightViewModel> ();
    }

    public async Task<FlightViewModel> UpdateAsync(long id, FlightUpdateModel model, bool isUsesDeleted = false)
    {
        var flights = await flightRepository.GetAllAsync();
        var existFlight = new Flights();
        if (isUsesDeleted)
            existFlight = flights.FirstOrDefault(e => e.Id == id);
        else
            existFlight = flights.FirstOrDefault(e => e.Id == id && !e.IsDeleted)
                ?? throw new Exception($"This flight is not found With this id {id}");

        var updatedFlight = await flightRepository.UpdateAsync(id, model.MapTo<Flights>());
        return updatedFlight.MapTo<FlightViewModel> (); 
    }
}
