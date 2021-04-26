using System;
using System.Collections.Generic;
using System.Linq;
using GASolver.Abstractions.Entities;

namespace GASolver.Implementation.Entities
{
    internal class Individual : List<bool>, IIndividual
    {
        public Individual() {}
        public Individual(IEnumerable<bool> collection) : base(collection) {}

        public void Mutate()
        {
            Random rnd = new();

            int index = rnd.Next(this.Count());

            this[index] = !this[index];
        }

        public static Individual GenerateRandom(int genomeLenght)
        {
            Random rnd = new();
            Individual result = new();

            result = new Individual(Enumerable.Range(0, genomeLenght).Select(_ => rnd.NextBool()));

            return result;
        }

        public static void Cross(ref Individual a, ref Individual b)
        {
            Random rnd = new();

            if (a.Count() != b.Count())
            {
                throw new InvalidOperationException("Cannot do crosover with genomes with different lenght");
            }

            for (int i = 0; i < a.Count(); i++)
            {
                bool mask = rnd.NextBool();
                bool swapMask = (a[i] ^ b[i]) & mask;;

                a[i] ^= swapMask;
                b[i] ^= swapMask;
            }
        }
    }

    static class RandomExtensions
    {
        public static bool NextBool(this Random r) => r.Next() % 2 == 0;
    }
}
