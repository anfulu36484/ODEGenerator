using System;
using System.Linq;
using DotNumerics.ODE;

namespace ODEGenerator.CodeGenerator.CSharpCodeGenerator
{
    class ODEsSolver
    {
        private xBaseOdeRungeKutta _odeRungeKutta;

        public ODEsSolver(xBaseOdeRungeKutta odeRungeKutta)
        {
            _odeRungeKutta = odeRungeKutta;
        }

        public double[,] Solve(OdeFunction odeFunction, double[] initialValues, double[] tauRange)
        {
            Console.WriteLine("Решатель системы оду запущен");
            _odeRungeKutta.InitializeODEs(odeFunction, initialValues.Count());
            return _odeRungeKutta.Solve(initialValues,tauRange);
        }

    }
}
