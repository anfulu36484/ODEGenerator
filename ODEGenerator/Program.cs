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
            /*Element A = new Element("A");
            Element B = new Element("B");
            Element C = new Element("C");
            
            A.Add(B,1);
            A.Add(C,0.2);

            B.Add(C,0.7);
            C.Add(A,0.4);*/

            ODE ode = new ODE();
            /*ode.Add(A);
            ode.Add(B);
            ode.Add(C);*/

            var elements = Enumerable.Range(1, 10).Select(n => new Element("R" + n)).ToList();

            elements[0].Add(elements[1],10);

            elements[elements.Count - 1].Add(elements[elements.Count - 2],20);

            for (int i = 1; i < elements.Count-1; i++)
            {
                elements[i].Add(elements[i+1],10);
                elements[i].Add(elements[i-1],20);
            }

            ode.AddRange(elements);

            ode.PrintResult();

            Console.Read();
        }
    }
}
