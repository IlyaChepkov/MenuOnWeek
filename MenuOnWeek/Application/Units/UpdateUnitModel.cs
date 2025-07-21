using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Units;

public sealed class UpdateUnitModel
{
    public required string Name { get; set; }

    public required Guid Id { get; set; }
}
