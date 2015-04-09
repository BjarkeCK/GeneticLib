using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AForge.Neuro;

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
            this.FittestChromosome = population[0];
            this.CurrentGeneration = 0;
        }

        public Population<T> Population { get { return _population; } }

        public int CurrentGeneration { get; private set; }

        /// <summary>
        /// Evolve the the population to the next generation.
        /// </summary>
        public void Evolve()
        {
            foreach (T chromosome in _population)
            {
                chromosome.FitnessValue = _fitnessFunction.Invoke(chromosome);
            }

            GetFittestChromsome();

            IList<T> selection = _selection.ApplySelection(_population).Select(x => x.Clone()).ToArray();

            foreach (IEvolveFunction function in _eveolveFunctions)
            {
                function.Execute(selection, this._population);
            }

            EnsureSurvival();

            foreach (T chromosome in _population)
            {
                chromosome.OnEvolved();
            }

            this.CurrentGeneration++;
        }

        /// <summary>
        /// Ensures that the best chromosome gets carried over to the new generation.
        /// </summary>
        private void EnsureSurvival()
        {
            for (int i = 0; i < FittestChromosome.GetNumberOfGenes(); i++)
            {
                _population[0].SetGene(FittestChromosome, i);
            }
        }

        private void GetFittestChromsome()
        {
            double bestFitnessValue = 0;
            foreach (T chromosome in _population)
            {
                if (chromosome.FitnessValue > bestFitnessValue)
                {
                    FittestChromosome = chromosome.Clone();
                    bestFitnessValue = chromosome.FitnessValue;
                }
            }
        }

        public T FittestChromosome { get; private set; }

        public void AddEvolveFunction<T1>(double properbility) where T1 : IEvolveFunction, new()
        {
            T1 f = new T1();
            f.SetProperbility(properbility);
            this._eveolveFunctions.Add(f);
        }
    }
}