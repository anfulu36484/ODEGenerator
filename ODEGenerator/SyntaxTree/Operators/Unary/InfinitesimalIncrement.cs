using System.Text;
using ODEGenerator.Formatter;


namespace ODEGenerator.SyntaxTree.Operators.Unary
{
    class InfinitesimalIncrement:UnaryOperator
    {
        public InfinitesimalIncrement(ElementOfSyntaxTree elementOfSyntaxTrees)
            : base(elementOfSyntaxTrees, "d")
        {
        }

        public override StringBuilder Accept(IVisitor visitor)
        {
            return visitor.Visit(this);
        }


    }
}
