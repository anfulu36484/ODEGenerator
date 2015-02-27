using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODEGenerator.Formatter;

namespace ODEGenerator
{
    class ODE
    {
        private List<Substance> _substances = new List<Substance>();

        private List<Constant> _rateConstants = new List<Constant>(); 

        private ReactionsList _reactions;


        private ShellFormatter _shellFormatter;

        public ODE()
        {
            _shellFormatter = new ShellFormatter(new MathFormatter());
            _reactions = new ReactionsList(_shellFormatter);
        }


        public void ResetOutputFormat(IFormatter formatter)
        {
            _shellFormatter.ResetFormatter(formatter);
        }


        #region Добавление новых реакций

        SubstanceComparer _substanceComparer = new SubstanceComparer();

        public List<Substance> Substances
        {
            get { return _substances; }
        }

        public ReactionsList Reactions
        {
            get { return _reactions; }
        }

        public List<Constant> RateConstants
        {
            get { return _rateConstants; }
        }


        private void SetID(Substance substance)
        {
            substance.ODEId = Substances.Count + 1;

        }

        void AddNewSubstance(Substance substance)
        {
            if (!Substances.Contains(substance, _substanceComparer))
            {
                SetID(substance);
                _substances.Add(substance);
            }
        }

        void AddNewSubstances(Substance[] interactingSubstances, Substance[] theResultingSubstances)
        {
            foreach (var substance in interactingSubstances.Concat(theResultingSubstances))
            {
                AddNewSubstance(substance);
            }
        }


        void AddNewRateConstant(Constant constant)
        {
            if (!_rateConstants.Contains(constant))
                _rateConstants.Add(constant);
        }

        

        /// <summary>
        /// Добавить новую реакцию
        /// Пример: А->B
        /// </summary>
        /// <param name="interactingSubstances">A - исходный элемент</param>
        /// <param name="constant">k - контстанта скорости</param>
        /// <param name="theResultingSubstance">С - элемент, образовавшийся из элемента А</param>
        public void Add(Substance interactingSubstances, Constant constant, Substance theResultingSubstance)
        {
            Reactions.Add(new []{interactingSubstances}, constant, new []{theResultingSubstance});
            AddNewSubstances(new []{interactingSubstances}, new[] { theResultingSubstance });
            AddNewRateConstant(constant);
        }


        /// <summary>
        /// Добавить новую реакцию
        /// Пример: А+B->C
        /// </summary>
        /// <param name="interactingSubstances">A,B - взаимодействующие элементы</param>
        /// <param name="constant">k - контстанта скорости</param>
        /// <param name="theResultingSubstance">С - продукт взаимодействия элементов А и В</param>
        public void Add(Substance[] interactingSubstances, Constant constant, Substance theResultingSubstance)
        {
            Reactions.Add(interactingSubstances,constant,new []{theResultingSubstance});
            AddNewSubstances(interactingSubstances, new[] {theResultingSubstance});
            AddNewRateConstant(constant);
        }

        /// <summary>
        /// Добавить новую реакцию
        /// Пример: А+B->C+D
        /// </summary>
        /// <param name="interactingSubstances">A,B - взаимодействующие элементы</param>
        /// <param name="constant">k - контстанта скорости</param>
        /// <param name="theResultingSubstances">С, D - продукты взаимодействия элементов А и В</param>
        public void Add(Substance[] interactingSubstances, Constant constant, Substance[] theResultingSubstances)
        {
            Reactions.Add(interactingSubstances, constant, theResultingSubstances);
            AddNewSubstances(interactingSubstances, theResultingSubstances);
            AddNewRateConstant(constant);
        }

        /// <summary>
        /// Добавить новую реакцию
        /// Пример: А->B+C
        /// </summary>
        /// <param name="interactingSubstances">A,B - взаимодействующие элементы</param>
        /// <param name="constant">k - контстанта скорости</param>
        /// <param name="theResultingSubstances">С, D - продукты взаимодействия элементов А и В</param>
        public void Add(Substance interactingSubstances, Constant constant, Substance[] theResultingSubstances)
        {
            Reactions.Add(new []{interactingSubstances}, constant, theResultingSubstances);
            AddNewSubstances(new []{interactingSubstances}, theResultingSubstances);
            AddNewRateConstant(constant);
        }


        #endregion


        


        public List<StringBuilder> CreateExpressions()
        {
            return _shellFormatter.CreateExpressions(this);
        }


        public void PrintResult()
        {
            List<StringBuilder> result = CreateExpressions();
            foreach (var stringBuilder in result)
            {
                Console.WriteLine(stringBuilder.ToString());
            }
        }

    }

}
