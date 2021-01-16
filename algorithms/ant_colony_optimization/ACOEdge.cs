namespace Algorithms {
  class ACOEdge : IEdge {
    public int distance { get; set; }
    public double pheromonesConcentration { get; set; }

    public ACOEdge(int distance) {
      this.pheromonesConcentration = ACOParams.tau0;
      this.distance = distance;
    }

    public void UpdatePheromonesConcentration(double rho, int distance) {
      double tau = pheromonesConcentration / distance;

      pheromonesConcentration = rho * pheromonesConcentration + tau;
    }
  }
}
