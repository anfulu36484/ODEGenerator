using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODEGenerator
{
    class Substance
    {
        public readonly string NameOfSubstance;

        public double InitialValue;

        private GroupOfSubstances _groupOfSubstances;

        public Substance(string nameOfSubstance, double initialValue)
        {
            NameOfSubstance = nameOfSubstance;
            InitialValue = initialValue;
        }

        public Substance(string nameOfSubstance, double initialValue, GroupOfSubstances groupOfSubstances)
            : this(nameOfSubstance, initialValue)
        {
            GroupOfSubstances = groupOfSubstances;
        }

        private int odeId = -1;

        public int ODEId
        {
            get { return odeId; }
            set { odeId = value; }
        }

        public GroupOfSubstances GroupOfSubstances
        {
            get { return _groupOfSubstances; }
            set { _groupOfSubstances = value; }
        }
    }
    
}
