using System;
using System.IO;
using System.Linq;
using ODEGenerator.SyntaxTree.Numerical;

namespace ODEGenerator.CodeGenerator.CSharpCodeGenerator
{
    public class DataSaver
    {
        private double[,] _data;

        private string _nameOfSaveDirectory;
        private readonly GroupOfSubstances[] _groups;
        private readonly Substance[] _substances;

        public string FormatOutputFile = ".csv";

        public DataSaver(double[,] data, string nameOfSaveDirectory, Substance[] substances)
        {
            _data = data;
            _nameOfSaveDirectory = nameOfSaveDirectory;
            _substances = substances;
        }

        public DataSaver(double[,] data, string nameOfSaveDirectory, Substance[] substances, GroupOfSubstances[] groups)
            :this(data,nameOfSaveDirectory,substances)
        {
            _groups = groups;
        }



        string Double2Str(double input)
        {
            return Convert.ToString(input).Replace(',', '.');
        }


        void SavePartOfTwoDimensionalArray(int[] indexArray, string nameOfFile)
        {
            using (FileStream fs = new FileStream(nameOfFile, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
            {
                StreamWriter sw = new StreamWriter(fs);
                for (int i = 0; i < _data.GetLength(0); i++)
                {
                    for (int j = 0; j < indexArray.Count(); j++)
                        sw.Write("{0};", Double2Str(_data[i, indexArray[j]]));
                    sw.WriteLine();
                }
                sw.Close();
            }
        }

        void SaveGroup(GroupOfSubstances group)
        {
            string nameOfFile = string.Format(@"{0}\{1}{2}", _nameOfSaveDirectory, group.NameOfGroup, FormatOutputFile);
            SavePartOfTwoDimensionalArray(group.Substances.Keys.ToArray(), nameOfFile);
        }

        void SaveGroups()
        {
            foreach (var @group in _groups)
            {
                SaveGroup(@group);
            }
        }

        void SaveSubstance(Substance substance)
        {
            string nameOfFile = string.Format(@"{0}\{1}{2}", _nameOfSaveDirectory, substance.Name, FormatOutputFile);
            SavePartOfTwoDimensionalArray(new int[] {substance.ODEId}, nameOfFile);
        }

        void SaveSubstancesWithoutGroup()
        {
            foreach (var substance in _substances.Where(n => n.GroupOfSubstances == null))
            {
                SaveSubstance(substance);
            }
        }

        public void Save()
        {
            for (int i = 0; i < _data.GetLength(0); i++)
            {
                for (int j = 0; j < _data.GetLength(1); j++)
                {
                    Console.Write(" ");
                    Console.Write(_data[i,j]);
                }
                Console.WriteLine();
            }

            if(_groups!=null)
                SaveGroups();
            SaveSubstancesWithoutGroup();
        }

    }
}
