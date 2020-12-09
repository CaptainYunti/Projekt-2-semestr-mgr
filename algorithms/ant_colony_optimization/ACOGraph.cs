using System;

namespace Algorithms {
  class ACOGraph : Graph {
    // pheromones evaporation factor
    public double rho { get; } = 0.5;
    // parameter regulating the influence of pheromone
    // concentration on the selection of the next city
    public int alpha { get; } = 100;
    // parameter regulating the influence of the local value of the criterion
    // function on the selection of the next city
    public int beta { get; } = 20;
    // initial concentration of pheromones
    public double tau0 { get; } = 0.5;
    // number of pheromones spread over 1 edge
    public double q { get; } = 0.7;

    public PheromonesEdge[,] pheromonesEdges { get; set; }

    public void Load(string path) {
      System.IO.StreamReader file = new System.IO.StreamReader(path);

      size = Convert.ToInt32(file.ReadLine());
      distanceEdges = new DistanceEdge[size, size];
      pheromonesEdges = new PheromonesEdge[size, size];

      int rowCounter = 0;
      string line;

      while ((line = file.ReadLine()) != null) {
        string[] row = line.Split();

        for (int i = 0; i < row.Length; ++i) {
          distanceEdges[rowCounter, i] = 
            new DistanceEdge(Convert.ToInt32(row[i]));
          pheromonesEdges[rowCounter, i] = 
            new PheromonesEdge(tau0);
        }

        ++rowCounter;
      }
    }    
    public PheromonesEdge PheromonesEdge(int i, int j) {
      return pheromonesEdges[i, j];
    }
  }
}
