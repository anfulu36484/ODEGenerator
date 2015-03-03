using System.Text;
using ODEGenerator.Formatter;

namespace ODEGenerator.SyntaxTree.Operators.Multarny
{
    class PlusOperator :MultarnyOperator
    {
        public PlusOperator() : base("+")
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
