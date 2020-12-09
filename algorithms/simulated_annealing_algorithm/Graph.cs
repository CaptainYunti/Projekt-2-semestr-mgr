using System;
using System.Collections.Generic;

namespace SimulatedAnnealingAlgorithm {
  class Graph {
    public int size { get; set; }
    public Edge[,] graph { get; set; }

    public List<int> cities { get; set; }

    public void Load(string path) {
      System.IO.StreamReader file = new System.IO.StreamReader(path);

      size = Convert.ToInt32(file.ReadLine());
      graph = new Edge[size, size];
      cities = new List<int>();

      int rowCounter = 0;
      string line;

      while ((line = file.ReadLine()) != null) {
        string[] row = line.Split();

        for (int i = 0; i < row.Length; ++i) {
          graph[rowCounter, i] = new Edge(Convert.ToInt32(row[i]));
        }

        cities.Add(rowCounter);

        ++rowCounter;
      }
    }

    public void Print() {
      for (int i = 0; i < size; ++i) {
        for (int j = 0; j < size; ++j) {
          System.Console.Write(graph[i, j].distance + " ");
        }
        System.Console.WriteLine();
      }
    }

    public void PrintShortestPath() {

      System.Console.WriteLine("Visited cities:");
      System.Console.Write(cities[0]);

      for (int i = 1; i < size; ++i) {
        System.Console.Write(" -> " + cities[i]);
      }

      int pathDistance = CalculatePathDistance(cities);

      System.Console.WriteLine(" -> " + cities[0]);
      System.Console.WriteLine("Path length: " + pathDistance);
    }

    public Edge Edge(int i, int j) {
      return graph[i, j];
    }

    public int CalculatePathDistance(List<int> permutation) {
      int distance = 0;

      for (int i = 0; i < size - 1; ++i) {
        distance += Edge(permutation[i], permutation[i + 1]).distance;
      }

      distance += Edge(permutation[permutation.Count - 1], permutation[0]).distance;

      return distance;
    }
  }
}
