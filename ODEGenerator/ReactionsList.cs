using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODEGenerator
{
    class ReactionsList :IEnumerable<Reaction>
    {
        List<Reaction> _reactionsList = new List<Reaction>();

        public IEnumerator<Reaction> GetEnumerator()
        {
            return _reactionsList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(Reaction reaction)
        {
            _reactionsList.Add(reaction);
        }

        public StringBuilder GetExpressionOfExpenditure(Substance substance)
        {
            StringBuilder sb = new StringBuilder();
            

            for (int i = 0; i < _reactionsList.Count; i++)
            {
                if (_reactionsList[i].IsSubstanceContainsInInteractingSubstances(substance))
                {
                    sb.Append("-");
                    sb.Append(_reactionsList[i].GetExpressionOfExpenditure());
                }
            }
            return sb;
        }

        public StringBuilder GetExpressionOfFormation(Substance substance)
        {
            StringBuilder sb = new StringBuilder();
            SubstanceComparer substanceComparer = new SubstanceComparer();
            for (int i = 0; i < _reactionsList.Count; i++)
            {
                if (substanceComparer.Equals(substance, _reactionsList[i].TheReactionProduct))
                {
                    sb.Append("+");
                    sb.Append(_reactionsList[i].GetExpressionOfExpenditure());
                }
            }
            return sb;
        }

    }
}
