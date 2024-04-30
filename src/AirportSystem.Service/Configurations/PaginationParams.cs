namespace AirportSystem.Service.Configurations;

public class PaginationParams
{
    public int PageIndex { get; set; } = Constants.defaultPageIndex;
    public int PageSize { get; set; } = Constants.defaultPageSize;
}
