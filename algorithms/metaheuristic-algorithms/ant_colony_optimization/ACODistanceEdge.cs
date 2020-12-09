namespace MetaheuristicAlgorithms {
  class ACODistanceEdge : IDistanceEdge {
    
    public int distance { get; set; }

    public ACODistanceEdge( int distance) {
      this.distance = distance;
    }
  }
}
