using System.Collections.Generic;

namespace Algorithms {
  class Specimen {
    public List<int> chromosomes { get; set; }

    public Specimen(int chromosomeSize) {
      chromosomes = new List<int>();

      for (int i = 0; i < chromosomeSize; ++i) {
        chromosomes.Add(-1);
      }
    }

    public Specimen(List<int> chromosomes) {
      this.chromosomes = chromosomes;
    }

    public int GetGen(int number) {
      return chromosomes[number];
    }

    public void SetGen(int number, int allel) {
      chromosomes[number] = allel;
    }

    public bool ContainsGen(int gen) {
      return chromosomes.Contains(gen);
    }
  }
}
