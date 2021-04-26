using GASolver.Abstractions.Entities;
using GASolver.Abstractions.Options;

namespace GASolver.Implementation.Options
{
    class SolverOptions : ISolverOptions
    {
        public int GenerationsCount { get; set; }
        public int PopulationSize { get; set; }
        public double MutationsPercent { get; set; }
        public int GenomeLenght { get; set; }
        public IFitnessFunction FitnessFunction { get; set; }
    }
}
