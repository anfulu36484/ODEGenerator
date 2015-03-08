using System.Text;
using ODEGenerator.Formatter;

namespace ODEGenerator.CodeGenerator.CSharpCodeGenerator
{
    class CsharpCodeGenerator :CodeGenerator
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

        string DeclareEquestions()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var expression in odEs.CreateExpressions())
            {
                sb.Append(expression.Accept(_visitor));
                sb.AppendLine(";");
            }
            return sb.ToString();
        }

        public string Generate()
        {
            return string.Format(
                @"namespace ODENumerics
{{
    public class ODEFunction
    {{
        {0}

        public static double[] ODEs(double t, double[] {1})
        {{
            double[] {2} = new double[{3}];
            {4}
            return {2};
        }}
    }}
}}", DeclareConstants(),
   _visitor.NameOfinputArray,
   _visitor.NameOfoutputArray,
   odEs.Substances.Count,
   DeclareEquestions());
        }


        


    }
}
