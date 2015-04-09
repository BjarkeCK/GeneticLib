using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AForge.Neuro;
using AForge.Neuro.Learning;

namespace GeneticLib
{
    internal class Program
    {
        private static Random r = new Random();

        private static void Main(string[] args)
        {
            NeuralNetworkChromosome[] population = Enumerable.Range(0, 100).Select(x => new NeuralNetworkChromosome(1, 20, 10, 1)).ToArray();

            GeneticTrainer<NeuralNetworkChromosome> trainer = new GeneticTrainer<NeuralNetworkChromosome>(population, FitnessFunction, new EliteSelection(10));
            trainer.AddEvolveFunction<UniformCrossover>(0.75);
            trainer.AddEvolveFunction<Mutation>(0.2);
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(trainer.GetFittestChromosome().FitnessValue);
                trainer.Evolve();
            }

            double input = r.NextDouble();
            foreach (var item in population)
            {
                Console.WriteLine(item.Compute(new double[] { input })[0]);
            }
        }

        private static double FitnessFunction(NeuralNetworkChromosome chromosome)
        {
            double input = r.NextDouble();
            double output = chromosome.Compute(new double[] { input })[0];
            return input * output;
        }
    }
}