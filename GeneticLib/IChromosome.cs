using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticLib
{
    public interface IChromosome<T>
    {
        double FitnessValue { get; set; }

        void SetGene(T other, int geneIndex);

        void RandomizeGene(int gene, double amount);

        void OnEvolved();

        int GetNumberOfGenes();

        T Clone();
    }
}