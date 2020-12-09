using System;
using System.Collections.Generic;

namespace MetaheuristicAlgorithms {
  class SimulatedAnnealingAlgorithm {
    private SAGraph graph; 

    // temperature value
    public double T = 10000.0;
    // epoch length (number of internal iterations)
    int L = 500;
    // temperature change factor
    double r = 0.95;

    private int iterationsNumber = 200;

    private List<int> lastPermutation;

    private static Random random = new Random();

    public void Start() {
      lastPermutation = GetRandomPermutation();

      graph.cities = new List<int>(lastPermutation);

      for (int i = 0; i < iterationsNumber; ++i) {
        DoInternalIterations();
        CalculateNewTemperature();
      }

      graph.PrintShortestPath();
    }

    public void LoadGraph(string path) {
      graph = new SAGraph();

      graph.Load(path);
    }

    private void DoInternalIterations() {
      for (int j = 0; j < L; ++j) {
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
        if (x < Math.Exp(-delta / T)) {
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

    private void CalculateNewTemperature() {
      T = T * r;
    }
  }
}
