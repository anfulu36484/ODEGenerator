using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ODEGenerator;
using ODEGenerator.CodeGenerator;
using ODEGenerator.SyntaxTree.Numerical;

namespace ODEGeneratorExample
{
    /// <summary>
    /// Chain transfer to monomer and low molecular weight agent
    ///  Rajeev M. Modeling and simulation of poly(lactic acid) polymerization: thesis … PhD. – India, 2006. – 122 p.
    /// </summary>
    class Example4 :IExample
    {
        public void RunTest()
        {
            ODEs odEs = new ODEs();

            double MoCo = 3252.79; // Mo/Co

            Substance M = new Substance("M", 10); //Monomer
            Substance C = new Substance("C", M.Value / MoCo); //Catalyst
            Substance H2O = new Substance("H2O", 0.0001); //Transfer agent - water
            Substance H = new Substance("H", 0); //H+
            Substance OH = new Substance("OH", 0); //OH-

            GroupOfSubstances P = new GroupOfSubstances("P"); //Living polymer chain
            GroupOfSubstances D = new GroupOfSubstances("D"); //The dead polymer chain

            Constant ko = new Constant("ko", 0.003); //constant initiation
            Constant kj = new Constant("kj", 0.9); //constant growth
            Constant kt = new Constant("kt", Math.Pow(10, -6)); //Constant chain terminators
            Constant ktc = new Constant("ktc", 9); //The constant of chain transfer agent to a low molecular weight

            int L = 1200;

            for (int j = 1; j <= L; j++)
                P.CreateSubstance(0, j);

            for (int j = 1; j <= L - 1; j++)
                D.CreateSubstance(0, j);


            odEs.Add(M, C, ko, P[1]);
            for (int j = 1; j <= L - 1; j++)
            {
                odEs.Add(P[j], M, kj, P[j + 1]);
                odEs.Add(P[j], M, kt, D[j], P[1]);
                odEs.Add(P[j], H2O, ktc, D[j], H, OH);
            }

            double[] timeAray = new double[] { 0, 10, 90, 150, 200, 300, 400 };

            MatlabCodeGenerator matlabCodeGenerator = new MatlabCodeGenerator(
                odEs,
                timeAray,
                P, D);

            matlabCodeGenerator.Generate(Environment.CurrentDirectory + @"\test\test4");
        }
    }
}
