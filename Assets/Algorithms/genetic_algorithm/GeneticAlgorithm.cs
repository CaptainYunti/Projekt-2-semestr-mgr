using System;
using System.Collections.Generic;

namespace Algorithms {
  public class GeneticAlgorithm : IAlgorithm {
    private Graph graph;
    private List<List<int>> permutations;
    private static Random random = new Random();

    public void Start() {
      GenerateStartPermutations();

      for (int i = 0; i < GAParams.iterationsNumber; ++i) {
        CrossPermutation();
        MutatePermutation();
        SortPermutations();
        SelectBestPermutations();

        if (graph.CalculatePathDistance(graph.cities) > 
            graph.CalculatePathDistance(permutations[0])) {
          graph.cities = permutations[0];
        }
      }

      graph.PrintShortestPath();
    }

    public void LoadGraph(string path) {
      graph = new Graph();

      graph.Load(path, Algorithm.GA);
    }

    private void CrossPermutation() {
      for (int i = 0; i < permutations.Count; ++i) {
        if (random.NextDouble() > GAParams.pk) {
          continue;
        }

        int permutationNum1 = random.Next(permutations.Count);
        int permutationNum2;

        List<int> newPermutation = new List<int>();

        while ((permutationNum2 = random.Next(permutations.Count)) ==
               permutationNum1);

        for (int j = 0; j < permutations[permutationNum1].Count / 2; ++j) {
          newPermutation.Add(permutations[permutationNum1][j]);
        }

        for (int j = 0; j < permutations[permutationNum2].Count; ++j) {
          if (!newPermutation.Contains(permutations[permutationNum2][j])) {
            newPermutation.Add(permutations[permutationNum2][j]);
          }
        }

        permutations.Add(newPermutation);
      }
    }

    private void MutatePermutation() {
      List<int> mutatedPermutations = new List<int>();

      for (int i = 0; i < permutations.Count; ++i) {
        if (random.NextDouble() > GAParams.pm) {
          continue;
        }

        int permutationNum;

        do {
          permutationNum = random.Next(permutations.Count);
        } while (mutatedPermutations.Contains(permutationNum));

        mutatedPermutations.Add(permutationNum);

        int city1ToSwap = random.Next(permutations[permutationNum].Count);
        int city2ToSwap;

        while ((city2ToSwap = random.Next(permutations[permutationNum].Count)) ==
               city1ToSwap);

        int temp = permutations[permutationNum][city1ToSwap];

        permutations[permutationNum][city1ToSwap] = 
          permutations[permutationNum][city2ToSwap];
        permutations[permutationNum][city2ToSwap] = temp;
      }
    }

    private void SortPermutations() {
      List<(int, int)> ranking = new List<(int, int)>();

      for (int i = 0; i < permutations.Count; ++i) {
        ranking.Add((i, graph.CalculatePathDistance(permutations[i])));
      }

      ranking.Sort((permutation1, permutation2) => 
        permutation1.Item2.CompareTo(permutation2.Item2));

      List<List<int>> sortedPermutations = new List<List<int>>();

      for (int i = 0; i < permutations.Count; ++i) {
        sortedPermutations.Add(permutations[ranking[i].Item1]);
      }

      permutations = sortedPermutations;
    }

    private void SelectBestPermutations() {
      while (permutations.Count > GAParams.populationSize) {
        permutations.RemoveAt(permutations.Count - 1);
      }
    }

    private void GenerateStartPermutations() {
      permutations = new List<List<int>>();

      for (int i = 0; i < GAParams.populationSize; ++i) {
        permutations.Add(new List<int>());

        permutations[i].Add(0);

        while (permutations[i].Count != graph.size) {
          int city;

          do {
            city = random.Next(graph.size);
          } while(permutations[i].Contains(city));

          permutations[i].Add(city);
        }
      }
    }

        public List<int> GetCities()
        {
            return graph.cities;
        }
    }
}
