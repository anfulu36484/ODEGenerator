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
            /*ODE ode = new ODE();

            Substance A = new Substance("A");
            Substance B = new Substance("B");
            Substance D = new Substance("D");
            Substance C = new Substance("C");

            ode.Add(new []{A,B},"k1",new []{D,C});
            ode.Add(new[] { D, C }, "k2", new[] { A, B });
            
            ode.PrintResult();*/
            Test();
            Console.Read();
        }


        static void Test()
        {
            ODE ode = new ODE();
            Substance[] R = Enumerable.Range(0, 100).Select(n => new Substance("R"+n)).ToArray();
            Substance M = new Substance("M");

            string kp = "kp";
            string kd = "kd";

            ode.Add(new []{R[0],M},kp,R[1]);

            for (int i = 1; i < R.Count()-1; i++)
            {
                ode.Add(new[] { R[i], M }, kp, R[i+1]);
                ode.Add(new[] { R[i + 1] }, kd, new[] { R[i], M });
            }

            ode.Add(new[] { R.Last(), M }, kp, new Substance("Rn+1"));

            ode.PrintResult();
        }
    }
}
