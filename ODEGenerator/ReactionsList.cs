using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODEGenerator.Formatter;

namespace ODEGenerator
{
    class ReactionsList 
    {
        List<Reaction> _fullReactionsList = new List<Reaction>();
        List<Reaction> _reactionsListWithoutDuplicates = new List<Reaction>();
        private ShellFormatter _shellFormatter;

        public ReactionsList(ShellFormatter shellFormatter)
        {
            _shellFormatter = shellFormatter;
        }


        public void Add(Substance[] interactingSubstances, Constant constant, Substance[] theResultingSubstances)
        {
            if (theResultingSubstances.Count() == 1)
            {
                Reaction reaction = new Reaction(interactingSubstances,constant,theResultingSubstances[0],_shellFormatter);
                _fullReactionsList.Add(reaction);
                _reactionsListWithoutDuplicates.Add(reaction);
            }
            else
            {
                foreach (var resultingElement in theResultingSubstances)
                {
                    _fullReactionsList.Add(new Reaction(interactingSubstances, constant, resultingElement, _shellFormatter));
                }
                _reactionsListWithoutDuplicates.Add(new Reaction(interactingSubstances, constant, theResultingSubstances[0],_shellFormatter));
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
