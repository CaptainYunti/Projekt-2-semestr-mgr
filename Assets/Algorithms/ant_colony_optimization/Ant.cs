using System;
using System.Collections.Generic;
using UnityEngine;

namespace Algorithms {
  class Ant{
    private Graph graph;
    private List<int> visitedCities;

    int startCity;
    int lastCity;

    public Ant(Graph graph) {
      this.graph = graph;
      this.visitedCities = new List<int>();

      ClearCycle();
    }

    public void DoCycle() {
      for (int i = 0; i < graph.size - 1; ++i) {
        NextCity();
      }

      if (graph.CalculatePathDistance(visitedCities) <
          graph.CalculatePathDistance(graph.cities)) {
        graph.cities = new List<int>(visitedCities);
      }

      ClearCycle();
    }

    private void NextCity() {
      int city = SelectNextCity();
      int distance = graph.Edge(lastCity, city).distance;

      visitedCities.Add(city);

      ((ACOEdge) graph.Edge(lastCity, city)).pheromonesConcentration +=
        ACOParams.q /distance;

      lastCity = city;   
    }

    private int SelectNextCity() {
      int city = -1;
      // probibility of choosing of the city;
      double p = 0.0;

      for (int currentCity = 0; currentCity < graph.size; ++currentCity) {
        if (visitedCities.Contains(currentCity)) {
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

    private double CountProbabilityNumerator(int city)
    {
            //print("city: " + city.ToString());
      double pheromonesConcentration = 
        ((ACOEdge) graph.Edge(lastCity, city)).pheromonesConcentration;
      // value of the local criterion function
      double eta = 1.0 / graph.Edge(lastCity, city).distance;

      double probabilityNumerator = 
        Math.Pow(pheromonesConcentration, ACOParams.alpha) * 
        Math.Pow(eta, ACOParams.beta);

      return probabilityNumerator;
    }

    private double CountProbabilityDenumerator() {
      double probabilityDenumerator = 0;

      for (int city = 0; city < graph.size; ++city) {
        if (visitedCities.Contains(city)) {
          continue;
        }

        var distance = graph.Edge(lastCity, city).distance;

        probabilityDenumerator += 
          Math.Pow(distance, ACOParams.alpha) * 
          Math.Pow(distance, ACOParams.beta);
      }

      return probabilityDenumerator;
    }

    private void ClearCycle() {
            System.Random random = new System.Random();

      startCity = random.Next(graph.size);
      lastCity = startCity;
      visitedCities.Clear();

      visitedCities.Add(lastCity);
    }
  }
}
