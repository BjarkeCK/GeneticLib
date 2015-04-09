using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneticLib;

namespace GeneticLibExamples
{
    internal class Program
    {
        private static GeneticTrainer<NeuralNetworkChromosome> _trainer;
        private static Random _random = new Random();
        private const int INPUT_NEURONS_COUNT = 2;

        private static void Main(string[] args)
        {
            List<NeuralNetworkChromosome> population = CreatePopulation();
            _trainer = new GeneticTrainer<NeuralNetworkChromosome>(population, FitnessFunction, new EliteSelection(10));
            _trainer.AddEvolveFunction<UniformCrossover>(0.75);
            _trainer.AddEvolveFunction<SinglePointCrossover>(0.75);
            _trainer.AddEvolveFunction<Mutation>(0.2);

            for (int i = 0; i < 100000; i++)
            {
                TestNetwork();
                TrainNetwork();
            }
        }

        private static void TrainNetwork()
        {
            for (int n = 0; n < 100; n++) // n generations
            {
                _trainer.Evolve();
            }
        }

        private static double[] CalculateOutput(double[] input)
        {
            double[] output = input.ToArray();
            Array.Sort(output);
            return output;
        }

        private static double[] GenerateInput()
        {
            return Enumerable.Range(0, INPUT_NEURONS_COUNT).Select(x => 1 - _random.NextDouble() * 2).ToArray();
        }

        private static void TestNetwork()
        {
            NeuralNetworkChromosome network = _trainer.FittestChromosome;
            double[] input = GenerateInput();
            double[] output = network.Compute(input);
            double[] target = CalculateOutput(input);

            Console.WriteLine("After {0} generations:", _trainer.CurrentGeneration);
            Console.WriteLine("calculated output: {0}", string.Join(",", output.Select(x => x.ToString("0.00"))));
            Console.WriteLine("target output:     {0}", string.Join(",", target.Select(x => x.ToString("0.00"))));
            Console.WriteLine("Fitness Value:     {0}", network.FitnessValue);
            Console.WriteLine();

            FitnessFunction(network);
        }

        private static List<NeuralNetworkChromosome> CreatePopulation()
        {
            List<NeuralNetworkChromosome> population = new List<NeuralNetworkChromosome>();
            for (int i = 0; i < 100; i++)
            {
                population.Add(new NeuralNetworkChromosome(INPUT_NEURONS_COUNT, 5, 5, INPUT_NEURONS_COUNT));
            }
            return population;
        }

        private static double FitnessFunction(NeuralNetworkChromosome chromosome)
        {
            double fitness = 0;
            int n = 3;
            for (int i = 0; i < n; i++)
            {
                double[] input = GenerateInput();
                double[] outputFromNetwork = chromosome.Compute(input);
                double[] calculatedOutput = CalculateOutput(input);
                double sum = 0;

                for (int z = 0; z < input.Length; z++)
                {
                    sum += Math.Abs(calculatedOutput[z] - outputFromNetwork[z]);
                }

                fitness += INPUT_NEURONS_COUNT - sum;
            }

            return fitness / n / 2;
        }
    }
}