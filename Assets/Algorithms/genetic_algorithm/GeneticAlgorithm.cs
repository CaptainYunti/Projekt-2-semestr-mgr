using System;
using System.Collections.Generic;

namespace Algorithms
{
    public class GeneticAlgorithm : IAlgorithm
    {
        private Graph graph;
        private Population population;

        private static Random random = new Random();

        public void LoadGraph(string path)
        {
            graph = new Graph();

            graph.Load(path, Algorithm.GA);
        }

        public void Start(int iterationsNumber)
        {
            population = new Population(graph.size);

            population.GenerateSpecimens(GAParams.populationSize);

            for (int j = 0; j < iterationsNumber; ++j)
            {
                CrossAllSpecimens();
                MutateAllSpecimens();
                SelectBestSpecimens();

                if (graph.CalculatePathDistance(graph.cities) >
                    graph.CalculatePathDistance(population.GetSpecimen(0).chromosomes))
                {
                    graph.cities = population.GetSpecimen(0).chromosomes;
                }
            }
        }

        public int GetBestSolution()
        {
            return graph.GetShortestPath();
        }

        private (Specimen, Specimen) CrossSpecimens(Specimen specimen1,
                                                    Specimen specimen2)
        {

            Specimen newSpecimen1 = new Specimen(graph.size);
            Specimen newSpecimen2 = new Specimen(graph.size);

            int k1 = random.Next(graph.size);
            int k2 = random.Next(graph.size);

            if (k2 < k1)
            {
                Swap(ref k1, ref k2);
            }

            for (int j = k1; j <= k2; ++j)
            {
                newSpecimen1.SetGen(j, specimen2.GetGen(j));
                newSpecimen2.SetGen(j, specimen1.GetGen(j));
            }

            int oldGenIndex = k2 + 1;
            int newSpecimen1GenIndex = k2 + 1;
            int newSpecimen2GenIndex = k2 + 1;

            for (int j = 0; j < graph.size; ++j)
            {
                oldGenIndex = (oldGenIndex >= graph.size) ? 0 : oldGenIndex;
                newSpecimen1GenIndex =
                  (newSpecimen1GenIndex >= graph.size) ? 0 : newSpecimen1GenIndex;
                newSpecimen2GenIndex =
                  (newSpecimen2GenIndex >= graph.size) ? 0 : newSpecimen2GenIndex;

                if (!newSpecimen1.ContainsGen(specimen1.GetGen(oldGenIndex)))
                {
                    newSpecimen1.SetGen(newSpecimen1GenIndex++,
                                        specimen1.GetGen(oldGenIndex));
                }

                if (!newSpecimen2.ContainsGen(specimen2.GetGen(oldGenIndex)))
                {
                    newSpecimen2.SetGen(newSpecimen2GenIndex++,
                                        specimen2.GetGen(oldGenIndex));
                }

                oldGenIndex++;
            }

            return (newSpecimen1, newSpecimen2);
        }

        private void CrossAllSpecimens()
        {
            for (int i = 0; i < GAParams.populationSize; ++i)
            {
                if (random.NextDouble() > GAParams.pk)
                {
                    continue;
                }

                Specimen specimen1 = population.GetRandomSpecimen();
                Specimen specimen2 = population.GetRandomSpecimen();

                (Specimen, Specimen) newSpecimens =
                  CrossSpecimens(specimen1, specimen2);

                population.Add(newSpecimens.Item1);
                population.Add(newSpecimens.Item2);
            }
        }

        private void MutateSpecimen(ref Specimen specimen)
        {
            int k1 = random.Next(graph.size);
            int k2 = random.Next(graph.size);

            if (k2 < k1)
            {
                Swap(ref k1, ref k2);
            }

            List<int> subChromosome = new List<int>();

            for (int i = k2; i > k1; --i)
            {
                subChromosome.Add(specimen.GetGen(i));
            }

            int genIndex = 0;

            for (int i = k1 + 1; i <= k2; ++i)
            {
                specimen.SetGen(i, subChromosome[genIndex++]);
            }
        }

        private void MutateAllSpecimens()
        {
            for (int i = 0; i < population.GetSize(); ++i)
            {
                if (random.NextDouble() > GAParams.pm)
                {
                    continue;
                }

                Specimen specimen = population.GetSpecimen(i);

                MutateSpecimen(ref specimen);
            }
        }

        private void SelectBestSpecimens()
        {
            population.Sort(graph);

            while (population.GetSize() > GAParams.populationSize)
            {
                population.RemoveLastSpecimen();
            }
        }

        private void Swap(ref int a, ref int b)
        {
            int temp = a;

            a = b;
            b = temp;
        }

        public List<int> GetCities()
        {
            return graph.cities;
        }

        public int ShortestPath()
        {
            return graph.CalculatePathDistance(graph.cities);
        }
    }
}



