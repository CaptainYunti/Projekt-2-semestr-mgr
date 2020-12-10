namespace Algorithms {
  public static class ACOParams {
    // pheromones evaporation factor
    public static double rho => 0.5;
    // parameter regulating the influence of pheromone
    // concentration on the selection of the next city
    public static int alpha => 100;
    // parameter regulating the influence of the local value of the criterion
    // function on the selection of the next city
    public static int beta => 20;
    // initial concentration of pheromones
    public static double tau0 => 0.5;
    // number of pheromones spread over 1 edge
    public static double q => 0.7;

    public static int iterationsNumber => 500;
  }
}
