using System;
using System.Collections.Generic;

namespace AntColonyOptimization {
  class Ant {
    private Graph graph;
    private List<int> visitedCities { get; set; }

    int startCity;
    int lastCity;
    int currentPath;

    public Ant(Graph graph) {
      this.graph = graph;
      this.visitedCities = new List<int>();

      ClearCycle();
    }

    public void DoCycle() {
      for (int i = 0; i < graph.size - 1; ++i) {
        NextCity();
      }
    }

    private void NextCity() {
      int city = SelectNextCity();
      int distance = graph.Edge(lastCity, city).distance;

      visitedCities.Add(city);

      graph.Edge(lastCity, city).pheromonesConcentration += graph.q /distance;

      currentPath += distance;
      lastCity = city;

      if (visitedCities.Count != graph.size) {
        return;
      }      

      currentPath += graph.Edge(lastCity, startCity).distance;

      visitedCities.Add(startCity);

      if (graph.shortestPath > currentPath) {
        graph.shortestPath = currentPath;
        graph.cities = visitedCities.ToArray();
      }

      ClearCycle();
    }

    private int SelectNextCity() {
      int city = -1;
      // probibility of choosing of the city;
      double p = 0.0;

      foreach (var currentCity in graph.cities) {
        if (WasCityVisited(currentCity)) {
          continue;
        }

        var probabilityNumerator = CountProbabilityNumerator(currentCity);
        var probabilityDenumerator = CountProbabilityDenumerator();

        double currentP = probabilityNumerator / probabilityDenumerator;

        if (currentP >= p) {
          city = currentCity;
          p = currentP;
        }
      }

      return city;
    }

    private bool WasCityVisited(int city) {
      return visitedCities.Contains(city);
    }

    private double CountProbabilityNumerator(int city)
    {
      double pheromonesConcentration = 
        graph.Edge(lastCity, city).pheromonesConcentration;
      // value of the local criterion function
      double eta = 1.0 / graph.Edge(lastCity, city).distance;

      double probabilityNumerator = 
        Math.Pow(pheromonesConcentration, graph.alpha) * 
        Math.Pow(eta, graph.beta);

      return probabilityNumerator;
    }

    private double CountProbabilityDenumerator() {
      double probabilityDenumerator = 0;

      foreach (var city in graph.cities) {
        if (WasCityVisited(city)) {
          continue;
        }

        var distance = graph.Edge(lastCity, city).distance;

        probabilityDenumerator += 
          Math.Pow(distance, graph.alpha) * 
          Math.Pow(distance, graph.beta);
      }

      return probabilityDenumerator;
    }
  
    private void ClearCycle() {
      Random random = new Random();

      currentPath = 0;
      startCity = random.Next(graph.size);
      lastCity = startCity;
      visitedCities.Clear();

      visitedCities.Add(lastCity);
    }
  }
}
