using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticLib
{
    public class UniformCrossover : IEvolveFunction
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
                    for (int i = 0; i < chromosome.GetNumberOfGenes(); i++)
                    {
                        if (_random.NextDouble() > 0.5)
                        {
                            T mother = selection[_random.Next(0, selection.Count)];
                            chromosome.SetGene(mother, i);
                        }
                        else
                        {
                            T father = selection[_random.Next(0, selection.Count)];
                            chromosome.SetGene(father, i);
                        }
                    }
                }
            }
        }
    }
}