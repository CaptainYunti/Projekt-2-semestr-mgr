using System.Collections.Generic;

namespace Algorithms {
  interface IAlgorithm {
    void Start();
    void LoadGraph(string path);

        List<int> GetCities();
  }
}
