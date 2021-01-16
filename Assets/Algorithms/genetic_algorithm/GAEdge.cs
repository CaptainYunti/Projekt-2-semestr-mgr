namespace Algorithms {
  class GAEdge : IEdge {
    public int distance { get; set; }

    public GAEdge(int distance) {
      this.distance = distance;
    }
  }
}
