using System.Text;
using ODEGenerator.Formatter;

namespace ODEGenerator.SyntaxTree.Numerical
{
    public abstract class NumericalElement:ElementOfSyntaxTree
    {
        private double _value;

        protected NumericalElement(string name, double value) : base(name)
        {
            _value = value;
        }

        public double Value
        {
            get { return _value; }
            set { _value = value; }
        }


    }

}
