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

        public override StringBuilder Accept(IVisitor visitor)
        {
            return visitor.Visit(this);
        }

 
    }
}
