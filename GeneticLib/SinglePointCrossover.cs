using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticLib
{
    public class SinglePointCrossover : IEvolveFunction
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
                    int crossOverPoint = _random.Next(0, chromosome.GetNumberOfGenes());
                    T mother = selection[_random.Next(0, selection.Count)];
                    T father = selection[_random.Next(0, selection.Count)];

                    for (int i = 0; i < crossOverPoint; i++)
                    {
                        chromosome.SetGene(mother, i);
                    }

                    for (int i = crossOverPoint; i < chromosome.GetNumberOfGenes(); i++)
                    {
                        chromosome.SetGene(father, i);
                    }
                }
            }
        }
    }
}