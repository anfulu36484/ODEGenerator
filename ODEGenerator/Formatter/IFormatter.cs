using System.Collections.Generic;
using System.Text;

namespace ODEGenerator.Formatter
{
    interface IFormatter
    {
        StringBuilder GetExpressionOfExpenditure(Reaction reaction);

        List<StringBuilder> CreateExpressions(ODE ode);

    }

}
