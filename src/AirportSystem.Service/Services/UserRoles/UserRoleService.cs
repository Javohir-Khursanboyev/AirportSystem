using AirportSystem.Data.UnitOfWorks;
using AirportSystem.Domain.Entities.Users;
using AirportSystem.Service.Configurations;
using AirportSystem.Service.DTOs.UserRoles;
using AirportSystem.Service.Exceptions;
using AirportSystem.Service.Extensions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
        existRole.Create();
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
        if(!string.IsNullOrEmpty(search))
            userRoles = userRoles.Where(role => role.Name.ToLower().Contains(search.ToLower()));

        var paginateRoles =await userRoles.ToPaginateAsQueryable(@params).ToListAsync();
        return  mapper.Map<IEnumerable<UserRolesViewModel>>(paginateRoles);
    }

    public async ValueTask<UserRolesViewModel> GetByIdAsync(long id)
    {
        var role = await unitOfWork.UserRoles.SelectAsync(r => r.Id == id, isTracked: false)
            ?? throw new NotFoundException($"Role is not fount this ID = {id}");

        return mapper.Map<UserRolesViewModel>(role);  
    }

    public async ValueTask<UserRolesViewModel> UpdateAsync(long id, UserRolesUpdateModel model)
    {
        var existRole = await unitOfWork.UserRoles.SelectAsync(r => r.Id == id)
            ?? throw new NotFoundException($"Role is not fount this ID = {id}");

        var alreadyRole = await unitOfWork.UserRoles.SelectAsync(r => r.Name.ToLower() ==  model.Name.ToLower());
        if (alreadyRole is not null)
            throw new AlreadyExistException($"Role already exists this Name: {model.Name}");

        mapper.Map(model, existRole);
        return mapper.Map<UserRolesViewModel>(existRole);
    }
}
