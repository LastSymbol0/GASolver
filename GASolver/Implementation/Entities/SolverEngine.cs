using System;
using System.Collections.Generic;
using System.Linq;
using GASolver.Abstractions.Entities;
using GASolver.Abstractions.Options;

namespace GASolver.Implementation.Entities
{
    internal class SolverEngine : ISolverEngine
    {
        private Individual[] _parentsPopulation;
        private Individual[] _offspringsPopulation;
        private IFitnessFunction _fitnessFunction;

        private int _genomeLenght;
        private int _populationSize;
        private double _mutationPercent;
        private readonly Random _random = new();

        public void Init(ISolverOptions options)
        {
            _fitnessFunction = options?.FitnessFunction ??  throw new ArgumentNullException(nameof(options));
            _populationSize = options?.PopulationSize ??  throw new ArgumentNullException(nameof(options));
            _mutationPercent = options?.MutationsPercent ??  throw new ArgumentNullException(nameof(options));
            _genomeLenght = options?.GenomeLenght ??  throw new ArgumentNullException(nameof(options));

            InitFirstGeneration();
        }

        private void InitFirstGeneration()
        {
            var firstPopulation = new Individual[_populationSize];

            for (int i = 0; i < _populationSize; i++)
            {
                firstPopulation[i] = Individual.GenerateRandom(_genomeLenght);
            }

            _parentsPopulation = firstPopulation;
            _offspringsPopulation = new Individual[_populationSize];
        }

        public void Mutate()
        {
            foreach (var individual in _offspringsPopulation)
            {
                if (_random.NextDouble() <= _mutationPercent)
                {
                    individual.Mutate();
                }
            }
        }

        public void Populate()
        {
            for (int i = 0; i < _populationSize / 2; i++)
            {
                int index1 = i << 1;
                int index2 = index1 | 1;

                Individual.Cross(
                    ref _offspringsPopulation[index1],
                    ref _offspringsPopulation[index2]);
            }
        }

        public void Select()
        {
            for (int i = 0; i < _populationSize; i++)
            {
                int index1 = _random.Next(_populationSize);
                int index2 = _random.Next(_populationSize);

                long fitness1 = _fitnessFunction.Run(_parentsPopulation.ElementAt(index1));
                long fitness2 = _fitnessFunction.Run(_parentsPopulation.ElementAt(index2));

                _offspringsPopulation[i] = fitness1 > fitness2
                    ? _parentsPopulation.ElementAt(index1)
                    : _parentsPopulation.ElementAt(index2);
            }
        }


        public Individual GetBestIndividual() => _parentsPopulation.OrderByDescending(x => _fitnessFunction.Run(x)).FirstOrDefault();
    }
}
