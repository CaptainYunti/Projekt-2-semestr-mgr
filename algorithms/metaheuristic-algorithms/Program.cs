namespace MetaheuristicAlgorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            SimulatedAnnealingAlgorithm saa = new SimulatedAnnealingAlgorithm();
            AntColonyOptimization aco = new AntColonyOptimization();

            saa.LoadGraph("./graphs/tsp_15.txt");
            aco.LoadGraph("./graphs/tsp_15.txt");

            saa.Start();
            aco.Start();
        }
    }
}
