using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODEGenerator.Formatter;

namespace ODEGenerator.SyntaxTree.Operators.Unary
{
    abstract class UnaryOperator :OperatorElement
    {
        private ElementOfSyntaxTree _element;

        protected UnaryOperator(ElementOfSyntaxTree element,string name) : base(name)
        {
            _element = element;
        }

        public ElementOfSyntaxTree Element
        {
            get { return _element; }
        }
    }
}
