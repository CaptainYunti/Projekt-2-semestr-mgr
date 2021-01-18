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
            //alg = SimulatedAnnealingAlgorithm.GetInstance();
            algorithms = new List<IAlgorithm>();
            //alg = ScriptableObject.CreateInstance();
            algorithms.Add(new SimulatedAnnealingAlgorithm());
            algorithms.Add(new AntColonyOptimization());
            algorithms.Add(new GeneticAlgorithm());
            algorithms.Add(new NearestNeigbourAlgorithm());

            playerAI = GameObject.FindGameObjectWithTag("Player AI");


            /*
            print("Wyżarzanie");
            SimulatedAnnealing();
            print("Mrówki");
            AntColony();
            print("Genetyczny");
            GeneticAlgorithm();
            print("Sąsiad");
            NearestNeighbour();
            */

            //playerAI.GetComponent<PlayerAI>().enabled = true;
            //PlayerAI.areAlgorithmsCompute = false;
            //count = 0;
            //numberAlgorithm = 4;

            print(graphPath);


        }


        private void Update()
        {
            
        }

        public static List<int> SimulatedAnnealing()
        {
            //SimulatedAnnealingAlgorithm.LoadGraph(graphPath);
            //SimulatedAnnealingAlgorithm.Start();
            //return SimulatedAnnealingAlgorithm.GetCities();

            print("Wyżarzanie");
            algorithms[0].LoadGraph(graphPath);
            algorithms[0].Start();
            //print("Ile miast: " + algorithms[0].GetCities().ToArray().Length); //dziala
            //print("Pierwsze miasto:" + algorithms[0].GetCities()[0]); //dziala
            //PlayerAI.roads[0] = algorithms[0].GetCities(); //nie dziala
            return algorithms[0].GetCities();

        }

        public static List<int> AntColony()
        {
            print("Mrówki");
            algorithms[1].LoadGraph(graphPath);
            algorithms[1].Start();
            //PlayerAI.roads[1] = algorithms[1].GetCities();
            return algorithms[1].GetCities();
        }

        public static List<int> GeneticAlgorithm()
        {
            print("Genetyczny");
            algorithms[2].LoadGraph(graphPath);
            algorithms[2].Start();
            //PlayerAI.roads[2] = algorithms[2].GetCities();
            return algorithms[2].GetCities();
        }

        public static List<int> NearestNeighbour()
        {
            print("Sąsiad");
            algorithms[3].LoadGraph(graphPath);
            algorithms[3].Start();
            // PlayerAI.roads[3] = algorithms[1].GetCities();
            return algorithms[3].GetCities();
        }

    }
}
