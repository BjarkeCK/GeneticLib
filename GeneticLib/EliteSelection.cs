using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticLib
{
    public class EliteSelection : ISelection
    {
        private int _numberOfElites;

        public EliteSelection(int numberOfElites)
        {
            this._numberOfElites = numberOfElites;
        }

        public IList<T> ApplySelection<T>(Population<T> population) where T : IChromosome<T>
        {
            return population.OrderByDescending(x => x.FitnessValue).Take(_numberOfElites).ToArray();
        }
    }
}