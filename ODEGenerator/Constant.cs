using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODEGenerator
{
    class Constant
    {
        public readonly string NameOfRateConstant;

        public readonly double ValueOfRateConstant;

        public Constant(string nameOfRateConstant, double valueOfRateConstant)
        {
            NameOfRateConstant = nameOfRateConstant;
            ValueOfRateConstant = valueOfRateConstant;
        }
    }
}
