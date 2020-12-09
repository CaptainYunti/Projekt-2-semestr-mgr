using System.Collections.Generic;

namespace MetaheuristicAlgorithms {
  class AntColonyOptimization {
    private ACOGraph graph; 
    private List<Ant> ants;

    private int iterationsNumber = 200;

    public AntColonyOptimization() {
      ants = new List<Ant>();
    }

    public void LoadGraph(string path) {
      graph = new ACOGraph();

      graph.Load(path);

      for (int i = 0; i < graph.size; ++i) {
        ants.Add(new Ant(graph));
      }
    }

    public void Start() {
      for (int i = 0; i < iterationsNumber; ++i) {
        DoIteration();
        UpdatePheromonoesConcentration();
      }

      graph.PrintShortestPath();
    }

    private void DoIteration() {
      foreach (var ant in ants) {
        ant.DoCycle();
      }
    }

    private void UpdatePheromonoesConcentration() {
      for (int i = 0; i < graph.size - 1; ++i) {
        var distance = graph.DistanceEdge(i, i + 1).distance;

        graph.PheromonesEdge(i, i + 1).UpdatePheromonesConcentration(graph.rho,
                                                                     distance);
      }
    }
  }
}