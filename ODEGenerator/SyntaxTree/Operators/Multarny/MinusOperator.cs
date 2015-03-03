using System.Text;
using ODEGenerator.Formatter;

namespace ODEGenerator.SyntaxTree.Operators.Multarny
{
    class MinusOperator : MultarnyOperator
    {
        public MinusOperator()
            : base("+")
        {

        }

        public override StringBuilder Accept(IFormatter formatter)
        {
            return formatter.Visit(this);
        }

        public override bool IsNegative()
        {
            return false;
        }
    }
}
