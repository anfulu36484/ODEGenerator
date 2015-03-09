using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODEGenerator.Formatter;
using ODEGenerator.SyntaxTree;
using ODEGenerator.SyntaxTree.Numerical;
using ODEGenerator.SyntaxTree.Operators.Binary;
using ODEGenerator.SyntaxTree.Operators.Multarny;
using ODEGenerator.SyntaxTree.Operators.Unary;
using System.IO;

namespace ODEGenerator
{
    class ODEs
    {
        private List<Substance> _substances = new List<Substance>();

        private List<Constant> _rateConstants = new List<Constant>(); 

        private ReactionsList _reactions = new ReactionsList();


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


        void AddReaction(Substance[] interactingSubstances, Constant constant, Substance[] theResultingSubstance)
        {
            Reactions.Add(interactingSubstances, constant, theResultingSubstance);
            AddNewSubstances(interactingSubstances, theResultingSubstance);
            AddNewRateConstant(constant);
        }

        /// <summary>
        /// Добавить новую реакцию, пример А->B
        /// </summary>
        /// <param name="initialSubstance">Исходное вещество (А)</param>
        /// <param name="constant">k - контстанта скорости реакции</param>
        /// <param name="resultingSubstance">Продукт реакции (B)</param>
        public void Add(Substance initialSubstance, Constant constant, Substance resultingSubstance)
        {
            AddReaction(new[] { initialSubstance }, constant, new[] { resultingSubstance });
        }

        /// <summary>
        /// Добавить новую реакцию, пример А->B+С
        /// </summary>
        /// <param name="initialSubstance">Исходное вещество (А)</param>
        /// <param name="constant">k - контстанта скорости реакции</param>
        /// <param name="firstResultingSubstance">Первый продукт реакции (B)</param>
        /// <param name="secondResultingSubstance">Второй продукт реакции (С)</param>
        public void Add(Substance initialSubstance, 
                        Constant constant, 
                        Substance firstResultingSubstance,
                        Substance secondResultingSubstance)
        {
            AddReaction(new[] { initialSubstance }, constant, new[] { firstResultingSubstance, secondResultingSubstance });
        }

        /// <summary>
        /// Добавить новую реакцию, пример А->B+С+D
        /// </summary>
        /// <param name="initialSubstance">Исходное вещество (А)</param>
        /// <param name="constant">k - контстанта скорости реакции</param>
        /// <param name="firstResultingSubstance">Первый продукт реакции (B)</param>
        /// <param name="secondResultingSubstance">Второй продукт реакции (С)</param>
        /// <param name="thirdReactiveSubstance">Третий продукт реакции (D)</param>
        public void Add(Substance initialSubstance,
                        Constant constant,
                        Substance firstResultingSubstance,
                        Substance secondResultingSubstance,
                        Substance thirdReactiveSubstance)
        {
            AddReaction(new[] { initialSubstance }, constant, new[] { firstResultingSubstance, secondResultingSubstance, thirdReactiveSubstance });
        }


        /// <summary>
        /// Добавить новую реакцию, пример: А+B->C
        /// </summary>
        /// <param name="firstReactiveSubstance">Первое вещество, вступающее в реакцию (А)</param>
        /// <param name="secondReactiveSubstance">Второе вещество, вступающее в реакцию (B)</param>
        /// <param name="constant">k - контстанта скорости реакции</param>
        /// <param name="theResultingSubstance">Продукт реакции (С)</param>
        public void Add(Substance firstReactiveSubstance,
                        Substance secondReactiveSubstance,
                        Constant constant,
                        Substance theResultingSubstance)
        {
            AddReaction(new[] { firstReactiveSubstance, secondReactiveSubstance }, constant, new[] { theResultingSubstance });
        }

        /// <summary>
        /// Добавить новую реакцию, пример: А+B->C+D
        /// </summary>
        /// <param name="firstReactiveSubstance">Первое вещество, вступающее в реакцию (А)</param>
        /// <param name="secondReactiveSubstance">Второе вещество, вступающее в реакцию (B)</param>
        /// <param name="constant">k - контстанта скорости реакции</param>
        /// <param name="firstResultingSubstance">Первый продукт реакции (C)</param>
        /// <param name="secondResultingSubstance">Второй продукт реакции (D)</param>
        public void Add(Substance firstReactiveSubstance,
                        Substance secondReactiveSubstance,
                        Constant constant,
                        Substance firstResultingSubstance,
                        Substance secondResultingSubstance)
        {
            AddReaction(new[] { firstReactiveSubstance, secondReactiveSubstance }, constant,
                new[] { firstResultingSubstance, secondResultingSubstance });
        }

