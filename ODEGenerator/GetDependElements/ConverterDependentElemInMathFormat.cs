using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODEGenerator.GetDependElements
{
    class ConverterDependentElemInMathFormat :IConverterOfDependentElements
    {

        public StringBuilder ConvertDependedElements(List<DependedElements> dependedElementsList)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                foreach (var dependedElements in dependedElementsList)
                {
                    sb.Append("-");
                    sb.Append(dependedElements.RateConstant);

                    for (int i = 0; i < dependedElements.Elements.Count; i++)
                    {
                        sb.Append("*");
                        sb.Append(dependedElements.Elements[i]);
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return sb;
        }
    }
}
