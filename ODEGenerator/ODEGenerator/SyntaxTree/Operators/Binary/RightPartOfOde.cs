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
    public class RightPartOfOde :BinaryOperator
    {
        private DivisionOperator _divisionOperator;

        public RightPartOfOde(Substance firstElement, NumericalElement secondElement) 
            : base("Правая часть дифференциального уравнения", firstElement, secondElement)
        {
            _divisionOperator = new DivisionOperator();
            DivisionOperator.AddElements(new InfinitesimalIncrement(firstElement),
                                     new InfinitesimalIncrement(secondElement));
        }

        public DivisionOperator DivisionOperator
        {
            get { return _divisionOperator; }
        }

        public override StringBuilder Accept(IVisitor visitor)
        {
            return visitor.Visit(this);
        }

    }
}
