using GASolver.Abstractions;
using GASolver.Abstractions.Entities;
using GASolver.Abstractions.Options;
using GASolver.Implementation.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using GASolver.Implementation.Options;
using GASolver.Utils;

namespace GASolver
{
    public class Solver
    {
       private readonly SolverOptions _solverOptions = new SolverOptions();

        public void Configure(Action<ISolverOptions> optionsBuilder)
        {
            optionsBuilder(_solverOptions);
        }

        public IIndividual Solve()
        {
            ISolverEngine engine = new SolverEngine();

            engine.Init(_solverOptions);

            using (ProgressBar progressBar = new ProgressBar())
            {
                for (int i = 0; i < _solverOptions.GenerationsCount; i++)
                {
                    engine.Select();
                    engine.Populate();
                    engine.Mutate();

                    var progressPercent = (float) i / (float) _solverOptions.GenerationsCount;
                    progressBar.Report(progressPercent);
                }
            }

            return engine.GetBestIndividual();
        }
    }
}
