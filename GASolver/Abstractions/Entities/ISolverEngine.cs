using System.Collections.Generic;
using GASolver.Abstractions.Options;
using GASolver.Implementation.Entities;

namespace GASolver.Abstractions.Entities
{
    interface ISolverEngine
    {
        void Init(ISolverOptions options);

        void Select();
        void Populate();
        void Mutate();

        Individual GetBestIndividual();
    }
}
