using System.Text;
using ODEGenerator.Formatter;
using ODEGenerator.SyntaxTree.Numerical;

namespace ODEGenerator.SyntaxTree.Operators.Binary
{
    class ExponentiationOperator :BinaryOperator
    {
        public ExponentiationOperator(NumericalElement firstElement, NumericalElement secondElement) 
            : base("^", firstElement, secondElement)
        {
        }

        public override StringBuilder Accept(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
