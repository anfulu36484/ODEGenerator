using System.Collections.Generic;
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
    class MathFormatter :IFormatter
    {


        public StringBuilder Visit(NumericalElement numericalElement)
        {
            return new StringBuilder(numericalElement.Name);
        }


        /// <param name="pOperator">+</param>
        public StringBuilder Visit(PlusOperator pOperator)
        {
            StringBuilder sb = new StringBuilder();
            if (!(pOperator.Elements[0] is MinusOne))
            {
                sb.Append(pOperator.Elements[0].IsNegative() ? "-" : "");
                sb.Append(pOperator.Elements[0].Accept(this));
            }
            for (int i = 1; i < pOperator.Elements.Count(); i++)
            {
                if (!(pOperator.Elements[i] is MinusOne))
                {
                    sb.Append(pOperator.Elements[i].IsNegative() ? "-" : "+");
                    sb.Append(pOperator.Elements[i].Accept(this));
                }
            }
            return sb;
        }

        /// <param name="pOperator">-</param>
        public StringBuilder Visit(MinusOperator pOperator)
        {
            StringBuilder sb = new StringBuilder();
            if (!(pOperator.Elements[0] is MinusOne))
            {
                sb.Append(pOperator.Elements[0].IsNegative() ? "-" : "");
                sb.Append(pOperator.Elements[0].Accept(this));
            }
            for (int i = 1; i < pOperator.Elements.Count(); i++)
            {
                if (!(pOperator.Elements[i] is MinusOne))
                {
                    sb.Append(pOperator.Elements[i].IsNegative() ? "+" : "-");
                    sb.Append(pOperator.Elements[i].Accept(this));
                }
            }
            return sb;
        }

        /// <param name="mOperator">*</param>
        public StringBuilder Visit(MultiplicationOperator mOperator)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(mOperator.Elements[0].Accept(this));
            for (int i = 1; i < mOperator.Elements.Count; i++)
            {
                sb.Append("*");
                sb.Append(mOperator.Elements[i].Accept(this));
            }

            return sb;
        }

        /// <param name="dOperator">/</param>
        public StringBuilder Visit(DivisionOperator dOperator)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(dOperator.Elements[0].Accept(this));
            for (int i = 1; i < dOperator.Elements.Count; i++)
            {
                sb.Append(dOperator.Name);
                sb.Append(dOperator.Elements[i].Accept(this));
            }

            if (dOperator.Elements.Count(n => n is MinusOne) % 2 != 0)
            {
                sb.Insert(0, "(-");
                sb.Append(")");
            }

            return sb;
        }

        /// <param name="pOperator">( )</param>
        public StringBuilder Visit(ParenthesesOperator pOperator)
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
        public StringBuilder Visit(Function function)
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
        public StringBuilder Visit(InfinitesimalIncrement increment)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(increment.Name);
            sb.Append("(");
            sb.Append(increment.Element.Accept(this));
            sb.Append(")");
            return sb;
        }


        public StringBuilder Visit(EqualOperator bOperator)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(bOperator.FirstElement.Accept(this));
            sb.Append(bOperator.Name);
            sb.Append(bOperator.SecondElement.Accept(this));
            return sb;
        }

        public StringBuilder Visit(ExponentiationOperator bOperator)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("(");
            sb.Append(bOperator.FirstElement.Accept(this));
            sb.Append(")");
            sb.Append(bOperator.Name);
            sb.Append(bOperator.SecondElement.Accept(this));
            return sb;
        }

        public StringBuilder Visit(RightPartOfOde rightPartOfOde)
        {
            return rightPartOfOde.Accept(this);
        }
    }
}
