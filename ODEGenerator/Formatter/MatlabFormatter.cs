using System;
using System.Collections.Generic;
using System.Text;

namespace ODEGenerator.Formatter
{
    class MatlabFormatter :IFormatter
    {

        private string nameOfinputArray = "y";
        private string nameOfoutputArray = "out";

        public string NameOfinputArray
        {
            get { return nameOfinputArray; }
            set { nameOfinputArray = value; }
        }

        public string NameOfoutputArray
        {
            get { return nameOfoutputArray; }
            set { nameOfoutputArray = value; }
        }

        public StringBuilder GetExpressionOfExpenditure(Reaction reaction)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(reaction.Constant.NameOfRateConstant);
            sb.Append("*");
            sb.Append(string.Format("{0}({1})",NameOfinputArray,reaction.InteractingSubstances[0].ODEId));

            for (int i = 1; i < reaction.InteractingSubstances.Count; i++)
            {
                sb.Append("*");
                sb.Append(string.Format("{0}({1})",NameOfinputArray,reaction.InteractingSubstances[i].ODEId));
            }
            return sb;
        }

        public List<StringBuilder> CreateExpressions(ODE ode)
        {
            List<StringBuilder> differentialEquations = new List<StringBuilder>();
            foreach (var substance in ode.Substances)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(string.Format("{0}({1}) = ",NameOfoutputArray, substance.ODEId));
                sb.Append(ode.Reactions.GetExpressionOfExpenditure(substance));
                sb.Append(ode.Reactions.GetExpressionOfFormation(substance));
                sb.Append(";");
                differentialEquations.Add(sb);
            }
            return differentialEquations;
        }
    }
}
