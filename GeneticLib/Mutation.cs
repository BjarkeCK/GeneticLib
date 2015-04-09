using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticLib
{
    public class Mutation : IEvolveFunction
    {
        private static Random _random = new Random();

        private double _properbility;

        public void SetProperbility(double properbility)
        {
            this._properbility = properbility;
        }

        public void Execute<T>(IList<T> selection, Population<T> population) where T : IChromosome<T>
        {
            foreach (T chromosome in population)
            {
                if (_random.NextDouble() < _properbility)
                {
                    chromosome.RandomizeGene(_random.Next(0, chromosome.GetNumberOfGenes()), _random.NextDouble());
                }
            }
        }
    }
}