using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Algorithms {
  class AlgorithmsManager {
    private string graphPath;

    private int iterationsNumber;
    private int repetitionsNumber;

    public int lastResult { get; set; }

    private List<string> graphsName;
    private List<long> averageTimes;
    private List<int> averagePaths;
    private List<int> shortestPaths;
    private List<int> longestPaths;

    public AlgorithmsManager() {
      graphsName = new List<string>();
      averageTimes = new List<long>();
      averagePaths = new List<int>();
      shortestPaths = new List<int>();
      longestPaths = new List<int>();
    }

    public void LoadGraph(string graphPath) {
      this.graphPath = graphPath; 
    }

    public void SetIterationsNumber(int iterationsNumber) {
      this.iterationsNumber = iterationsNumber;
    }

    public void SetRepetitionsNumber(int repetitionsNumber) {
      this.repetitionsNumber = repetitionsNumber;
    }

    public void Start(Algorithm algorithm) {
      IAlgorithm selectedAlgorithm;
      
      switch (algorithm) {
        case Algorithm.SAA:
          selectedAlgorithm =  new SimulatedAnnealingAlgorithm();
          break;
        case Algorithm.ACO:
          selectedAlgorithm =  new AntColonyOptimization();
          break;
        case Algorithm.GA:
          selectedAlgorithm =  new GeneticAlgorithm();
          break;
        case Algorithm.NNA:
          selectedAlgorithm =  new NearestNeigbourAlgorithm();
          break;
        default:
          throw new System.Exception("Algorithm doesn't exists!");
      }

      selectedAlgorithm.LoadGraph(graphPath);
      selectedAlgorithm.Start(iterationsNumber);

      lastResult = selectedAlgorithm.GetBestSolution();
    }

    public void DoMeasurement(Algorithm algorithm,
                              List<(string, string)> graphs) {
      averageTimes.Clear();
      averagePaths.Clear();
      shortestPaths.Clear();
      longestPaths.Clear();

      for (int i = 0; i < graphs.Count; ++i) {
        Stopwatch stopwatch = new Stopwatch();

        long averageTime = 0;
        int shortestPath = int.MaxValue;
        int longestPath = int.MinValue;
        int averagePath = 0;

        LoadGraph(graphs[i].Item2);

        for (int j = 0; j < repetitionsNumber; ++j) {
          stopwatch.Start();

          Start(algorithm);

          averageTime += stopwatch.ElapsedMilliseconds;
          averagePath += lastResult;
          shortestPath = 
            (lastResult < shortestPath) ? lastResult : shortestPath;
          longestPath = 
            (lastResult > longestPath) ? lastResult : longestPath;
        }

        averageTime /= repetitionsNumber;
        averagePath /= repetitionsNumber;

        graphsName.Add(graphs[i].Item1);
        averageTimes.Add(averageTime);
        averagePaths.Add(averagePath);
        shortestPaths.Add(shortestPath);
        longestPaths.Add(longestPath);
      }
    }

    public void SaveMeasurementToFile(string filePath) {
      string measurement = "";

      measurement += "graph average-time average-path " +
                     "shortest-path longest-path\n";

      for (int i = 0; i < averagePaths.Count; ++i) {
        measurement += graphsName[i] + " ";
        measurement += averageTimes[i] + " ";
        measurement += averagePaths[i] + " ";
        measurement += shortestPaths[i] + " ";
        measurement += longestPaths[i] + "\n";
      }
      
      File.WriteAllText(filePath, measurement);
    }
  }
}
