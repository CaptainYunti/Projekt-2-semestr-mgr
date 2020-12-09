namespace SimulatedAnnealingAlgorithm
{
  class Program
  {
    static void Main(string[] args)
    {
      SimulatedAnnealingAlgorithm saa = new SimulatedAnnealingAlgorithm();

      saa.LoadGraph("./graphs/tsp_15.txt");
      saa.Start();
    }
  }
}
