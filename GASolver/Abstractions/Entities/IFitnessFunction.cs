using System;

namespace GASolver.Abstractions.Entities
{
    public interface IFitnessFunction
    {
        long Run(IIndividual genome);
    }
}
