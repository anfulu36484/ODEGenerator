using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODEGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            ODE ode = new ODE();

            Substance A = new Substance("A");
            Substance B = new Substance("B");
            Substance D = new Substance("D");
            Substance C = new Substance("C");

            ode.Add(new []{A,B},"k",new []{D,C});
            
            ode.PrintResult();

            Console.Read();
        }
    }
}
