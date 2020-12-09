using System;

namespace AntColonyOptimization {
  class Graph {
    public int size { get; set; }
    public Edge[,] graph { get; set; }

    public int[] cities { get; set; }

    // pheromones evaporation factor
    public double rho { get; } = 0.1;
    // parameter regulating the influence of pheromone
    // concentration on the selection of the next city
    public int alpha { get; } = 15;
    // parameter regulating the influence of the local value of the criterion
    // function on the selection of the next city
    public int beta { get; } = 200;
    // initial concentration of pheromones
    public double tau0 { get; } = 0.2;
    // number of pheromones spread over 1 edge
    public double q { get; } = 0.2;

    public int shortestPath { get; set; } = int.MaxValue;

    public void Load(string path) {
      System.IO.StreamReader file = new System.IO.StreamReader(path);

      size = Convert.ToInt32(file.ReadLine());
      graph = new Edge[size, size];
      cities = new int[size];

      int rowCounter = 0;
      string line;

      while ((line = file.ReadLine()) != null) {
        string[] row = line.Split();

        for (int i = 0; i < row.Length; ++i) {
          graph[rowCounter, i] = new Edge(tau0, Convert.ToInt32(row[i]));
        }

        cities[rowCounter] = rowCounter;

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

      System.Console.WriteLine(" -> " + cities[0]);
      System.Console.WriteLine("Path length: " + shortestPath);
    }

    public Edge Edge(int i, int j) {
      return graph[i, j];
    }
  }
}
