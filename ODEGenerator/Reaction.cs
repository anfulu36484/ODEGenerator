using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODEGenerator.Formatter;
using ODEGenerator.SyntaxTree;
using ODEGenerator.SyntaxTree.Numerical;
using ODEGenerator.SyntaxTree.Operators.Multarny;

namespace ODEGenerator
{

    class Reaction
    {
        private List<Substance> _interactingSubstances; 

        private Substance _reactionProduct;

        private Constant _constant;


        public Reaction(Substance[] interactingSubstances, 
                        Constant constant,
                        Substance theReactionProduct)
        {
            _interactingSubstances = interactingSubstances.ToList();
            _constant = constant;
            _reactionProduct = theReactionProduct;
        }


        public List<Substance> InteractingSubstances
        {
            get { return _interactingSubstances; }
        }

        public Constant Constant
        {
            get { return _constant; }
        }

        public Substance TheReactionProduct
        {
            get { return _reactionProduct; }
        }



        public bool IsSubstanceContainsInInteractingSubstances(Substance substance)
        {
            return _interactingSubstances.Contains(substance);
        }


        /// <summary>
        /// Получить выражение расходования
        /// </summary>
        /// <returns></returns>
        public ElementOfSyntaxTree GetExpressionOfExpenditure()
        {
            MultiplicationOperator multiplicationOperator = new MultiplicationOperator();
            multiplicationOperator.AddElement(Constant);
            multiplicationOperator.AddElements(_interactingSubstances);
            return multiplicationOperator;
        }

        
    }
}
