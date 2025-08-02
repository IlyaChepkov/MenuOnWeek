using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Units;

public sealed record UpdateUnitCommand(Guid Id, string? Name);
