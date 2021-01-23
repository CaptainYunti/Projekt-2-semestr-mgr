using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms
{
    class SimulatedAnnealingAlgorithm : IAlgorithm
    {
        private Graph graph;
        private List<int> lastPermutation;

        private static Random random = new Random();

        public void LoadGraph(string path)
        {
            graph = new Graph();

            graph.Load(path, Algorithm.SAA);
        }

        public void Start(int iterationsNumber)
        {
            lastPermutation = GetRandomPermutation();

            graph.cities = new List<int>(lastPermutation);

            for (int j = 0; j < iterationsNumber; ++j)
            {
                DoInternalIterations();
                SAParams.CalculateNewTemperature();
            }
        }

        public int GetBestSolution()
        {
            return graph.GetShortestPath();
        }

        private void DoInternalIterations()
        {
            for (int j = 0; j < SAParams.L; ++j)
            {
                List<int> adjacentPermutation =
                  GetRandomAdjacentPermutation(lastPermutation);

                if (graph.CalculatePathDistance(adjacentPermutation) <
                    graph.CalculatePathDistance(graph.cities))
                {
                    graph.cities = new List<int>(adjacentPermutation);
                }

                // difference in costs of solutions: new and previous
                int delta =
                  graph.CalculatePathDistance(adjacentPermutation) -
                  graph.CalculatePathDistance(lastPermutation);

                if (ShouldChangeSolution(delta))
                {
                    lastPermutation = adjacentPermutation;
                }
            }
        }

        private bool ShouldChangeSolution(int delta)
        {
            if (delta < 0)
            {
                return true;
            }
            else
            {
                double x = random.NextDouble();

                // choose the worse solution with some probability
                if (x < Math.Exp(-delta / SAParams.T))
                {
                    return true;
                }
            }

            return false;
        }

        private List<int> GetRandomPermutation()
        {
            List<int> permutation = new List<int>();

            for (int i = 0; i < graph.size; ++i)
            {
                permutation.Add(i);
            }

            permutation = permutation.OrderBy(x => Guid.NewGuid()).ToList();

            return permutation;
        }

        /* 
          Get random adjacent permutation for the permutation
          given in the parameter using k-swap (with k = 1) algorithm.
        */
        private List<int> GetRandomAdjacentPermutation(List<int> permutation)
        {
            List<int> adjacentPermutation = new List<int>(permutation);

            int position = random.Next(adjacentPermutation.Count);
            int newPosition = random.Next(adjacentPermutation.Count);

            int city = adjacentPermutation[position];

            adjacentPermutation.RemoveAt(position);
            adjacentPermutation.Insert(newPosition, city);

            return adjacentPermutation;
        }

        public List<int> GetCities()
        {
            return graph.cities;
        }

        public int ShortestPath()
        {
            return graph.CalculatePathDistance(graph.cities);
        }
    }
}

