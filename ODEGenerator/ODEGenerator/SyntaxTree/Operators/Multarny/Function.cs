using System.Text;
using ODEGenerator.Formatter;
using ODEGenerator.SyntaxTree.Numerical;

namespace ODEGenerator.SyntaxTree.Operators.Multarny
{
    public class Function :MultarnyOperator
    {

        public Function(string nameOfFunction, params NumericalElement[] arguments) 
            : base(nameOfFunction)
        {
            Elements.AddRange(arguments);
        }

        public override StringBuilder Accept(IVisitor visitor)
        {
            return visitor.Visit(this);
        }


    }
}
