using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODEGenerator
{
    class ReactionsList 
    {
        List<Reaction> _fullReactionsList = new List<Reaction>();
        List<Reaction> _reactionsListWithoutDuplicates = new List<Reaction>();

        public void Add(Substance[] interactingSubstances, string rateConstant, Substance[] theResultingSubstances)
        {
            if (theResultingSubstances.Count() == 1)
            {
                Reaction reaction = new Reaction(interactingSubstances,rateConstant,theResultingSubstances[0]);
                _fullReactionsList.Add(reaction);
                _reactionsListWithoutDuplicates.Add(reaction);
            }
            else
            {
                foreach (var resultingElement in theResultingSubstances)
                {
                    _fullReactionsList.Add(new Reaction(interactingSubstances, rateConstant, resultingElement));
                }
                _reactionsListWithoutDuplicates.Add(new Reaction(interactingSubstances, rateConstant, theResultingSubstances[0]));
            }
            
        }

        public StringBuilder GetExpressionOfExpenditure(Substance substance)
        {
            StringBuilder sb = new StringBuilder();


            for (int i = 0; i < _reactionsListWithoutDuplicates.Count; i++)
            {
                if (_reactionsListWithoutDuplicates[i].IsSubstanceContainsInInteractingSubstances(substance))
                {
                    sb.Append("-");
                    sb.Append(_reactionsListWithoutDuplicates[i].GetExpressionOfExpenditure());
                }
            }
            return sb;
        }

        public StringBuilder GetExpressionOfFormation(Substance substance)
        {
            StringBuilder sb = new StringBuilder();
            SubstanceComparer substanceComparer = new SubstanceComparer();
            for (int i = 0; i < _fullReactionsList.Count; i++)
            {
                if (substanceComparer.Equals(substance, _fullReactionsList[i].TheReactionProduct))
                {
                    sb.Append("+");
                    sb.Append(_fullReactionsList[i].GetExpressionOfExpenditure());
                }
            }
            return sb;
        }

    }
}
