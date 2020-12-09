namespace AntColonyOptimization {
  class Edge {
    public double pheromonesConcentration { get; set; }
    public int distance { get; set; }

    public Edge(double pheromonesConcentration, int distance) {
      this.pheromonesConcentration = pheromonesConcentration;
      this.distance = distance;
    }

    public void UpdatePheromonesConcentration(double rho) {
      double tau = pheromonesConcentration / distance;

      pheromonesConcentration = rho * pheromonesConcentration + tau;
    }
  }
}
