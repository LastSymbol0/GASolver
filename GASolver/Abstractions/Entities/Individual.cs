using System;
using System.Collections.Generic;
using System.Linq;

namespace GASolver.Abstractions.Entities
{
    // Representation of genome - array of bytes
    public interface IIndividual : IEnumerable<bool>
    {
    }
}
