using System.Text;
using ODEGenerator.Formatter;
using ODEGenerator.SyntaxTree.Numerical;

namespace ODEGenerator.SyntaxTree.Operators.Multarny
{
    class Function :MultarnyOperator
    {

        public Function(string nameOfFunction, params NumericalElement[] arguments) 
            : base(nameOfFunction)
        {
            Elements.AddRange(arguments);
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
