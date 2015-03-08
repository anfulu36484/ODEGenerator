using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ODEGenerator.Formatter;

namespace ODEGenerator.CodeGenerator.PureCCodeGenerator
{
    class PureCCodeGenerator :CodeGenerator
    {
        public PureCCodeGenerator(ODEs odEs, double[] timeArray) : base(odEs, timeArray)
        {
            _visitor = new PureCVisitor();
        }

        public PureCCodeGenerator(ODEs odEs, double[] timeArray, params GroupOfSubstances[] arrayOfGroupOfSubstances) : base(odEs, timeArray, arrayOfGroupOfSubstances)
        {
            _visitor = new PureCVisitor();
        }

        string DeclareConstants()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("double ");
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
            return string.Format(@"
#include ""stdafx.h""

extern ""C"" {{

__declspec(dllexport)
void SolveODE(double *{0}, double t, double *{1})
{{
    {2}
	{3}
}}
}}",            _visitor.NameOfinputArray,
                _visitor.NameOfoutputArray,
                DeclareConstants(),
                DeclareEquestions());


        }
    }
}
