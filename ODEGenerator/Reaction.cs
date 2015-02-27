using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODEGenerator.Formatter;

namespace ODEGenerator
{

    class Reaction
    {
        private List<Substance> _interactingSubstances; 

        private Substance _reactionProduct;

        private Constant _constant;

        private ShellFormatter _shellFormatter;


        public Reaction(Substance[] interactingSubstances, 
                        Constant constant,
                        Substance theReactionProduct,
                        ShellFormatter shellFormatter)
        {
            _interactingSubstances = interactingSubstances.ToList();
            _constant = constant;
            _reactionProduct = theReactionProduct;
            _shellFormatter = shellFormatter;
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
        public StringBuilder GetExpressionOfExpenditure()
        {
            return _shellFormatter.GetExpressionOfExpenditure(this);
        }


    }
}
