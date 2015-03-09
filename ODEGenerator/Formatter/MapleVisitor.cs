using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ODEGenerator.SyntaxTree.Numerical;

namespace ODEGenerator.Formatter
{
    class MapleVisitor :MathVisitor
    {
        public override StringBuilder Visit(Substance substance)
        {
            StringBuilder sb = new StringBuilder();
            if (substance.GroupOfSubstances != null)
                sb.AppendFormat("{0}[{1}]", substance.GroupOfSubstances.NameOfGroup, substance.GroupID);
            else
                sb.Append(substance.Name);
            return sb;
        }
    }
}
