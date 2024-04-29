using AirportSystem.Service.Configurations;

namespace AirportSystem.WebApi.Extensions;

public static class ServiceCollection
{
    public static void InjectEnvironmentItems(this WebApplication app)
    {
        var paginationParams = new PaginationParams
            (Convert.ToInt32(app.Configuration.GetSection("PaginationParams: PageIndex").Value),
            Convert.ToInt32(app.Configuration.GetSection("PaginationParams: PageSize").Value));
    }
}
