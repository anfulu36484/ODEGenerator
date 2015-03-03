using System.Text;
using ODEGenerator.Formatter;

namespace ODEGenerator.SyntaxTree.Operators.Multarny
{
    class MultiplicationOperator :MultarnyOperator
    {
        public MultiplicationOperator() : base("*")
        {
        }

        public override StringBuilder Accept(IFormatter formatter)
        {
            return formatter.Visit(this);
        }

        public override bool IsNegative()
        {
            if (Elements.IsEven(n => n.IsNegative()))
                return false;
            return true;
        }
    }
}
