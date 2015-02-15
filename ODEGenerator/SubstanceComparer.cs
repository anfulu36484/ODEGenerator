using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODEGenerator
{
    class SubstanceComparer : IEqualityComparer<Substance>
    {
        public bool Equals(Substance x, Substance y)
        {
            return x.NameOfSubstance == y.NameOfSubstance;
        }

        public int GetHashCode(Substance obj)
        {
            return obj.NameOfSubstance.GetHashCode();
        }
    }
}
