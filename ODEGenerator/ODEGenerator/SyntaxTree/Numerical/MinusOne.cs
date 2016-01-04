using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODEGenerator.Formatter;

namespace ODEGenerator.SyntaxTree.Numerical
{
    class MinusOne :Constant
    {
        public MinusOne() : base("(-1)", -1)
        {
        }

    }
}
