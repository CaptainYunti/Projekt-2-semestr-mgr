using System.Collections.Generic;
using System.IO;

namespace Algorithms
{
  class Program
  {
    private static void DoMeasurement(Algorithm algorithm,
                                      int repetitionsNumber = 10,
                                      int iterationsNumber = 1000) {
      AlgorithmsManager algorithmsManager = new AlgorithmsManager();

      algorithmsManager.SetRepetitionsNumber(repetitionsNumber);
      algorithmsManager.SetIterationsNumber(iterationsNumber);

      List<(string, string)> graphs = new List<(string, string)>();

      foreach (string dirFile in Directory.GetDirectories("./graphs")) {
        foreach (string fileName in Directory.GetFiles(dirFile)) {
            graphs.Add((fileName.Split('\\')[2]
                                .Split(".")[0]
                                .Replace("tsp_", ""),
                        fileName)); 
        }
      }

      algorithmsManager.DoMeasurement(algorithm, graphs);
      algorithmsManager.SaveMeasurementToFile("./results/" +
                                              algorithm + 
                                              "-results.txt");
    }

    static void Main(string[] args)
    {  
      DoMeasurement(Algorithm.SAA, 10, 1000);
      DoMeasurement(Algorithm.ACO, 10, 1);
      DoMeasurement(Algorithm.GA, 10, 2500);
      DoMeasurement(Algorithm.NNA);
    }
  }
}
