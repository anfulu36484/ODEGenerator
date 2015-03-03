using System.Collections.Generic;
using System.Linq;
using ODEGenerator.SyntaxTree;
using ODEGenerator.SyntaxTree.Numerical;
using ODEGenerator.SyntaxTree.Operators.Multarny;

namespace ODEGenerator
{
    class ReactionsList 
    {
        List<Reaction> _fullReactionsList = new List<Reaction>();
        List<Reaction> _reactionsListWithoutDuplicates = new List<Reaction>();

        public void Add(Substance[] interactingSubstances, Constant constant, Substance[] theResultingSubstances)
        {
            if (theResultingSubstances.Count() == 1)
            {
                Reaction reaction = new Reaction(interactingSubstances,constant,theResultingSubstances[0]);
                _fullReactionsList.Add(reaction);
                _reactionsListWithoutDuplicates.Add(reaction);
            }
            else
            {
                foreach (var resultingElement in theResultingSubstances)
                {
                    _fullReactionsList.Add(new Reaction(interactingSubstances, constant, resultingElement));
                }
                _reactionsListWithoutDuplicates.Add(new Reaction(interactingSubstances, constant, theResultingSubstances[0]));
            }
            
        }

        enum TypeOfExpression
        {
            Expenditure, Formation
        }


        ElementOfSyntaxTree GetExpression(Substance substance,TypeOfExpression typeOfExpression)
        {
            

            List<MultiplicationOperator> multiplicationOperatorsList = new List<MultiplicationOperator>();

            for (int i = 0; i < _reactionsListWithoutDuplicates.Count; i++)
            {
                if (_reactionsListWithoutDuplicates[i].IsSubstanceContainsInInteractingSubstances(substance))
                {
                    MultiplicationOperator multiplicationOperator = new MultiplicationOperator();
                    if(typeOfExpression==TypeOfExpression.Expenditure)
                        multiplicationOperator.AddElement(new MinusOne());
                    multiplicationOperator.AddElement(_reactionsListWithoutDuplicates[i].GetExpressionOfExpenditure());
                    multiplicationOperatorsList.Add(multiplicationOperator);
                }
            }

            if (multiplicationOperatorsList.Count > 1)
            {
                PlusOperator plusOperator = new PlusOperator();
                plusOperator.AddElements(multiplicationOperatorsList);
                return plusOperator;
            }
            if (multiplicationOperatorsList.Count == 1)
                return multiplicationOperatorsList[0];
            return null;
        }


        public ElementOfSyntaxTree GetExpressionOfExpenditure(Substance substance)
        {
            List<MultiplicationOperator> multiplicationOperatorsList = new List<MultiplicationOperator>();

            for (int i = 0; i < _reactionsListWithoutDuplicates.Count; i++)
            {
                if (_reactionsListWithoutDuplicates[i].IsSubstanceContainsInInteractingSubstances(substance))
                {
                    MultiplicationOperator multiplicationOperator = new MultiplicationOperator();
                    multiplicationOperator.AddElement(new MinusOne());
                    multiplicationOperator.AddElement(_reactionsListWithoutDuplicates[i].GetExpressionOfExpenditure());
                    multiplicationOperatorsList.Add(multiplicationOperator);
                }
            }

            if (multiplicationOperatorsList.Count > 1)
            {
                PlusOperator plusOperator = new PlusOperator();
                plusOperator.AddElements(multiplicationOperatorsList);
                return plusOperator;
            }
            if (multiplicationOperatorsList.Count == 1)
                return multiplicationOperatorsList[0];
            return null;
        }

        SubstanceComparer _substanceComparer = new SubstanceComparer();

        public ElementOfSyntaxTree GetExpressionOfFormation(Substance substance)
        {
            List<MultiplicationOperator> multiplicationOperatorsList = new List<MultiplicationOperator>();

            for (int i = 0; i < _fullReactionsList.Count; i++)
            {
                if (_substanceComparer.Equals(substance,_fullReactionsList[i].TheReactionProduct))
                {
                    MultiplicationOperator multiplicationOperator = new MultiplicationOperator();
                    multiplicationOperator.AddElement(_fullReactionsList[i].GetExpressionOfExpenditure());
                    multiplicationOperatorsList.Add(multiplicationOperator);
                }
            }

            if (multiplicationOperatorsList.Count > 1)
            {
                PlusOperator plusOperator = new PlusOperator();
                plusOperator.AddElements(multiplicationOperatorsList);
                return plusOperator;
            }
            if (multiplicationOperatorsList.Count == 1)
                return multiplicationOperatorsList[0];
            return null;
        }

    }
}
