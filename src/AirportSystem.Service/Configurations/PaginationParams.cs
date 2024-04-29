namespace AirportSystem.Service.Configurations;

public class PaginationParams
{
    public PaginationParams(int pageIndex, int pageSize)
    {
        this.PageIndex = pageIndex;
        this.PageSize = pageSize;
    }

    public PaginationParams() { }

    public int PageIndex { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}
