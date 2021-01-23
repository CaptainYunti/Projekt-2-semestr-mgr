using System.Collections.Generic;
using System.Diagnostics;

namespace Algorithms
{
    class AntColonyOptimization : IAlgorithm
    {
        private Graph graph;
        private List<Ant> ants;

        public AntColonyOptimization()
        {
            ants = new List<Ant>();
        }

        public void LoadGraph(string path)
        {
            graph = new Graph();

            graph.Load(path, Algorithm.ACO);

            for (int i = 0; i < graph.size; ++i)
            {
                ants.Add(new Ant(graph));
            }
        }

        public void Start(int iterationsNumber)
        {
            for (int i = 0; i < iterationsNumber; ++i)
            {
                DoIteration();
                UpdatePheromonoesConcentration();
            }

            for (int i = 0; i < graph.size - 1; ++i)
            {
                ((ACOEdge)graph.Edge(i, i + 1)).resetPheromonesConcentration();
            }
        }

        public int GetBestSolution()
        {
            return graph.GetShortestPath();
        }

        private void DoIteration()
        {
            foreach (var ant in ants)
            {
                ant.DoCycle();
            }
        }

        private void UpdatePheromonoesConcentration()
        {
            for (int i = 0; i < graph.size - 1; ++i)
            {
                var distance = graph.Edge(i, i + 1).distance;

                ((ACOEdge)graph.Edge(i, i + 1))
                                .UpdatePheromonesConcentration(ACOParams.rho,
                                                               distance);
            }
        }

        public List<int> GetCities()
        {
            return graph.cities;
        }

        public int ShortestPath()
        {
            return graph.CalculatePathDistance(graph.cities);
        }
    }
}
