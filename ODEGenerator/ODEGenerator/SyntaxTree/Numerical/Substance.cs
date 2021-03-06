﻿using System.Text;
using ODEGenerator.Formatter;

namespace ODEGenerator.SyntaxTree.Numerical
{
    public class Substance :NumericalElement
    {

        private readonly GroupOfSubstances _groupOfSubstances;

        private int odeId = -1;

        private int groupId = -1;

        public Substance(string nameOfSubstance, double initialValue)
            : base(nameOfSubstance, initialValue)
        {
        }

        public Substance(string nameOfSubstance, double initialValue, GroupOfSubstances groupOfSubstances)
            : base(nameOfSubstance, initialValue)
        {
            _groupOfSubstances = groupOfSubstances;
        }

        public int ODEId
        {
            get { return odeId; }
            set { odeId = value; }
        }

        public int GroupID
        {
            get { return groupId; }
            set { groupId = value; }
        }

        public GroupOfSubstances GroupOfSubstances
        {
            get { return _groupOfSubstances; }
        }


        public override StringBuilder Accept(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
    
}
