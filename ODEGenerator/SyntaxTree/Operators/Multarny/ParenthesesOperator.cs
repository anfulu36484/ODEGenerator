﻿using System.Text;
using ODEGenerator.Formatter;

namespace ODEGenerator.SyntaxTree.Operators.Multarny
{
    class ParenthesesOperator :MultarnyOperator
    {
        public ParenthesesOperator() : base("Круглые скобки")
        {
        }

        public override StringBuilder Accept(IVisitor visitor)
        {
            return visitor.Visit(this);
        }


    }
}
