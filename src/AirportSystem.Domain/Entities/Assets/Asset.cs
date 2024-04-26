using AirportSystem.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportSystem.Domain.Entities.Assets;

public class Asset : Auditable
{
    public string Name { get; set; }
    public string Path { get; set; }
}
