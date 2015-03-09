using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DotNumerics.ODE;

namespace ODEGenerator.CodeGenerator.CSharpCodeGenerator
{
    class CsharpCodeManager
    {
        private readonly int _countOfCodeParts = 1;
        private readonly GroupOfSubstances[] _groupOfSubstanceses;
        private CsharpCodeGenerator _codeGenerator;
        private Compiler _compiler;
        private ODEsSolver _odesSolver;
        private DataSaver _dataSaver;
        private ODEs _odes;
        private double[] _timeArray;

        public CsharpCodeManager(ODEs odes, double[] timeArray, xBaseOdeRungeKutta odeRungeKutta)
        {
            _timeArray = timeArray;
            _codeGenerator = new CsharpCodeGenerator(odes,timeArray);
            _compiler = new Compiler(odes.Substances.Count);
            _odesSolver = new ODEsSolver(odeRungeKutta);
            _odes = odes;
        }

        public CsharpCodeManager(ODEs odes, double[] timeArray, xBaseOdeRungeKutta odeRungeKutta,
            GroupOfSubstances[] groupOfSubstanceses)
            :this(odes,timeArray,odeRungeKutta)
        {
            _groupOfSubstanceses = groupOfSubstanceses;
        }

        public CsharpCodeManager(ODEs odes, double[] timeArray, xBaseOdeRungeKutta odeRungeKutta, int countOfCodeParts,
            GroupOfSubstances[] groupOfSubstanceses)
            : this(odes, timeArray, odeRungeKutta, groupOfSubstanceses)
        {
            _countOfCodeParts = countOfCodeParts;
        }

        public List<string> GenerateCode()
        {
            return _codeGenerator.GeneratingAtParts(_countOfCodeParts);
        }

        public void PrintGeneratedCode()
        {
            Console.Write(GenerateCode());
        }

        private double[] GetInitialValues()
        {
            return _odes.Substances.Select(n => n.Value).ToArray();
        }

        public void SolveODEs(string nameOfDirectory)
        {
            _compiler.CompileParts(GenerateCode());
            Console.WriteLine("Библиотека(и) для решения системы оду создана(ы)");
            OdeFunction odeFunction = _compiler.ODEsSolve;
            double[,] result = _odesSolver.Solve(odeFunction, GetInitialValues(), _timeArray);

            if (!Directory.Exists(nameOfDirectory))
                Directory.CreateDirectory(nameOfDirectory);

            _dataSaver = _groupOfSubstanceses!=null ?
                new DataSaver(result,nameOfDirectory,_odes.Substances.ToArray(),_groupOfSubstanceses) 
                : new DataSaver(result, nameOfDirectory, _odes.Substances.ToArray());
            _dataSaver.Save();
        }

        
    }
}
