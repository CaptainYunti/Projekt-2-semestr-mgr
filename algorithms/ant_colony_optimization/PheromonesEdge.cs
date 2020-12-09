namespace Algorithms {
  class PheromonesEdge {
    
    public double pheromonesConcentration { get; set; }

    public PheromonesEdge(double pheromonesConcentration) {
      this.pheromonesConcentration = pheromonesConcentration;
    }

    public void UpdatePheromonesConcentration(double rho, int distance) {
      double tau = pheromonesConcentration / distance;

      pheromonesConcentration = rho * pheromonesConcentration + tau;
    }
  }
}
