using System.Collections.Generic;
using System.Linq;
using System.Text;
using ODEGenerator.Formatter;

namespace ODEGenerator.SyntaxTree.Operators.Multarny
{
    abstract class MultarnyOperator :OperatorElement
    {
        readonly List<ElementOfSyntaxTree> _elements = new List<ElementOfSyntaxTree>();

        protected MultarnyOperator(string name) : base(name)
        {
        }

        public List<ElementOfSyntaxTree> Elements
        {
            get { return _elements; }
        }

        public void AddElement(ElementOfSyntaxTree elementOfSyntaxTree)
        {
            if (elementOfSyntaxTree!=null)
                _elements.Add(elementOfSyntaxTree);
        }

        public void AddElements(params ElementOfSyntaxTree[] elementOfSyntaxTree)
        {
            var newElements = elementOfSyntaxTree.Where(n => n != null);
            if(newElements.Count()!=0)
                _elements.AddRange(newElements);
        }

        public void AddElements(IEnumerable<ElementOfSyntaxTree> elementOfSyntaxTree)
        {
            var newElements = elementOfSyntaxTree.Where(n => n != null);
            if (newElements.Count() != 0)
                _elements.AddRange(elementOfSyntaxTree);
        }

    }
}
