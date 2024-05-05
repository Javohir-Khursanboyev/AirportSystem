using AirportSystem.Data.UnitOfWorks;
using AirportSystem.Domain.Entities.Users;
using AirportSystem.Service.Configurations;
using AirportSystem.Service.DTOs.UserRoles;
using AirportSystem.Service.Exceptions;
using AirportSystem.Service.Extensions;
using AirportSystem.Service.Helper;
using AutoMapper;
using System.Data;

namespace AirportSystem.Service.Services.UserRoles;

public class UserRoleService(IUnitOfWork unitOfWork, IMapper mapper) : IUserRoleService
{
    public async ValueTask<UserRolesViewModel> CreateAsync(UserRolesCreateModel model)
    {
        var role = await unitOfWork.UserRoles.SelectAsync(r => r.Name.ToLower() == model.Name.ToLower());
        if (role is not null)
            throw new AlreadyExistException($"Role already exists this Name: {model.Name}");

        var existRole = mapper.Map<UserRole>(model);
        existRole.CreatedByUserId = HttpContextHelper.UserId;
        var createdUserRole = await unitOfWork.UserRoles.InsertAsync(existRole);
        await unitOfWork.SaveAsync();
        return mapper.Map<UserRolesViewModel>(createdUserRole);
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var role = await unitOfWork.UserRoles.SelectAsync(r => r.Id == id)
            ?? throw new NotFoundException($"Role is not fount this ID = {id}");

        await unitOfWork.UserRoles.DropAsync(role);
        await unitOfWork.SaveAsync();
        return true;
    }

    public async ValueTask<IEnumerable<UserRolesViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var userRoles = unitOfWork.UserRoles.SelectAsQueryable(isTracked : false).OrderBy(filter);
        if(string.IsNullOrEmpty(search))
            userRoles = userRoles.Where(role => role.Name.Contains(search, StringComparison.OrdinalIgnoreCase));

        return await Task.FromResult(mapper.Map<IEnumerable<UserRolesViewModel>>(userRoles.ToPaginateAsQueryable(@params)));
    }

    public async ValueTask<UserRolesViewModel> GetByIdAsync(long id)
    {
        var role = await unitOfWork.UserRoles.SelectAsync(r => r.Id == id, isTracked: false)
            ?? throw new NotFoundException($"Role is not fount this ID = {id}");

        return mapper.Map<UserRolesViewModel>(role);  
    }

    public async ValueTask<UserRolesViewModel> UpdateAsync(long id, UserRolesUpdateModel model)
    {
        var role = await unitOfWork.UserRoles.SelectAsync(r => r.Id == id)
            ?? throw new NotFoundException($"Role is not fount this ID = {id}");

        var existRole = await unitOfWork.UserRoles.SelectAsync(r => r.Name.ToLower() ==  model.Name.ToLower());
        if (existRole is not null)
            throw new AlreadyExistException($"Role already exists this Name: {model.Name}");


        existRole.Id = id;
        existRole.Name = model.Name;
        existRole.UpdatedAt = DateTime.UtcNow;
        existRole.UpdatedByUserId = HttpContextHelper.UserId;
        await unitOfWork.UserRoles.UpdateAsync(role);
        await unitOfWork.SaveAsync();
        return mapper.Map<UserRolesViewModel>(existRole);
    }
}
