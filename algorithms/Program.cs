using System.Collections.Generic;

namespace Algorithms
{
  class Program
  {
    static void Main(string[] args)
    {
      List<IAlgorithm> algorithms = new List<IAlgorithm>();

      algorithms.Add(new SimulatedAnnealingAlgorithm());
      algorithms.Add(new AntColonyOptimization());
      algorithms.Add(new GeneticAlgorithm());
      algorithms.Add(new NearestNeigbourAlgorithm());

      foreach (var algorithm in algorithms) {
        algorithm.LoadGraph("./graphs/tsp_15.txt");
        algorithm.Start();
      }
    }
  }
}
