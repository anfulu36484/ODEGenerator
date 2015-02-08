using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODEGenerator
{
    class ODE
    {
        List<Element> elements = new List<Element>();

        public void Add(Element element)
        {
            elements.Add(element);
        }

        public void AddRange(IEnumerable<Element> e)
        {
            elements.AddRange(e);
        }

        public List<StringBuilder> GetResult()
        {
            List<StringBuilder> result = new List<StringBuilder>();

            foreach (var element in elements)
            {

                StringBuilder sb = new StringBuilder();

                sb.Append("d" + element.NameOfElement + "/dt = ");
                sb.Append(element.GetElements());
                foreach (var element1 in elements)
                {
                    StringBuilder sb2 = element1.FindElement(element);
                    if (sb2 != null)
                        sb.Append(sb2);
                }
                result.Add(sb);
            }
            return result;
        }

        public void PrintResult()
        {
            List<StringBuilder> result = GetResult();
            foreach (var stringBuilder in result)
            {
                Console.WriteLine(stringBuilder.ToString());
            }
        }

    }
}
