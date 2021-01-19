using System;
using System.Diagnostics;

namespace Algorithms {
  class NearestNeigbourAlgorithm : IAlgorithm {
    private Graph graph;

    private static Random random = new Random();

    public void LoadGraph(string path) {
      graph = new Graph();

      graph.Load(path, Algorithm.NNA);
    }

    public void Start(int iterationsNumber) {
      graph.cities.Add(random.Next(graph.size));

      for (int j = 0; j < graph.size - 1; ++j) {
        int city = graph.cities[j];
        int nearestCity = -1;
        int shortestPath = int.MaxValue;

        for (int currentCity = 0; currentCity < graph.size; ++currentCity) {
          if (!graph.cities.Contains(currentCity)) {
            if (shortestPath > graph.Edge(city, currentCity).distance) {
              shortestPath = graph.Edge(city, currentCity).distance;
              nearestCity = currentCity;
            }
          }
        }

        graph.cities.Add(nearestCity);
      }
    }

    public int GetBestSolution() {
      return graph.GetShortestPath();
    }
  }
}
