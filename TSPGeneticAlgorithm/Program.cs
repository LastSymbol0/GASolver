using System;
using GASolver;
using TSPGeneticAlgorithm.Utils;

namespace TSPGeneticAlgorithm
{
    class Program
    {
        public static int[,] Points { get; set; } = new int[,]
        {
            //1   2   3   4   5
            {0, 57, 81, 12, 63}, // 1
            {57, 0, 25, 63, 89}, // 2
            {81, 25, 0, 88, 24}, // 3
            {12, 63, 88, 0, 71}, // 4
            {63, 89, 24, 71, 0} // 5
        };


        static void Main(string[] args)
        {
            Points = MatrixGenerator.GeneratePointsMatrix(256, 256, 0);

            Solver solver = new Solver();
            FitnessFunction fitnessFunction = new FitnessFunction();

            solver.Configure(opt =>
            {
                opt.FitnessFunction = fitnessFunction;
                opt.GenerationsCount = 100;
                opt.PopulationSize = 100;
                opt.MutationsPercent = 0.15;

                opt.GenomeLenght = fitnessFunction.GetgenomeLenght();
            });

            var res = solver.Solve();

            Console.WriteLine($"Result ff rate: {fitnessFunction.Run(res)}");
        }
    }
}
