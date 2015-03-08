using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODEGenerator.SyntaxTree
{
    class ListOfSyntaxTrees :IEnumerable<ElementOfSyntaxTree>
    {
        List<ElementOfSyntaxTree> _list = new List<ElementOfSyntaxTree>(); 

        public IEnumerator<ElementOfSyntaxTree> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(ElementOfSyntaxTree elementOfSyntaxTree)
        {
            _list.Add(elementOfSyntaxTree);
        }
    }
}
