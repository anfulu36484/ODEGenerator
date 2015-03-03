namespace ODEGenerator.SyntaxTree.Operators.Binary
{
    abstract class BinaryOperator :OperatorElement
    {
        private ElementOfSyntaxTree _firstElement;
        private ElementOfSyntaxTree _secondElement;

        protected BinaryOperator(string name, ElementOfSyntaxTree firstElement, ElementOfSyntaxTree secondElement) 
            : base(name)
        {
            _firstElement = firstElement;
            _secondElement = secondElement;
        }

        public ElementOfSyntaxTree FirstElement
        {
            get { return _firstElement; }
        }

        public ElementOfSyntaxTree SecondElement
        {
            get { return _secondElement; }
        }

        public override bool IsNegative()
        {
            return false;
        }
    }
}
