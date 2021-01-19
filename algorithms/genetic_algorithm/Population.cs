using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms {
  class Population {
    public List<Specimen> specimens { get; set; }
    
    private int chromosomesSize;
    private static Random random = new Random();

    public Population(int chromosomesSize) {
      this.specimens = new List<Specimen>();
      this.chromosomesSize = chromosomesSize;
    }

    public int GetSize() {
      return specimens.Count;
    }

    public void GenerateSpecimens(int populationSize) {
      for (int i = 0; i < populationSize; ++i) {
        List<int> chromosomes = new List<int>();

        for (int j = 0; j < chromosomesSize; ++j) {
          chromosomes.Add(j);
        }

        chromosomes = chromosomes.OrderBy(x => Guid.NewGuid()).ToList();

        specimens.Add(new Specimen(chromosomes));
      }
    }

    public void Add(Specimen specimen) {
      specimens.Add(specimen);
    }

    public Specimen GetSpecimen(int number) {
      return specimens[number];
    }

    public Specimen GetRandomSpecimen() {
      return specimens[random.Next(GetSize())];
    }

    public void Sort(Graph graph) {
      List<(int, int)> ranking = new List<(int, int)>();

      for (int i = 0; i < GetSize(); ++i) {
        ranking.Add((i, graph.CalculatePathDistance(specimens[i].chromosomes)));
      }

      ranking.Sort((chromosomes1, chromosomes2) => 
        chromosomes1.Item2.CompareTo(chromosomes2.Item2));

      List<Specimen> sortedSpecimens = new List<Specimen>();

      for (int i = 0; i < GetSize(); ++i) {
        sortedSpecimens.Add(specimens[ranking[i].Item1]);
      }

      specimens = sortedSpecimens;   
    }

    public void RemoveLastSpecimen() {
      specimens.RemoveAt(GetSize() - 1);
    }
  }
}
