using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticLib
{
    public interface ISelection
    {
        IList<T> ApplySelection<T>(Population<T> population) where T : IChromosome<T>;
    }
}