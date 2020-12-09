namespace MetaheuristicAlgorithms {
  class SADistanceEdge : IDistanceEdge {
    public int distance { get; set; }

    public SADistanceEdge(int distance) {
      this.distance = distance;
    }
  }
}