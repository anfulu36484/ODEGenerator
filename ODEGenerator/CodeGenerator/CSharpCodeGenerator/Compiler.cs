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

        private static int _countOfEquestions;

        private double[] output;

        public Compiler(int countOfEquestions)
        {
            _countOfEquestions = countOfEquestions;
            output = new double[countOfEquestions];
        }

        List<string> _namesOFAssemblies = new List<string>();

        string GetNewNameOfAssembly()
        {
            string name = string.Format(@"test\odesCsharp{0}.dll", _namesOFAssemblies.Count + 1);
            _namesOFAssemblies.Add(name);
            return name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code">Код для компиляции</param>
        public void Compile(string code)
        {

            // Настройки компиляции 
            Dictionary<string, string> providerOptions = new Dictionary<string, string>
            {
                {"CompilerVersion", "v4.0"}
            };
            CSharpCodeProvider provider = new CSharpCodeProvider(providerOptions);

            CompilerParameters compilerParams = new CompilerParameters
            {
                OutputAssembly = GetNewNameOfAssembly(),
                GenerateExecutable = false
            };

            // Компиляция 
            CompilerResults results = provider.CompileAssemblyFromSource(compilerParams, code);

            // Выводим информацию об ошибках 
            Console.WriteLine("Number of Errors: {0}", results.Errors.Count);
            foreach (CompilerError err in results.Errors)
            {
                Console.WriteLine("ERROR {0}", err.ErrorText);
            }
        }

        public void CompileParts(List<string> codesList)
        {
            foreach (var code in codesList)
            {
                Compile(code);
            }
        }


        private MethodInfo[] _methods;

        public void LoadLibraries()
        {
            _methods = new MethodInfo[_namesOFAssemblies.Count];
            for (int i = 0; i < _namesOFAssemblies.Count; i++)
            {
                Assembly assembly = Assembly.LoadFile(string.Format(@"{0}\{1}", Environment.CurrentDirectory, _namesOFAssemblies[i]));
                Type type = assembly.GetType("ODENumerics.ODEFunction");
                _methods[i] = type.GetMethod("ODEs");
            }
            Console.WriteLine("Библиотека(и) для решения системы оду загружена(ы)");
        }

        

        public double[] ODEsSolve(double t, double[] y)
        {
            if (_methods == null)
                LoadLibraries();
            
            for (int i = 0; i < _methods.Count(); i++)
            {
                _methods[i].Invoke(null, new[] { t, (object)y, output });
            }
            return output;
        }



    }
}
