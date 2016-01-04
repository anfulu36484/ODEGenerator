using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Microsoft.Win32.SafeHandles;
using ODEGenerator.SyntaxTree;
using ODEGenerator.SyntaxTree.Numerical;
using ODEGenerator.SyntaxTree.Operators.Binary;
using ODEGenerator.SyntaxTree.Operators.Multarny;
using ODEGenerator.SyntaxTree.Operators.Unary;

namespace ODEGenerator.Formatter
{
    public class MathVisitor :IVisitor
    {


        public virtual StringBuilder Visit(Constant constant)
        {
            return new StringBuilder(constant.Name);
        }

        public virtual StringBuilder Visit(Substance substance)
        {
            return new StringBuilder(substance.Name);
        }


        StringBuilder VisitToBasicArithmeticPperator(MultarnyOperator @operator)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < @operator.Elements.Count; i++)
            {
                if (i != 0)
                    sb.Append(@operator.Name);
                sb.Append(@operator.Elements[i].Accept(this));
            }
            return sb;
        }


        /// <param name="operator"> + </param>
        public virtual StringBuilder Visit(PlusOperator @operator)
        {
            return VisitToBasicArithmeticPperator(@operator);
        }


        /// <param name="operator"> - </param>
        public virtual StringBuilder Visit(MinusOperator @operator)
        {
            return VisitToBasicArithmeticPperator(@operator);
        }


        /// <param name="operator">*</param>
        public StringBuilder Visit(MultiplicationOperator @operator)
        {
            return VisitToBasicArithmeticPperator(@operator);
        }

        /// <param name="operator">/</param>
        public virtual StringBuilder Visit(DivisionOperator @operator)
        {
            return VisitToBasicArithmeticPperator(@operator);
        }

        /// <param name="pOperator">( )</param>
        public virtual StringBuilder Visit(ParenthesesOperator pOperator)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("(");
            foreach (var element in pOperator.Elements)
            {
                sb.Append(element.Accept(this));
            }
            sb.Append(")");
            return sb;
        }

        /// <param name="function">f(x)</param>
        public virtual StringBuilder Visit(Function function)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(function.Name);
            sb.Append("(");
            sb.Append(function.Elements[0].Accept(this));
            for (int i = 1; i < function.Elements.Count; i++)
            {
                sb.Append(",");
                sb.Append(function.Elements[i].Accept(this));
            }
            sb.Append(")");
            return sb;
        }

        /// <param name="increment">d(x)</param>
        public virtual StringBuilder Visit(InfinitesimalIncrement increment)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(increment.Name);
            sb.Append("(");
            sb.Append(increment.Element.Accept(this));
            sb.Append(")");
            return sb;
        }


        public virtual StringBuilder Visit(EqualOperator bOperator)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(bOperator.FirstElement.Accept(this));
            sb.Append(bOperator.Name);
            sb.Append(bOperator.SecondElement.Accept(this));
            return sb;
        }

        public virtual StringBuilder Visit(ExponentiationOperator bOperator)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("(");
            sb.Append(bOperator.FirstElement.Accept(this));
            sb.Append(")");
            sb.Append(bOperator.Name);
            sb.Append(bOperator.SecondElement.Accept(this));
            return sb;
        }

        public virtual StringBuilder Visit(RightPartOfOde rightPartOfOde)
        {
            return rightPartOfOde.DivisionOperator.Accept(this);
        }
    }
}
