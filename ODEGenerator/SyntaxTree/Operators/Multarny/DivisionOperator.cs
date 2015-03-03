using System.Text;
using ODEGenerator.Formatter;

namespace ODEGenerator.SyntaxTree.Operators.Multarny
{
    class DivisionOperator :MultarnyOperator
    {
        public DivisionOperator() : base("/")
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
