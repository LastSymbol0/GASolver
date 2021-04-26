using System;

namespace TSPGeneticAlgorithm.Utils
{
    public static class MatrixGenerator
    {
        public static int[,] GeneratePointsMatrix(int citiesCount, int PathMaxLenght, int PathMinLenght)
        {
            int[,] result = new int[citiesCount, citiesCount];
            Random rnd = new Random();

            for (int i = 0; i < citiesCount; i++)
            {
                for (int j = 0; j < citiesCount; j++)
                {
                    if (i == j)
                    {
                        result[i, j] = 0;
                    }
                    else if (i > j)
                    {
                        result[i, j] = result[j, i];
                    }
                    else
                    {
                        result[i, j] = rnd.Next(PathMinLenght, PathMaxLenght);
                    }

                    //Console.Write($"{result[i, j]} ");
                }
                //Console.WriteLine("");
            }

            return result;
        }
    }
}
