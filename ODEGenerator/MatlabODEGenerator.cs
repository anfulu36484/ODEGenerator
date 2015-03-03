using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODEGenerator.Formatter;

namespace ODEGenerator
{
    class MatlabODEGenerator
    {
        private ODE ode;
        private double[] timeArray;
        private GroupOfSubstances[] arrayOfGroupOfSubstances;
        MatlabFormatter matlabFormatter = new MatlabFormatter();


        public MatlabODEGenerator(ODE ode, double[] timeArray)
        {
            this.ode = ode;
            this.timeArray = timeArray;
        }

        public MatlabODEGenerator(ODE ode, double[] timeArray, params GroupOfSubstances[] arrayOfGroupOfSubstances)
            :this(ode, timeArray)
        {
            this.arrayOfGroupOfSubstances = arrayOfGroupOfSubstances;
        }

        StringBuilder CreateSolveFunction()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("function out=solve(tau,y,initialValues)");
            sb.AppendLine("out=zeros(size(y));");

            sb.AppendLine("%Список констант");
            foreach (var rateConstant in ode.RateConstants)
                sb.AppendLine(string.Format("{0} = initialValues.{0};",rateConstant.Name));

            sb.AppendLine("%Система дифференциальных уравнений");
            foreach (var expression in ode.CreateExpressions())
                sb.AppendLine(expression.ToString());

            return sb;
        }

        StringBuilder CreateIcsFunction()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("function out=Ics()");
            sb.AppendLine(string.Format("out=zeros(1,{0});", ode.Substances.Count));

            foreach (var substance in ode.Substances)
                if(substance.Value!=0)
                    sb.AppendLine(string.Format("out(1,{0})={1};", substance.ODEId, substance.Value.ToString().Replace(',', '.')));

            return sb;
        }

        StringBuilder CreateTimeScript()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("%Время расчета");
            sb.Append("tauRange=[");
            foreach (var time in timeArray)
                sb.AppendFormat("{0} ", time.ToString().Replace(',', '.'));
            sb.Append("];\n");
            return sb;
        }

        private StringBuilder CreateGroupOfSubstancesOutput()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var groupOfSubstances in arrayOfGroupOfSubstances)
            {
                foreach (var substance in groupOfSubstances.Substances)
                {
                    if (substance.Value.ODEId != -1)
                    {
                        //Пример: R(1:5,1)=y(1:5,2);
                        sb.AppendFormat("{0}(1:{1},{2})={3}(1:{4},{5});\n",
                            groupOfSubstances.NameOfGroup,
                            timeArray.Length,
                            substance.Key,
                            matlabFormatter.NameOfinputArray,
                            timeArray.Length,
                            substance.Value.ODEId
                            );
                    }
                    else
                    {
                        throw new Exception(string.Format("Элемент {0} входит в состав группы {1}, но не входит в состав системы дифференциальных уравнений",
                        substance.Value.Name,groupOfSubstances.NameOfGroup));
                    }
                }
            }
            return sb;
        }

        private StringBuilder CreateNotGroupOutput()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var substance in ode.Substances.Where(n => n.GroupOfSubstances == null))
            {

                    sb.AppendFormat("{0}(1:{1},{2})={3}(1:{4},{5});\n",
                        substance.Name,
                        timeArray.Length,
                        1,
                        matlabFormatter.NameOfinputArray,
                        timeArray.Length,
                        substance.ODEId
                        );


            }
            return sb;
        }

        private StringBuilder CreateOutputScript()
        {
            StringBuilder sb = new StringBuilder();
            if (arrayOfGroupOfSubstances != null)
            {
                sb.Append(CreateGroupOfSubstancesOutput());
            }

            sb.Append(CreateNotGroupOutput());

            return sb;
        }

        


        StringBuilder CreateRunSolveFunction()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("clear");
            sb.AppendLine("clc");
            sb.AppendLine("tic\n");

            sb.AppendLine("%Определение начальных значений констант");
            foreach (var rateConstant in ode.RateConstants)
            {
                sb.AppendFormat("initialValues.{0}={1};\n", rateConstant.Name,
                    rateConstant.Value.ToString().Replace(',', '.'));
            }

            sb.Append("Time\n\n");

            sb.AppendLine("%Расчет системы дифференцтальных уравнений");
            sb.AppendLine("[tau,y]=ode45(@solve,tauRange,Ics(),odeset('RelTol',1e-13,'AbsTol',1e-13),initialValues);\n");


            sb.Append("Output\n\n");

            sb.AppendLine("toc");

            return sb;

        }

        


        string UTF8ToANSI(string utf8_string)
        {
            Encoding ANSI = Encoding.GetEncoding(1251);
            Encoding UTF8 = Encoding.UTF8;
            byte[] utf8_bytes, ansi_bytes;

            utf8_bytes = UTF8.GetBytes(utf8_string);
            ansi_bytes = Encoding.Convert(UTF8, ANSI, utf8_bytes);

            string ansi_str = ANSI.GetString(ansi_bytes);

            return ansi_str;
        }


        void SaveFunctionToFile(string nameOfDirectory, string nameOfFile, StringBuilder function)
        {
            if (!Directory.Exists(nameOfDirectory))
            Directory.CreateDirectory(nameOfDirectory);
            using (StreamWriter sw = new StreamWriter(nameOfDirectory+@"\"+nameOfFile, false, Encoding.GetEncoding(1251)))
            {
                sw.Write(UTF8ToANSI(function.ToString()));
            }
        }

        public void Generate(string nameOfDirectory)
        {
            SaveFunctionToFile(nameOfDirectory,"solve.m",CreateSolveFunction());
            SaveFunctionToFile(nameOfDirectory,"Ics.m",CreateIcsFunction());
            SaveFunctionToFile(nameOfDirectory, "RunSolve.m", CreateRunSolveFunction());
            SaveFunctionToFile(nameOfDirectory, "Time.m", CreateTimeScript());
            SaveFunctionToFile(nameOfDirectory, "Output.m", CreateOutputScript());
        }
    }


}
