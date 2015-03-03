using System.Collections.Generic;

namespace ODEGenerator.SyntaxTree.Numerical
{
    class SubstanceComparer : IEqualityComparer<Substance>
    {
        public bool Equals(Substance x, Substance y)
        {
            return x.Name == y.Name;
        }

        public int GetHashCode(Substance obj)
        {
            return obj.GetHashCode();
        }
    }
}
