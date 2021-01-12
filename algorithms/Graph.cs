using System;
using System.Collections.Generic;

namespace Algorithms {
  class Graph {
    public int size { get; set; }
    public IEdge[,] edges { get; set; }
    public List<int> cities { get; set; }

    public Graph() {
      this.cities = new List<int>();
    }

    public void Load(string path, Algorithm algorithm) {
      System.IO.StreamReader file = new System.IO.StreamReader(path);

      size = Convert.ToInt32(file.ReadLine());

      edges = algorithm switch {
        Algorithm.ACO => new ACOEdge[size, size],
        Algorithm.SAA => new SAEdge[size, size],
        Algorithm.GA => new GAEdge[size, size],
        Algorithm.NNA => new NNEdge[size, size],
        _ => throw new ArgumentException(message: "invalid enum value",
                                         paramName: nameof(algorithm)),
      };
      
      int rowCounter = 0;
      string line;

      while ((line = file.ReadLine()) != null) {
        string[] row = line.Split();

        for (int i = 0; i < row.Length; ++i) {
          edges[rowCounter, i] = algorithm switch {
            Algorithm.ACO => new ACOEdge(Convert.ToInt32(row[i])),
            Algorithm.SAA => new SAEdge(Convert.ToInt32(row[i])),
            Algorithm.GA => new GAEdge(Convert.ToInt32(row[i])),
            Algorithm.NNA => new NNEdge(Convert.ToInt32(row[i])),
            _ => throw new ArgumentException(message: "invalid enum value",
                                             paramName: nameof(algorithm)),
          };
        }

        ++rowCounter;
      }
    }

    public void Print() {
      for (int i = 0; i < size; ++i) {
        for (int j = 0; j < size; ++j) {
          System.Console.Write(edges[i, j].distance + " ");
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

    public IEdge Edge(int i, int j) {
      return edges[i, j];
    }

    public int CalculatePathDistance(List<int> permutation) {
      if (permutation.Count == 0) {
        return int.MaxValue;
      }

      int distance = 0;

      for (int i = 0; i < size - 1; ++i) {
        distance += Edge(permutation[i], permutation[i + 1]).distance;
      }

      return distance;
    }
  }
}
