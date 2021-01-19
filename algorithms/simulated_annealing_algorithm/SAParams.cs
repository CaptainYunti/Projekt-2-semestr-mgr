namespace Algorithms {
  public static class SAParams {
    // temperature value
    public static double T = 10000.0;
    // epoch length (number of internal iterations)
    public static int L => 500;
    // temperature change factor
    public static double r => 0.95;

    public static void CalculateNewTemperature() {
      T = T * r;
    }
  }
}
