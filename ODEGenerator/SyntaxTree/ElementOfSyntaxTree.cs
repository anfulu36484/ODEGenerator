using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ODEGenerator.Formatter;
using IFormatter = ODEGenerator.Formatter.IFormatter;

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

        public abstract StringBuilder Accept(IFormatter formatter);

        public abstract bool IsNegative();

    }
}
