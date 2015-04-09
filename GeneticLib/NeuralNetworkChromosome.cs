using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AForge.Neuro;

namespace GeneticLib
{
    public class NeuralNetworkChromosome : IChromosome<NeuralNetworkChromosome>
    {
        private static Random _random = new Random();
        private double[] _genes;
        private ActivationNetwork _activationNetwork;

        public NeuralNetworkChromosome(int numOfinputNeurons, params int[] layers)
            : this(new ActivationNetwork(new BipolarSigmoidFunction(2), numOfinputNeurons, layers))
        {
        }

        public NeuralNetworkChromosome(ActivationNetwork activationNetwork)
        {
            this._activationNetwork = activationNetwork;
            this._genes = new double[CountNumberOfGenesInNeuralNetwork()];

            for (int i = 0; i < _genes.Length; i++)
            {
                _genes[i] = GenerateGene();
            }
            SetNeurons();
        }

        private NeuralNetworkChromosome(double[] genes, ActivationNetwork activationNetwork, double fitnessValue)
        {
            this._genes = genes;
            this._activationNetwork = activationNetwork;
            this.FitnessValue = fitnessValue;
        }

        public double FitnessValue { get; set; }

        public double[] Compute(double[] input)
        {
            return _activationNetwork.Compute(input);
        }

        public void SetGene(NeuralNetworkChromosome other, int geneIndex)
        {
            _genes[geneIndex] = other._genes[geneIndex];
        }

        public void RandomizeGene(int geneIndex, double amount)
        {
            _genes[geneIndex] = Math.Max(Math.Min(1, _genes[geneIndex] + GenerateGene() * amount), 0);
        }

        public NeuralNetworkChromosome Clone()
        {
            return new NeuralNetworkChromosome(_genes.ToArray(), _activationNetwork, FitnessValue);
        }

        public int GetNumberOfGenes()
        {
            return _genes.Length;
        }

        private void SetNeurons()
        {
            int v = 0;
            for (int i = 0, layersCount = _activationNetwork.Layers.Length; i < layersCount; i++)
            {
                Layer layer = _activationNetwork.Layers[i];
                for (int j = 0, neuronsCount = layer.Neurons.Length; j < neuronsCount; j++)
                {
                    ActivationNeuron neuron = (ActivationNeuron)layer.Neurons[j];
                    for (int k = 0, weightsCount = neuron.Weights.Length; k < weightsCount; k++)
                        neuron.Weights[k] = _genes[v++];

                    neuron.Threshold = _genes[v++];
                }
            }
        }

        private int CountNumberOfGenesInNeuralNetwork()
        {
            return _activationNetwork.Layers.Sum(x => x.Neurons.Sum(c => c.InputsCount + 1));
        }

        private double GenerateGene()
        {
            return _random.NextDouble();
        }

        public void OnEvolved()
        {
            SetNeurons();
        }
    }
}