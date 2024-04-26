using AirportSystem.Domain.Assets;
using AirportSystem.Domain.Positions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportSystem.Domain.Employees;

public class Employee
{
    public long UserId { get; set; }    

    public long PositionId { get; set; }
    public Position Position { get; set; }
    public long AssetId { get; set; }
    public Asset Asset { get; set; }
}
