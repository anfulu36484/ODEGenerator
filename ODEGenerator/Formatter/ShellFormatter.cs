using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODEGenerator.Formatter
{
    class ShellFormatter :IFormatter
    {

        private IFormatter _formatter;

        public ShellFormatter(IFormatter formatter)
        {
            _formatter = formatter;
        }

        public void ResetFormatter(IFormatter formatter)
        {
            _formatter = formatter;
        }

        public StringBuilder GetExpressionOfExpenditure(Reaction reaction)
        {
            return _formatter.GetExpressionOfExpenditure(reaction);
        }

        public List<StringBuilder> CreateExpressions(ODE ode)
        {
            return _formatter.CreateExpressions(ode);
        }
    }
}
