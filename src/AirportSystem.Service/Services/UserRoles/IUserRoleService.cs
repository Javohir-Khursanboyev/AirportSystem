using AirportSystem.Domain.Entities.Users;
using AirportSystem.Service.Configurations;
using AirportSystem.Service.DTOs.UserRoles;

namespace AirportSystem.Service.Services.UserRoles;

public interface IUserRoleService
{
    ValueTask<UserRolesViewModel> CreateAsync(UserRolesCreateModel model);
    ValueTask<UserRolesViewModel> UpdateAsync(long id, UserRolesUpdateModel model);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<UserRolesViewModel> GetByIdAsync(long id);
    ValueTask<IEnumerable<UserRolesViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}
