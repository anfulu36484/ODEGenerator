using System.Text;
using ODEGenerator.Formatter;

namespace ODEGenerator.SyntaxTree.Operators.Multarny
{
    public class PlusOperator :MultarnyOperator
    {
        public PlusOperator() : base("+")
        {

        }

        public override StringBuilder Accept(IVisitor visitor)
        {
            return visitor.Visit(this);
        }

    }
}
