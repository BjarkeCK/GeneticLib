using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticLib
{
    public class GeneticTrainer<T> where T : IChromosome<T>
    {
        private Population<T> _population;
        private FitnessFunction<T> _fitnessFunction;
        private List<IEvolveFunction> _eveolveFunctions;
        private ISelection _selection;

        public GeneticTrainer(IList<T> population, FitnessFunction<T> fitnessFunction, ISelection selection)
        {
            this._eveolveFunctions = new List<IEvolveFunction>();
            this._population = new Population<T>(population);
            this._fitnessFunction = fitnessFunction;
            this._selection = selection;
        }

        public void Evolve()
        {
            foreach (T chromosome in _population)
            {
                chromosome.FitnessValue = _fitnessFunction.Invoke(chromosome);
            }

            IList<T> selection = _selection.ApplySelection(_population).Select(x => x.Clone()).ToArray();

            foreach (IEvolveFunction function in _eveolveFunctions)
            {
                function.Execute(selection, this._population);
            }

            foreach (T chromosome in _population)
            {
                chromosome.OnEvolved();
            }
        }

        public Population<T> Population { get { return _population; } }

        public T GetFittestChromosome()
        {
            return _population.OrderByDescending(x => x.FitnessValue).First();
        }

        public void AddEvolveFunction<T1>(double properbility) where T1 : IEvolveFunction, new()
        {
            T1 f = new T1();
            f.SetProperbility(properbility);
            this._eveolveFunctions.Add(f);
        }
    }
}