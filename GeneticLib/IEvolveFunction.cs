using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticLib
{
    public interface IEvolveFunction
    {
        void SetProperbility(double properbility);

        void Execute<T>(IList<T> selection, Population<T> population) where T : IChromosome<T>;
    }
}