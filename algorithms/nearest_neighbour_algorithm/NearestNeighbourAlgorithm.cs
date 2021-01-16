using System;

namespace Algorithms {
  class NearestNeigbourAlgorithm : IAlgorithm {
    private Graph graph;

    public void Start() {
      Random random = new Random();

      graph.cities.Add(random.Next(graph.size));

      for (int i = 0; i < graph.size - 1; ++i) {
        int city = graph.cities[i];
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

      graph.PrintShortestPath();
    }

    public void LoadGraph(string path) {
      graph = new Graph();

      graph.Load(path, Algorithm.NNA);
    }
  }
}
