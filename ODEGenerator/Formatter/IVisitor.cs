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
    interface IVisitor
    {
        StringBuilder Visit(Constant constant);

        StringBuilder Visit(Substance substance);

        StringBuilder Visit(PlusOperator @operator);

        StringBuilder Visit(MinusOperator @operator);

        StringBuilder Visit(MultiplicationOperator @operator);

        StringBuilder Visit(DivisionOperator @operator);

        StringBuilder Visit(ParenthesesOperator @operator);

        StringBuilder Visit(Function function);

        StringBuilder Visit(InfinitesimalIncrement increment);

        StringBuilder Visit(EqualOperator @operator);

        StringBuilder Visit(ExponentiationOperator @operator);

        StringBuilder Visit(RightPartOfOde rightPartOfOde);

    }

}
