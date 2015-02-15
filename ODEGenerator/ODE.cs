using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODEGenerator
{
    class ODE
    {
        List<Substance> _substances = new List<Substance>();

        ReactionsList _reactions = new ReactionsList();

        #region Добавление новых элементов и их взаимодействий

        SubstanceComparer _substanceComparer = new SubstanceComparer();

        void AddNewElements(Substance[] interactingSubstances, Substance[] theResultingSubstances)
        {
            var newElements = interactingSubstances
                              .Concat(theResultingSubstances)
                              .Where(n =>!_substances.Contains(n, _substanceComparer));
            if(newElements.Count()>0)
                _substances.AddRange(newElements);
        }

        /// <summary>
        /// Добавить новое выражение.
        /// Пример: А->B
        /// </summary>
        /// <param name="interactingSubstances">A - исходный элемент</param>
        /// <param name="rateConstant">k - контстанта скорости</param>
        /// <param name="theResultingSubstance">С - элемент, образовавшийся из элемента А</param>
        public void Add(Substance interactingSubstances, string rateConstant, Substance theResultingSubstance)
        {
            _reactions.Add(new Reaction(new []{interactingSubstances}, rateConstant, theResultingSubstance));
            AddNewElements(new []{interactingSubstances}, new[] { theResultingSubstance });
        }


        /// <summary>
        /// Добавить новое выражение.
        /// Пример: А+B->C
        /// </summary>
        /// <param name="interactingSubstances">A,B - взаимодействующие элементы</param>
        /// <param name="rateConstant">k - контстанта скорости</param>
        /// <param name="theResultingSubstance">С - продукт взаимодействия элементов А и В</param>
        public void Add(Substance[] interactingSubstances, string rateConstant, Substance theResultingSubstance)
        {
            _reactions.Add(new Reaction(interactingSubstances,rateConstant,theResultingSubstance));
            AddNewElements(interactingSubstances, new[] {theResultingSubstance});
        }

        /// <summary>
        /// Добавить новое выражение.
        /// Пример: А+B->C+D
        /// </summary>
        /// <param name="interactingSubstances">A,B - взаимодействующие элементы</param>
        /// <param name="rateConstant">k - контстанта скорости</param>
        /// <param name="theResultingSubstances">С, D - продукты взаимодействия элементов А и В</param>
        public void Add(Substance[] interactingSubstances, string rateConstant, Substance[] theResultingSubstances)
        {
            foreach (var resultingElement in theResultingSubstances)
            {
                _reactions.Add(new Reaction(interactingSubstances,rateConstant,resultingElement));
            }
            AddNewElements(interactingSubstances, theResultingSubstances);
        }

        #endregion


        List<StringBuilder> CreateExpressionsOfExpenditure()
        {
            List<StringBuilder> differentialEquations = new List<StringBuilder>();
            foreach (var substance in _substances)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(string.Format("d{0}/dt = ", substance.NameOfSubstance));
                sb.Append(_reactions.GetExpressionOfExpenditure(substance));
                sb.Append(_reactions.GetExpressionOfFormation(substance));
                differentialEquations.Add(sb);
            }
            return differentialEquations;
        }




        public void PrintResult()
        {
            List<StringBuilder> result = CreateExpressionsOfExpenditure();
            foreach (var stringBuilder in result)
            {
                Console.WriteLine(stringBuilder.ToString());
            }
        }

    }

}
