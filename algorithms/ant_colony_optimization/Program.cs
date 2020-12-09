namespace AntColonyOptimization
{
    class Program
    {
        static void Main(string[] args)
        {
            AntColonyOptimization aco = new AntColonyOptimization();

            aco.LoadGraph("./graphs/tsp_15.txt");
            aco.Start();
        }
    }
}
