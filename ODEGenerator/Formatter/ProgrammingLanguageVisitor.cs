using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ODEGenerator.Formatter
{
    abstract class ProgrammingLanguageVisitor :MathVisitor
    {
        protected string nameOfinputArray;
        protected string nameOfoutputArray;

        public string NameOfinputArray
        {
            get { return nameOfinputArray; }
            set { nameOfinputArray = value; }
        }

        public string NameOfoutputArray
        {
            get { return nameOfoutputArray; }
            set { nameOfoutputArray = value; }
        }
    }
}
