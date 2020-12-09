using System.Collections.Generic;

namespace Algorithms {
  class Graph {
    public int size { get; set; }
    public DistanceEdge[,] distanceEdges { get; set; }

    public List<int> cities { get; set; }

    public Graph() {
      this.cities = new List<int>();
    }

    public void Print() {
      for (int i = 0; i < size; ++i) {
        for (int j = 0; j < size; ++j) {
          System.Console.Write(distanceEdges[i, j].distance + " ");
        }
        System.Console.WriteLine();
      }
    }

    public void PrintShortestPath() {

      System.Console.WriteLine("Visited cities:");
      System.Console.Write(cities[0]);

      for (int i = 1; i < cities.Count; ++i) {
        System.Console.Write(" -> " + cities[i]);
      }

      int pathDistance = CalculatePathDistance(cities);

      System.Console.WriteLine("\nPath length: " + pathDistance + "\n");
    }

    public DistanceEdge DistanceEdge(int i, int j) {
      return distanceEdges[i, j];
    }

    public int CalculatePathDistance(List<int> permutation) {
      if (permutation.Count == 0) {
        return int.MaxValue;
      }

      int distance = 0;

      for (int i = 0; i < size - 1; ++i) {
        distance += DistanceEdge(permutation[i], permutation[i + 1]).distance;
      }

      return distance;
    }
  }
}
