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
        private List<DependedElements> dependedElementsList;

        public readonly string NameOfElement;

        public Element(string nameOfElement)
        {
            dependedElementsList = new List<DependedElements>();
            NameOfElement = nameOfElement;
        }

        public void AddDependedElements(DependedElements dependedElements)
        {
            dependedElementsList.Add(dependedElements);
        }

        public StringBuilder GettingDependedElements()
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                foreach (var dependedElements in dependedElementsList)
                {
                    sb.Append("-");
                    sb.Append(dependedElements.RateConstant);
                    sb.Append("*");

                    for (int i = 0; i < UPPER; i++)
                    {
                        
                    }
                    
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
            var findElements = dependedElementsList.Where(n => findElement == n.Item1).ToList();
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

    class DependedElements
    {
        private List<Element> _elements;

        private double _rateConstant;

        public DependedElements(List<Element> elements, double rateConstant)
        {
            _elements = elements;
            _rateConstant = rateConstant;
        }

        public List<Element> Elements
        {
            get { return _elements; }
        }

        public double RateConstant
        {
            get { return _rateConstant; }
        }

        


    }

    
}
