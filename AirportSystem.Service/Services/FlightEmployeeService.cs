using AirportSystem.Data.Repositories;
using AirportSystem.Domain.Entities.FlightEmployee;
using AirportSystem.Service.Extensions;
using AirportSystem.Service.Interfaces;

namespace AirportSystem.Service.Services;

public class FlightEmployeeService : IFlightEmployeeService
{
    private readonly FlightEmployeeRepository flightEmployeeRepository;
    private readonly FlightService flightService;
    private readonly EmployeeService employeeService;
    public FlightEmployeeService(FlightEmployeeRepository flightEmployeeRepository, FlightService flightService, EmployeeService employeeService)
    {
        this.flightEmployeeRepository = flightEmployeeRepository;
        this.flightService = flightService;
        this.employeeService = employeeService;
    }
    public async Task<FlightEmployeeViewModel> CreateAsync(FlightEmployeeCreationModel model)
    {
        var existFlight = await flightService.GetByIdAsync(model.FlightId);
        var existEmployee = await employeeService.GetByIdAsync(model.EmployeeId);

        var flightEmployees = await flightEmployeeRepository.GetAllAsync();
        var existFlightEmployee = flightEmployees.FirstOrDefault(fe => fe.EmployeeId == model.EmployeeId && fe.FlightId == model.FlightId);
        if(existFlightEmployee is not null)
        {
            if(existFlightEmployee.IsDeleted)
                await UpdateAsync(existFlightEmployee.Id, model.MapTo<FlightEmployeeUpdateModel> (), true);

            throw new Exception($"This flightEmployee is already exist");
        }

        var createdFlightEmployee = await flightEmployeeRepository.InsertAsync(model.MapTo<FlightEmployees>());
        return createdFlightEmployee.MapTo<FlightEmployeeViewModel> ();
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var flightEmployees = await flightEmployeeRepository.GetAllAsync();
        var existFlightEmployee = flightEmployees.FirstOrDefault(fe => fe.Id == id && !fe.IsDeleted)
            ?? throw new Exception($"This flightEmployee is not found With this id : {id}");

        await flightEmployeeRepository.DeleteAsync(id);
        return true;
    }

    public async Task<IEnumerable<FlightEmployeeViewModel>> GetAllAsync()
    {
        var flightEmployees = await flightEmployeeRepository.GetAllAsync();
        return flightEmployees.Where(fe => !fe.IsDeleted).MapTo<FlightEmployeeViewModel> ();
    }

    public async Task<FlightEmployeeViewModel> GetByIdAsync(long id)
    {
        var flightEmployees = await flightEmployeeRepository.GetAllAsync();
        var existFlightEmployee = flightEmployees.FirstOrDefault(fe => fe.Id == id && !fe.IsDeleted)
            ?? throw new Exception($"This flightEmployee is not found With this id : {id}");

        return existFlightEmployee.MapTo<FlightEmployeeViewModel> ();
    }

    public async Task<FlightEmployeeViewModel> UpdateAsync(long id, FlightEmployeeUpdateModel model, bool isUsesDeleted)
    {
        var existFlight = await flightService.GetByIdAsync(model.FlightId);
        var existEmployee = await employeeService.GetByIdAsync(model.EmployeeId);

        var flightEmployees = await flightEmployeeRepository.GetAllAsync();
        var existFlightEmployee = new FlightEmployees();
        if (isUsesDeleted)
            existFlightEmployee = flightEmployees.FirstOrDefault(fe => fe.Id == id);
        else
            existFlightEmployee = flightEmployees.FirstOrDefault(fe => fe.Id == id && !fe.IsDeleted)
                ?? throw new Exception($"This flightEmployee is not found With this id : {id}");

        var updatedFlightEmployee = await flightEmployeeRepository.UpdateAsync(id, model.MapTo<FlightEmployees>());
        return updatedFlightEmployee.MapTo<FlightEmployeeViewModel> ();
    }
}