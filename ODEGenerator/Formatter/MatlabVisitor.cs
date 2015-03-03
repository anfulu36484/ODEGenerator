using System;
using System.Collections.Generic;
using System.Text;
using ODEGenerator.SyntaxTree;
using ODEGenerator.SyntaxTree.Numerical;
using ODEGenerator.SyntaxTree.Operators.Binary;
using ODEGenerator.SyntaxTree.Operators.Multarny;
using ODEGenerator.SyntaxTree.Operators.Unary;

namespace ODEGenerator.Formatter
{
    class MatlabVisitor :MathVisitor
    {

        private string nameOfinputArray = "y";
        private string nameOfoutputArray = "out";

        public string NameOfinputArray
        {
            get { return nameOfinputArray; }
            set { nameOfinputArray = value; }
        }

        public string NameOfoutputArray
        {
            get { return nameOfoutputArray; }
            set { nameOfoutputArray = value; }
        }

        public override StringBuilder Visit(Substance substance)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}({1})", nameOfinputArray, substance.ODEId);
            return sb;
        }

        public override StringBuilder Visit(RightPartOfOde rightPartOfOde)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}({1})",nameOfoutputArray,(rightPartOfOde.FirstElement as Substance).ODEId);
            return sb;
        }
    }
}