        /// <summary>
        /// Добавить новую реакцию, пример: А+B->C+D+E
        /// </summary>
        /// <param name="firstReactiveSubstance">Первое вещество, вступающее в реакцию (А)</param>
        /// <param name="secondReactiveSubstance">Второе вещество, вступающее в реакцию (B)</param>
        /// <param name="constant">k - контстанта скорости реакции</param>
        /// <param name="firstResultingSubstance">Первый продукт реакции (C)</param>
        /// <param name="secondResultingSubstance">Второй продукт реакции (D)</param>
        /// <param name="thirdResultingSubstance">Третий продукт реакции (E)</param>
        public void Add(Substance firstReactiveSubstance,
                        Substance secondReactiveSubstance,
                        Constant constant,
                        Substance firstResultingSubstance,
                        Substance secondResultingSubstance,
                        Substance thirdResultingSubstance)
        {
            AddReaction(new[] { firstReactiveSubstance, secondReactiveSubstance },
                constant, new[] { firstResultingSubstance, secondResultingSubstance, thirdResultingSubstance });
        }

        /// <summary>
        /// Добавить новую реакцию, пример: А+B+С->D
        /// </summary>
        /// <param name="firstReactiveSubstance">Первое вещество, вступающее в реакцию (А)</param>
        /// <param name="secondReactiveSubstance">Второе вещество, вступающее в реакцию (B)</param>
        /// <param name="thirdReactiveSubstance">Третье вещество, вступающее в реакцию (С)</param>
        /// <param name="constant">k - контстанта скорости реакции</param>
        /// <param name="theResultingSubstance">Продукт реакции (D)</param>
        public void Add(Substance firstReactiveSubstance,
                        Substance secondReactiveSubstance,
                        Substance thirdReactiveSubstance,
                        Constant constant,
                        Substance theResultingSubstance)
        {
            AddReaction(new[] { firstReactiveSubstance, secondReactiveSubstance,thirdReactiveSubstance },
                constant, new[] { theResultingSubstance });
        }

        /// <summary>
        /// Добавить новую реакцию, пример: А+B+С->D+E
        /// </summary>
        /// <param name="firstReactiveSubstance">Первое вещество, вступающее в реакцию (А)</param>
        /// <param name="secondReactiveSubstance">Второе вещество, вступающее в реакцию (B)</param>
        /// <param name="thirdReactiveSubstance">Третье вещество, вступающее в реакцию (С)</param>
        /// <param name="constant">k - контстанта скорости реакции</param>
        /// <param name="firstResultingSubstance">Первый продукт реакции (D)</param>
        /// <param name="secondResultingSubstance">Второй продукт реакции (E)</param>
        public void Add(Substance firstReactiveSubstance,
                        Substance secondReactiveSubstance,
                        Substance thirdReactiveSubstance,
                        Constant constant,
                        Substance firstResultingSubstance,
                        Substance secondResultingSubstance)
        {
            AddReaction(new[] { firstReactiveSubstance, secondReactiveSubstance, thirdReactiveSubstance },
                constant, new[] { firstResultingSubstance, secondResultingSubstance });
        }

        /// <summary>
        /// Добавить новую реакцию, пример: А+B+С->D+E+F
        /// </summary>
        /// <param name="firstReactiveSubstance">Первое вещество, вступающее в реакцию (А)</param>
        /// <param name="secondReactiveSubstance">Второе вещество, вступающее в реакцию (B)</param>
        /// <param name="thirdReactiveSubstance">Третье вещество, вступающее в реакцию (С)</param>
        /// <param name="constant">k - контстанта скорости реакции</param>
        /// <param name="firstResultingSubstance">Первый продукт реакции (D)</param>
        /// <param name="secondResultingSubstance">Второй продукт реакции (E)</param>
        /// <param name="thirdResultingSubstance">Третий продукт реакции (F)</param>
        public void Add(Substance firstReactiveSubstance,
                        Substance secondReactiveSubstance,
                        Substance thirdReactiveSubstance,
                        Constant constant,
                        Substance firstResultingSubstance,
                        Substance secondResultingSubstance, 
                        Substance thirdResultingSubstance)
        {
            AddReaction(new[] { firstReactiveSubstance, secondReactiveSubstance, thirdReactiveSubstance },
                constant, new[] { firstResultingSubstance, secondResultingSubstance, thirdResultingSubstance });
        }



        #endregion


        public List<ElementOfSyntaxTree> CreateExpressions()
        {
            List<ElementOfSyntaxTree> differentialEquations = new List<ElementOfSyntaxTree>();

            Constant t = new Constant("t",0);

            foreach (var substance in Substances)
            {
                //Правая часть уравнения
                RightPartOfOde rightPartOfOde = new RightPartOfOde(substance,t);


                //Левая часть уравнения
                PlusOperator plusOperator = new PlusOperator();
                plusOperator.AddElements(Reactions.GetExpressionOfExpenditure(substance),
                                        Reactions.GetExpressionOfFormation(substance));

                
                differentialEquations.Add(new EqualOperator(rightPartOfOde,plusOperator));

            }
            return differentialEquations;
        }


        public void PrintResult(IVisitor visitor)
        {
            List<ElementOfSyntaxTree> result = CreateExpressions();
            foreach (var n in result)
            {
                Console.WriteLine(n.Accept(visitor));
            }
        }

        public void SaveResult(string fileName, IVisitor visitor)
        {
            List<ElementOfSyntaxTree> result = CreateExpressions();
            using (StreamWriter sw = new StreamWriter(fileName))
            {
                foreach (var n in result)
                {
                    sw.WriteLine(n.Accept(visitor));
                }
            }
            
        }

    }

}
