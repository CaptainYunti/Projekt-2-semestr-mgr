namespace Algorithms
{
  class Program
  {
    static void Main(string[] args)
    {
      SimulatedAnnealingAlgorithm saa = new SimulatedAnnealingAlgorithm();
      AntColonyOptimization aco = new AntColonyOptimization();
      NearestNeigbourAlgorithm nna = new NearestNeigbourAlgorithm();

      saa.LoadGraph("./graphs/tsp_15.txt");
      aco.LoadGraph("./graphs/tsp_15.txt");
      nna.LoadGraph("./graphs/tsp_15.txt");

      saa.Start();
      aco.Start();
      nna.Start();
    }
  }
}
