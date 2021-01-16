namespace Algorithms {
  class NNEdge : IEdge {
    public int distance { get; set; }

    public NNEdge(int distance) {
      this.distance = distance;
    }
  }
}
