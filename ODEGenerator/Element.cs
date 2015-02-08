using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODEGenerator
{
    class Element
    {
        private List<Tuple<Element,double>> elements;

        public readonly string NameOfElement;

        public Element(string nameOfElement)
        {
            elements = new List<Tuple<Element, double>>();
            NameOfElement = nameOfElement;
        }

        public void Add(Element element, double k)
        {
            elements.Add(new Tuple<Element, double>(element,k));
        }

        public StringBuilder GetElements()
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                foreach (var element in elements)
                {
                    sb.Append("-");
                    sb.Append(element.Item2);
                    sb.Append("*");
                    sb.Append(element.Item1.NameOfElement);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return sb;
        }

        public StringBuilder FindElement(Element findElement)
        {
            StringBuilder sb = new StringBuilder();
            var findElements = elements.Where(n => findElement == n.Item1).ToList();
            if (findElements.Count() > 0)
            {
                foreach (var element in findElements)
                {
                    sb.Append("-");
                    sb.Append(element.Item2);
                    sb.Append("*");
                    sb.Append(NameOfElement);
                }
            }
            return sb;
        }
    }

    
}
