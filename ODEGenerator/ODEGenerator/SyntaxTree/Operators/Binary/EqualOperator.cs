using System.Text;
using ODEGenerator.Formatter;

namespace ODEGenerator.SyntaxTree.Operators.Binary
{
    public class EqualOperator :BinaryOperator
    {
        public EqualOperator(ElementOfSyntaxTree firstElement, ElementOfSyntaxTree secondElement) :
            base("=", firstElement, secondElement)
        {
        }

        public override StringBuilder Accept(IVisitor visitor)
        {
            return visitor.Visit(this);
        }


    }
}
