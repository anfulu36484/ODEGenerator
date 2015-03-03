using System.Text;
using ODEGenerator.Formatter;

namespace ODEGenerator.SyntaxTree.Operators.Binary
{
    class EqualOperator :BinaryOperator
    {
        public EqualOperator(ElementOfSyntaxTree firstElement, ElementOfSyntaxTree secondElement) :
            base("=", firstElement, secondElement)
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
