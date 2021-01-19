using System;
using System.Collections.Generic;

namespace Algorithms {
  class SimulatedAnnealingAlgorithm : IAlgorithm {
    private Graph graph; 

    private List<int> lastPermutation;

    private static Random random = new Random();

    public void Start() {
      lastPermutation = GetRandomPermutation();

      graph.cities = new List<int>(lastPermutation);

      for (int i = 0; i < SAParams.iterationsNumber; ++i) {
        DoInternalIterations();
        SAParams.CalculateNewTemperature();
      }

      graph.PrintShortestPath();
    }

    public void LoadGraph(string path) {
      graph = new Graph();

      graph.Load(path, Algorithm.SAA);
    }

    private void DoInternalIterations() {
      for (int j = 0; j < SAParams.L; ++j) {
        List<int> adjacentPermutation = 
          GetRandomAdjacentPermutation(lastPermutation);
        // difference in costs of solutions: new and previous
        int delta = 
          graph.CalculatePathDistance(adjacentPermutation) - 
          graph.CalculatePathDistance(lastPermutation);

        if (ShouldChangeSolution(delta)) {
          lastPermutation = adjacentPermutation;

          graph.cities = new List<int>(lastPermutation);
        }
      }
    }

    private bool ShouldChangeSolution(int delta) {
      if (delta < 0) {
        return true;
      } else {
        double x = random.NextDouble();

        // choose the worse solution with some probability
        if (x < Math.Exp(-delta / SAParams.T)) {
          return true;
        }
      }

      return false;
    }

    private List<int> GetRandomPermutation() {
      List<int> permutation = new List<int>();

      for (int i = 0; i < graph.size; ++i) {
        int city;

        do {
          city = random.Next(graph.size);
        } while(permutation.Contains(city));

        permutation.Add(city);
      }

      return permutation;
    }

    /* 
      Get random adjacent permutation for the permutation
      given in the parameter using k-swap algorithm.
    */
    private List<int> GetRandomAdjacentPermutation(List<int> permutation) {
      List<int> adjacentPermutation = new List<int>(permutation);
      int k = 4;
      List<int> cityIndexes = new List<int>();

      // remove k random cities from graph
      for (int i = 0; i < k; ++i) {
        int city = random.Next(adjacentPermutation.Count);

        cityIndexes.Add(adjacentPermutation[city]);
        adjacentPermutation.RemoveAt(city);
      }

      // insert the cities back in a different random order
      for (int i = 0; i < k; ++i) {
        int newPosition = random.Next(adjacentPermutation.Count);

        adjacentPermutation.Insert(newPosition, cityIndexes[i]);
      }

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
        /*
                private SimulatedAnnealingAlgorithm() { }

                private static SimulatedAnnealingAlgorithm _instance;

                public static SimulatedAnnealingAlgorithm GetInstance()
                {
                    if (_instance == null)
                    {
                        _instance = new SimulatedAnnealingAlgorithm();
                    }
                    return _instance;
                }
        */
    }
}
