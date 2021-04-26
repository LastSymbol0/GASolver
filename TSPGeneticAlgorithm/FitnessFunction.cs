using System;
using System.Linq;
using GASolver.Abstractions.Entities;

namespace TSPGeneticAlgorithm
{
    public class FitnessFunction : IFitnessFunction
    {
        private int[,] PathMatrix => Program.Points;
        private int AveragePathLenght { get; set; }

        public FitnessFunction()
        {
            AveragePathLenght = CalculateAveragePathLenght();
        }

        public long Run(IIndividual genome)
        {
            var path = genomeToPath(genome);
            var lenght = CalculatePathLenght(path);
            return AveragePathLenght - lenght;
        }

        public int GetgenomeLenght()
        {
            const int bitsPerPoint = 4;
            int pointsCount = PathMatrix.GetLength(0);

            return bitsPerPoint * pointsCount;
        }

        /// <summary>
        /// Converts genome (array of bits) to the path through the all cities (where index in array = sequential number of city; value = city "name")
        /// </summary>
        /// <param name="genome"></param>
        /// <example>
        /// Input:              0010.0110.1000.1001.0001
        /// Covert bits to int  ↓
        ///                     2.6.8.9.1
        /// Convert to path     ↓
        ///                     5.1.2.3.4
        ///               Path (5=>1=>2=>3=>4)
        /// </example>
        /// <returns></returns>
        private int[] genomeToPath(IIndividual genome)
        {
            const int bitsPerPoint = 4;
            int pointsCount = PathMatrix.GetLength(0);

            if (genome.Count() != pointsCount * bitsPerPoint)
                throw new ArgumentException();

            return Enumerable.Range(0, pointsCount)
                .Select(i =>
                    new {
                        city = i,
                        number = BitsToInt(genome
                            .Skip(i * bitsPerPoint)
                            .Take(bitsPerPoint)
                            .ToArray())
                        })
                .OrderBy(x => x.number)
                .Select(x => x.city)
                .ToArray();
        }

        private static int BitsToInt(bool[] genome)
        {
            int result = 0;

            //Console.Write("BitsToInt: ");
            for (int i = 0; i < genome.Count(); i++)
            {
                //Console.Write(genome.ElementAt(i) ? 1 : 0);
                result += (genome.ElementAt(i) ? 1 : 0) << i;
            }
            //Console.WriteLine($" => {result}");
            return result;
        }

        private int CalculatePathLenght(int[] path)
        {
            var length = 0;

            for (int i = 1; i < path.Length; i++)
            {
                length += PathMatrix[path[i], path[i - 1]];
            }

            //Console.WriteLine($"For path {{{string.Join(',', path)}}} lenght is {length}");

            return length;
        }

        private int CalculateAveragePathLenght()
        {
            var lenght = 0;

            for (int i = 0; i < PathMatrix.GetLength(0); i++)
                for (int j = 0; j < PathMatrix.GetLength(1); j++)
                    if (PathMatrix[i, j] != 0)
                        lenght += PathMatrix[i, j];

            lenght /= PathMatrix.Length;

            return lenght * PathMatrix.GetLength(0);
        }
    }
}
