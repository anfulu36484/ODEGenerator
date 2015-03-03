using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ODEGenerator.Formatter;

namespace ODEGenerator.SyntaxTree
{
    abstract class ElementOfSyntaxTree
    {
        private readonly string _name;


        protected ElementOfSyntaxTree(string name)
        {
            _name = name;
        }


        public string Name
        {
            get { return _name; }
        }

        public override int GetHashCode()
        {
            return _name.GetHashCode();
        }

        public abstract StringBuilder Accept(IVisitor visitor);


    }
}
