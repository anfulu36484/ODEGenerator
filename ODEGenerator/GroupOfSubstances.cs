using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODEGenerator.SyntaxTree.Numerical;

namespace ODEGenerator
{
    class GroupOfSubstances
    {
        public readonly string NameOfGroup;

        private SortedList<int, Substance> _substances = new SortedList<int, Substance>();

        public GroupOfSubstances(string nameOfGroup)
        {
            NameOfGroup = nameOfGroup;
        }

        public SortedList<int, Substance> Substances
        {
            get { return _substances; }
        }

        public Substance CreateSubstance(double initialValue, int groupId)
        {
            Substance substance = new Substance(NameOfGroup+groupId,initialValue,this);
            substance.GroupID = groupId;
            _substances.Add(groupId, substance);
            return substance;
        }


        public Substance this[int index]
        {
            get { return _substances[index]; }
        }

        public int Count { get { return _substances.Count; } }
    }
}
