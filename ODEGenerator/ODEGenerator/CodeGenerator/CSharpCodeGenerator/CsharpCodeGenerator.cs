using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ODEGenerator.Formatter;

namespace ODEGenerator.CodeGenerator.CSharpCodeGenerator
{
    public class CsharpCodeGenerator :CodeGenerator
    {
        public CsharpCodeGenerator(ODEs odEs, double[] timeArray) : base(odEs, timeArray)
        {
            _visitor = new CsharpVisitor();
        }

        public CsharpCodeGenerator(ODEs odEs, double[] timeArray, params GroupOfSubstances[] arrayOfGroupOfSubstances) 
            : base(odEs, timeArray, arrayOfGroupOfSubstances)
        {
            _visitor = new CsharpVisitor();
        }

        string DeclareConstants()
        {
            StringBuilder sb  = new StringBuilder();
            
            sb.Append("static double ");
            for (int i = 0; i < odEs.RateConstants.Count; i++)
            {
                sb.Append(odEs.RateConstants[i].Name);
                sb.Append("=");
                sb.Append(odEs.RateConstants[i].Value.ToString().Replace(",", "."));
                if (i != odEs.RateConstants.Count - 1)
                    sb.Append(",");
            }
            sb.Append(";");
            return sb.ToString();
        }

        List<string> DeclareEquestions()
        {
            List<string> list = new List<string>(odEs.Substances.Count);
            foreach (var expression in odEs.CreateExpressions())
            {
                list.Add(expression.Accept(_visitor)+";");
            }
            return list;
        }


        string Generate(string equestions)
        {
            return string.Format(
                @"namespace ODENumerics
{{
    public class ODEFunction
    {{
        {0}

        public static void ODEs(double t, double[] {1}, double[] {2})
        {{
            {3}
        }}
    }}
}}", DeclareConstants(),
   _visitor.NameOfinputArray,
   _visitor.NameOfoutputArray,
   equestions);
        }


        private void CreateFullParts(int countOfParts, int sizeOfpart, StringBuilder sb,
            List<string> equestions,ref int numberOfEquestion,
            List<string> parts)
        {
            for (int i = 0; i < countOfParts; i++)
            {
                for (int j = 0; j < sizeOfpart; j++)
                {
                    sb.AppendLine(equestions[numberOfEquestion]);
                    numberOfEquestion++;
                }
                parts.Add(sb.ToString());
                sb.Clear();
            }
        }

        private void CreateNotFullPart(ref int numberOfEquestion, StringBuilder sb, List<string> equestions, List<string> parts)
        {
            for (; numberOfEquestion < odEs.Substances.Count; )
            {
                sb.AppendLine(equestions[numberOfEquestion]);
                numberOfEquestion++;
            }

            if (sb.Length > 0)
                parts.Add(sb.ToString());
        }

        public List<string> GeneratingAtParts(int countOfParts)
        {
            if(countOfParts<1 && countOfParts>odEs.Substances.Count)
                throw new FormatException();
            int sizeOfpart = odEs.Substances.Count/countOfParts;

            List<string> equestions = DeclareEquestions();

            List<string> parts = new List<string>(countOfParts);

            int numberOfEquestion = 0;

            StringBuilder sb = new StringBuilder();

            //Создание полных групп 
            CreateFullParts(countOfParts, sizeOfpart, sb, equestions,ref numberOfEquestion, parts);

            //Неполная группа
            CreateNotFullPart(ref numberOfEquestion, sb, equestions, parts);


            //parts.ToList().ForEach(n=>Console.Write(n+"\n\n\n"));


            return parts.Select(Generate).ToList();
        }

        
    }
}
