﻿using System.Text;
using ODEGenerator.Formatter;

namespace ODEGenerator.SyntaxTree.Numerical
{
    class Constant :NumericalElement
    {
        public Constant(string nameOfConstant, double valueOfConstant)
            : base(nameOfConstant, valueOfConstant)
        {
        }

        public override StringBuilder Accept(IVisitor visitor)
        {
            return visitor.Visit(this);
        }

    }
}
