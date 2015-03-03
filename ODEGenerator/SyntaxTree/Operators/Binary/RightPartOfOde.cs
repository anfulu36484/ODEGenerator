using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODEGenerator.Formatter;
using ODEGenerator.SyntaxTree.Numerical;
using ODEGenerator.SyntaxTree.Operators.Multarny;
using ODEGenerator.SyntaxTree.Operators.Unary;

namespace ODEGenerator.SyntaxTree.Operators.Binary
{
    class RightPartOfOde :BinaryOperator
    {
        private DivisionOperator _divisionOperator;

        public RightPartOfOde(NumericalElement firstElement, NumericalElement secondElement) 
            : base("Правая часть дифференциального уравнения", firstElement, secondElement)
        {
            _divisionOperator = new DivisionOperator();
            _divisionOperator.AddElements(new InfinitesimalIncrement(firstElement),
                                     new InfinitesimalIncrement(secondElement));
        }

        public override StringBuilder Accept(IFormatter formatter)
        {
            return formatter.Visit(_divisionOperator);
        }
    }
}
