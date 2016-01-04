using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ODEGenerator;
using ODEGenerator.Formatter;
using ODEGenerator.SyntaxTree.Numerical;

namespace ODEGeneratorExample
{
    class Example1:IExample
    {
        /// <summary>
        /// A+B->C+D
        /// </summary>
        public void RunTest()
        {
            ODEs odEs = new ODEs();

            Substance A = new Substance("A", 0.1);
            Substance B = new Substance("B", 0.3);
            Substance D = new Substance("D", 0);
            Substance C = new Substance("C", 0);

            Constant k = new Constant("k", 1.2);

            odEs.Add(A, B, k, D, C);
            odEs.PrintResult(new MathVisitor());
        }
    }
}
