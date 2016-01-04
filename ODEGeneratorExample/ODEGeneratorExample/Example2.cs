using System;
using ODEGenerator;
using ODEGenerator.CodeGenerator;
using ODEGenerator.Formatter;
using ODEGenerator.SyntaxTree.Numerical;

namespace ODEGeneratorExample
{
    class Example2:IExample
    {
        public void RunTest()
        {
            ODEs odEs = new ODEs();

            Substance A = new Substance("A", 10);
            Substance B = new Substance("B", 20);
            Substance D = new Substance("D", 0);
            Substance C = new Substance("C", 0);

            Constant k1 = new Constant("k1", 1.2);
            Constant k2 = new Constant("k2", 1.7);

            odEs.Add(A, B, k1, D, C);
            odEs.Add(D, C, k2, A, B);

            odEs.PrintResult(new MathVisitor());

            MatlabCodeGenerator matlabCodeGenerator = new MatlabCodeGenerator(odEs, new[] { 0.0, 10, 20 });
            matlabCodeGenerator.Generate(Environment.CurrentDirectory + @"\test\test2");
        }
    }
}
