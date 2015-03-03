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
    class MatlabFormatter :IFormatter
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


        public StringBuilder Visit(NumericalElement numericalElement)
        {
            throw new NotImplementedException();
        }

        public StringBuilder Visit(PlusOperator pOperator)
        {
            throw new NotImplementedException();
        }

        public StringBuilder Visit(MinusOperator mOperator)
        {
            throw new NotImplementedException();
        }

        public StringBuilder Visit(MultiplicationOperator mOperator)
        {
            throw new NotImplementedException();
        }

        public StringBuilder Visit(DivisionOperator dOperator)
        {
            throw new NotImplementedException();
        }

        public StringBuilder Visit(ParenthesesOperator pOperator)
        {
            throw new NotImplementedException();
        }

        public StringBuilder Visit(Function function)
        {
            throw new NotImplementedException();
        }

        public StringBuilder Visit(InfinitesimalIncrement increment)
        {
            throw new NotImplementedException();
        }

        public StringBuilder Visit(EqualOperator bOperator)
        {
            throw new NotImplementedException();
        }

        public StringBuilder Visit(ExponentiationOperator bOperator)
        {
            throw new NotImplementedException();
        }

        public StringBuilder Visit(RightPartOfOde rightPartOfOde)
        {
            throw new NotImplementedException();
        }

        public StringBuilder Visit(BinaryOperator bOperator)
        {
            throw new NotImplementedException();
        }
    }
}
