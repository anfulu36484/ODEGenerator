﻿using System.Text;
using ODEGenerator.Formatter;

namespace ODEGenerator.SyntaxTree.Operators.Multarny
{
    class DivisionOperator :MultarnyOperator
    {
        public DivisionOperator() : base("/")
        {
        }

        public override StringBuilder Accept(IVisitor visitor)
        {
            return visitor.Visit(this);
        }

 
    }
}
