using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODEGenerator;
using ODEGenerator.Formatter;
using ODEGenerator.SyntaxTree.Numerical;


namespace ODEGeneratorExample
{
    /// <summary>
    /// Simple Reaction 
    /// A+B->C
    /// </summary>
    class Example0:IExample
    {
        public void RunTest()
        {
            ODEs odEs = new ODEs(); //system of differential equations

            Substance A = new Substance("A", 0.1); //starting (initial) concentration of A - 0.1
            Substance B = new Substance("B", 0.3); //starting (initial) concentration of A - 0.3
            Substance C = new Substance("C", 0); //starting (initial) concentration of A - 0

            Constant k = new Constant("k", 1.2); //reaction rate constant

            odEs.Add(A, B, k, C); //adding to the reaction system (A+B->C)
            odEs.PrintResult(new MathVisitor());
        }
    }
}
