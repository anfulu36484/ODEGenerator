using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ODEGenerator.Formatter;

namespace ODEGenerator.CodeGenerator
{
    abstract class CodeGenerator
    {
        protected ODEs odEs;
        protected double[] timeArray;
        protected GroupOfSubstances[] arrayOfGroupOfSubstances;
        protected ProgrammingLanguageVisitor _visitor;


        protected CodeGenerator(ODEs odEs, double[] timeArray)
        {
            this.odEs = odEs;
            this.timeArray = timeArray;
        }

        protected CodeGenerator(ODEs odEs, double[] timeArray, params GroupOfSubstances[] arrayOfGroupOfSubstances)
            :this(odEs, timeArray)
        {
            this.arrayOfGroupOfSubstances = arrayOfGroupOfSubstances;
        }
    }
}
