using System.Collections.Generic;
using System.Linq;
using System.Text;
using ODEGenerator.SyntaxTree;
using ODEGenerator.SyntaxTree.Numerical;
using ODEGenerator.SyntaxTree.Operators.Binary;
using ODEGenerator.SyntaxTree.Operators.Multarny;
using ODEGenerator.SyntaxTree.Operators.Unary;

namespace ODEGenerator.Formatter
{
    interface IFormatter
    {
        StringBuilder Visit(NumericalElement numericalElement);

        StringBuilder Visit(PlusOperator pOperator);

        StringBuilder Visit(MinusOperator mOperator);

        StringBuilder Visit(MultiplicationOperator mOperator);

        StringBuilder Visit(DivisionOperator dOperator);

        StringBuilder Visit(ParenthesesOperator pOperator);

        StringBuilder Visit(Function function);

        StringBuilder Visit(InfinitesimalIncrement increment);

        StringBuilder Visit(EqualOperator bOperator);

        StringBuilder Visit(ExponentiationOperator bOperator);

        StringBuilder Visit(RightPartOfOde rightPartOfOde);

    }

}
