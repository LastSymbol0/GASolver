using GASolver.Abstractions.Entities;

namespace GASolver.Abstractions.Options
{
    public interface ISolverOptions
    {
        int GenerationsCount { set; get; }
        int PopulationSize { set; get; }
        double MutationsPercent { set; get; }
        int GenomeLenght { set; get; }
        IFitnessFunction FitnessFunction { get; set; }
    }
}
