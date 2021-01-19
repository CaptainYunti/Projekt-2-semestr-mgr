namespace Algorithms {
  interface IAlgorithm {
    void LoadGraph(string path);
    void Start(int iterationsNumber);
    int GetBestSolution();
  }
}
