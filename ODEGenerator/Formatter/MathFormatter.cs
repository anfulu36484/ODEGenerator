using System.Collections.Generic;
using System.Text;

namespace ODEGenerator.Formatter
{
    class MathFormatter :IFormatter
    {

        public StringBuilder GetExpressionOfExpenditure(Reaction reaction)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(reaction.Constant.NameOfRateConstant);
            sb.Append("*");
            sb.Append(reaction.InteractingSubstances[0].NameOfSubstance);

            for (int i = 1; i < reaction.InteractingSubstances.Count; i++)
            {
                sb.Append("*");
                sb.Append(reaction.InteractingSubstances[i].NameOfSubstance);
            }
            return sb;
        }

        public List<StringBuilder> CreateExpressions(ODE ode)
        {
            List<StringBuilder> differentialEquations = new List<StringBuilder>();
            foreach (var substance in ode.Substances)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(string.Format("d{0}/dt = ", substance.NameOfSubstance));
                sb.Append(ode.Reactions.GetExpressionOfExpenditure(substance));
                sb.Append(ode.Reactions.GetExpressionOfFormation(substance));
                differentialEquations.Add(sb);
            }
            return differentialEquations;
        }
    }
}
