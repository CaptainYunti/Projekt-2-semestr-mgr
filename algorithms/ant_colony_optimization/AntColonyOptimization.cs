using System.Collections.Generic;

namespace AntColonyOptimization {
  class AntColonyOptimization {
    private Graph graph; 
    private List<Ant> ants;

    private int iterationsNumber = 200;

    public AntColonyOptimization() {
      ants = new List<Ant>();
    }

    public void LoadGraph(string path) {
      graph = new Graph();

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
        graph.Edge(i, i + 1).UpdatePheromonesConcentration(graph.rho);
      }
    }
  }
}
