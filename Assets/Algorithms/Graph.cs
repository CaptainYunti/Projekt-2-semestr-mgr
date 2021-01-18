using System;
using System.Collections.Generic;
using UnityEngine;

namespace Algorithms {
  class Graph{
    public int size { get; set; }
    public IEdge[,] edges { get; set; }
    public List<int> cities { get; set; }

    public Graph() {
      this.cities = new List<int>();
    }

        public void Load(string path, Algorithm algorithm)
        {

            System.IO.StreamReader file = new System.IO.StreamReader(path);

            size = Convert.ToInt32(file.ReadLine());
            cities = new List<int>();

            switch (algorithm)
            {
                case Algorithm.ACO:
                    edges = new ACOEdge[size, size];
                    break;
                case Algorithm.SAA:
                    edges = new SAEdge[size, size];
                    break;
                case Algorithm.GA:
                    edges = new GAEdge[size, size];
                    break;
                case Algorithm.NNA:
                    edges = new NNEdge[size, size];
                    break;
                default:
                    break;
            }

            int rowCounter = 0;
            string line;

            while ((line = file.ReadLine()) != null)
            {
                string[] row = line.Split();

                for (int i = 0; i < row.Length; ++i)
                {
                    if (row[i] == "")
                    {
                        continue;
                    }
                    switch (algorithm)
                    {
                        case Algorithm.ACO:
                            edges[rowCounter, i] = new ACOEdge(Convert.ToInt32(row[i]));
                            break;
                        case Algorithm.SAA:
                            edges[rowCounter, i] = new SAEdge(Convert.ToInt32(row[i]));
                            break;
                        case Algorithm.GA:
                            edges[rowCounter, i] = new GAEdge(Convert.ToInt32(row[i]));
                            break;
                        case Algorithm.NNA:
                            edges[rowCounter, i] = new NNEdge(Convert.ToInt32(row[i]));
                            break;
                        default:
                            throw new ArgumentException(message: "invalid enum value",
                                                        paramName: nameof(algorithm));
                    }
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
