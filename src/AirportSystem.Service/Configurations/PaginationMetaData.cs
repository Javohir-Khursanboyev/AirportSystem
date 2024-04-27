namespace AirportSystem.Service.Configurations;

public class PaginationMetaData
{
    public PaginationMetaData(int totalCount, PaginationParams @params)
    {
        TotalPages = Convert.ToInt32(Math.Ceiling(totalCount / (decimal)@params.PageSize));
        CurrentPage = @params.PageIndex;
    }

    public int TotalPages { get; set; }
    public int CurrentPage { get; set; }
    public bool HasPrevious => CurrentPage > 1;
    public bool HasNext => CurrentPage < TotalPages;
}
