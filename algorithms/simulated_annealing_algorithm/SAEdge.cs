namespace Algorithms {
  class SAEdge : IEdge {
    public int distance { get; set; }

    public SAEdge(int distance) {
      this.distance = distance;
    }
  }
}
