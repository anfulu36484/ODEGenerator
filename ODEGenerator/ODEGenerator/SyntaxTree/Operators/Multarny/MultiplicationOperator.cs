﻿using System.Text;
using ODEGenerator.Formatter;

namespace ODEGenerator.SyntaxTree.Operators.Multarny
{
    public class MultiplicationOperator :MultarnyOperator
    {
        public MultiplicationOperator() : base("*")
        {
        }

        public override StringBuilder Accept(IVisitor visitor)
        {
            return visitor.Visit(this);
        }


    }
}
