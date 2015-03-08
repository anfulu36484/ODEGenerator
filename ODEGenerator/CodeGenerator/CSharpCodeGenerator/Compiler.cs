using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.CSharp;

namespace ODEGenerator.CodeGenerator.CSharpCodeGenerator
{
    class Compiler
    {

        string outputAssembly = "odesCsharp.dll";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code">Код для компилляции</param>
        public void Compile(string code)
        {

            // Настройки компиляции 
            Dictionary<string, string> providerOptions = new Dictionary<string, string>
            {
                {"CompilerVersion", "v4.0"}
            };
            CSharpCodeProvider provider = new CSharpCodeProvider(providerOptions);

            CompilerParameters compilerParams = new CompilerParameters { OutputAssembly = outputAssembly, GenerateExecutable = false };

            // Компиляция 
            CompilerResults results = provider.CompileAssemblyFromSource(compilerParams, code);

            // Выводим информацию об ошибках 
            Console.WriteLine("Number of Errors: {0}", results.Errors.Count);
            foreach (CompilerError err in results.Errors)
            {
                Console.WriteLine("ERROR {0}", err.ErrorText);
            }
        }

        private MethodInfo _method;

        public void LoadLibrary()
        {
            Assembly assembly =
                Assembly.LoadFile(string.Format(@"{0}\{1}", Environment.CurrentDirectory, outputAssembly));
            Type type = assembly.GetType("ODENumerics.ODEFunction");
            _method = type.GetMethod("ODEs");
        }

        public double[] ODEsSolve(double t, double[] y)
        {
            if(_method ==null)
                LoadLibrary();
            Object result = _method.Invoke(null, new[] { t, (object)y });

            return result as double[];
        }


    }
}
