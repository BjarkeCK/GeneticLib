using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticLib
{
    public class Population<T> : IEnumerable<T> where T : IChromosome<T>
    {
        private IList<T> _population;

        internal Population(IList<T> population)
        {
            this._population = population;
        }

        public int PopulationSize { get { return _population.Count; } }

        public T GetChromosome(int i)
        {
            return _population[i];
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return _population.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _population.GetEnumerator();
        }
    }
}