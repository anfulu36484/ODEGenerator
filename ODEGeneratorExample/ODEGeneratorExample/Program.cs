using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODEGeneratorExample
{
    class Program
    {
        static void Main(string[] args)
        {
            IExample example = new Example1();
            example.RunTest();
            Console.Read();
        }
    }
}
