using System;

namespace Algorithms {
  class NNGraph : Graph {
    public void Load(string path) {
      System.IO.StreamReader file = new System.IO.StreamReader(path);

      size = Convert.ToInt32(file.ReadLine());
      distanceEdges = new DistanceEdge[size, size];

      int rowCounter = 0;
      string line;

      while ((line = file.ReadLine()) != null) {
        string[] row = line.Split();

        for (int i = 0; i < row.Length; ++i) {
          distanceEdges[rowCounter, i] =
            new DistanceEdge(Convert.ToInt32(row[i]));
        }

        ++rowCounter;
      }
    }
  }
}