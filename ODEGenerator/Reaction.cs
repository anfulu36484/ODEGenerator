using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODEGenerator
{

    class Reaction
    {
        private List<Substance> _interactingSubstances; 

        private Substance _reactionProduct;

        private string _rateConstant;


        public Reaction(Substance[] interactingSubstances, string rateConstant, Substance theReactionProduct)
        {
            _interactingSubstances = interactingSubstances.ToList();
            _rateConstant = rateConstant;
            _reactionProduct = theReactionProduct;
        }


        public List<Substance> InteractingSubstances
        {
            get { return _interactingSubstances; }
        }

        public string RateConstant
        {
            get { return _rateConstant; }
        }

        public Substance TheReactionProduct
        {
            get { return _reactionProduct; }
        }

        public override int GetHashCode()
        {
            for (int i = 0; i < _interactingSubstances.Count; i++)
            {
                
            }
            return 
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
            StringBuilder sb = new StringBuilder();
            sb.Append(RateConstant);
            sb.Append("*");
            sb.Append(_interactingSubstances[0].NameOfSubstance);

            for (int i = 1; i < _interactingSubstances.Count; i++)
            {
                sb.Append("*");
                sb.Append(_interactingSubstances[i].NameOfSubstance);
            }
            return sb;
        }


    }
}
