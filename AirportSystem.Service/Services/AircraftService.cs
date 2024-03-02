using AirportSystem.Data.Repositories;
using AirportSystem.Domain.Entities.Aircraft;
using AirportSystem.Domain.Entities.Customer;
using AirportSystem.Service.Extensions;
using AirportSystem.Service.Interfaces;

namespace AirportSystem.Service.Services;

public class AircraftService : IAircraftService
{
    private readonly AircraftRepository aircraftRepository;
    public AircraftService(AircraftRepository aircraftRepository)
    {
        this.aircraftRepository = aircraftRepository;
    }
    public async Task<AircraftViewModel> CreateAsync(AircraftCreationModel model)
    {
        var aircrafts = await aircraftRepository.GetAllAsync();
        var existAircraft = aircrafts.FirstOrDefault(a => a.Name.ToLower() == model.Name.ToLower());
        if(existAircraft != null)
        {
            if(existAircraft.IsDeleted) 
                await UpdateAsync(existAircraft.Id, model.MapTo<AircraftUpdateModel> () ,true);

            throw new Exception($"This aircraft is already exist With this name : {model.Name}");
        }

        var createdAircraft = await aircraftRepository.InsertAsync(model.MapTo<Aircrafts>());
        return createdAircraft.MapTo<AircraftViewModel> ();
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var aircrafts = await aircraftRepository.GetAllAsync();
        var existAircraft = aircrafts.FirstOrDefault(a => a.Id == id && !a.IsDeleted)
            ?? throw new Exception($"This aircraft is not found With this id : {id}");

        await aircraftRepository.DeleteAsync(id);
        return true;
    }

    public async Task<IEnumerable<AircraftViewModel>> GetAllAsync()
    {
        var aircrafts = await aircraftRepository.GetAllAsync();
        return aircrafts.Where(a => !a.IsDeleted).MapTo<AircraftViewModel> ();
    }

    public async Task<AircraftViewModel> GetByIdAsync(long id)
    {
        var aircrafts = await aircraftRepository.GetAllAsync();
        var existAircraft = aircrafts.FirstOrDefault(a => a.Id == id && !a.IsDeleted)
            ?? throw new Exception($"This aircraft is not found With this id : {id}");

        return existAircraft.MapTo<AircraftViewModel> ();
    }

    public async Task<AircraftViewModel> UpdateAsync(long id, AircraftUpdateModel model, bool isUsesDeleted = false)
    {
        var aircrafts = await aircraftRepository.GetAllAsync();
        var existAircraft = new Aircrafts();
        if (isUsesDeleted)
            existAircraft = aircrafts.FirstOrDefault(e => e.Id == id);
        else
            existAircraft = aircrafts.FirstOrDefault(c => c.Id == id && !c.IsDeleted)
                ?? throw new Exception($"This customer is not found With this id : {id}");

        var updatedAircraft = await aircraftRepository.UpdateAsync(id, model.MapTo<Aircrafts>());
        return updatedAircraft.MapTo<AircraftViewModel> ();
    }
}
