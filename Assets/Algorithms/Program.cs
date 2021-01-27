using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.IO;

namespace Algorithms
{
  class Program : MonoBehaviour
  {

        //int numberAlgorithm;
        //static int count;
        public static string graphPath;
        public static List<IAlgorithm> algorithms;
        public static SimulatedAnnealingAlgorithm alg;
        GameObject playerAI;

        void Start()
        {
            algorithms = new List<IAlgorithm>();
            algorithms.Add(new SimulatedAnnealingAlgorithm());
            algorithms.Add(new AntColonyOptimization());
            algorithms.Add(new GeneticAlgorithm());
            algorithms.Add(new NearestNeigbourAlgorithm());

            playerAI = GameObject.FindGameObjectWithTag("Player AI");


            print(graphPath);


        }


        private void Update()
        {
            
        }

        public static List<int> SimulatedAnnealing()
        {

            print("Wyżarzanie");
            algorithms[0].LoadGraph(graphPath);
            algorithms[0].Start(1000);
            print("Dlugosc: " + algorithms[0].ShortestPath());
            return algorithms[0].GetCities();

        }

        public static List<int> AntColony()
        {
            print("Mrówki");
            algorithms[1].LoadGraph(graphPath);
            algorithms[1].Start(1);
            //PlayerAI.roads[1] = algorithms[1].GetCities();
            print("Dlugosc: " + algorithms[1].ShortestPath());
            return algorithms[1].GetCities();
        }

        public static List<int> GeneticAlgorithm()
        {
            print("Genetyczny");
            algorithms[2].LoadGraph(graphPath);
            algorithms[2].Start(1000);
            //PlayerAI.roads[2] = algorithms[2].GetCities();
            print("Dlugosc: " + algorithms[2].ShortestPath());
            return algorithms[2].GetCities();
        }

        public static List<int> NearestNeighbour()
        {
            print("Sąsiad");
            algorithms[3].LoadGraph(graphPath);
            algorithms[3].Start(1);
            // PlayerAI.roads[3] = algorithms[1].GetCities();
            print("Dlugosc: " + algorithms[3].ShortestPath());
            return algorithms[3].GetCities();
        }

    }
}
