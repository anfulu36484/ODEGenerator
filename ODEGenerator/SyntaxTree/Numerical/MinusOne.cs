using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODEGenerator.SyntaxTree.Numerical
{
    class MinusOne :Constant
    {
        public MinusOne() : base("-1", -1)
        {
        }

        public override bool IsNegative()
        {
            return true;
        }
    }
}
