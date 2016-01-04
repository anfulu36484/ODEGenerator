using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ODEGenerator.SyntaxTree.Numerical;
using ODEGenerator.SyntaxTree.Operators.Binary;

namespace ODEGenerator.Formatter
{
    class CsharpVisitor:ProgrammingLanguageVisitor
    {

        public CsharpVisitor()
        {
            nameOfinputArray = "y";
            nameOfoutputArray = "output";
        }

        public override StringBuilder Visit(Substance substance)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}[{1}]", nameOfinputArray, substance.ODEId-1);
            return sb;
        }

        public override StringBuilder Visit(RightPartOfOde rightPartOfOde)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}[{1}]", nameOfoutputArray, (rightPartOfOde.FirstElement as Substance).ODEId-1);
            return sb;
        }

        public override StringBuilder Visit(ExponentiationOperator bOperator)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Math.Pow({0},{1})", bOperator.FirstElement.Accept(this),
                bOperator.SecondElement.Accept(this));
            return sb;
        }
    }
}
