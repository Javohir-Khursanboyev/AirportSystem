namespace AirportSystem.Service.DTOs.Airports;

public class AirportViewModel
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string IATACode { get; set; }
    public string ICAOCode { get; set; }
}
