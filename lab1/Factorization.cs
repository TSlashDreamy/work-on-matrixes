using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    internal class Factorization
    {
        public void Factorize(string[,] equivalentMatrix)
        {
            Dictionary<string, List<int>> equivalenceClasses = new Dictionary<string, List<int>>();

            for (int i = 0; i < equivalentMatrix.GetLength(0); i++)
            {
                // Add row i to the equivalence class
                string row = "";
                for (int j = 0; j < equivalentMatrix.GetLength(1); j++)
                {
                    row += equivalentMatrix[i, j];
                }
                if (!equivalenceClasses.ContainsKey(row))
                {
                    equivalenceClasses.Add(row, new List<int>());
                }
                equivalenceClasses[row].Add(i);

                // Add column i to the equivalence class
                string column = "";
                for (int j = 0; j < equivalentMatrix.GetLength(1); j++)
                {
                    column += equivalentMatrix[j, i];
                }
                if (!equivalenceClasses.ContainsKey(column))
                {
                    equivalenceClasses.Add(column, new List<int>());
                }
                equivalenceClasses[column].Add(i + equivalentMatrix.GetLength(0));
            }

            int classNum = 1;
            string result = "";
            foreach (List<int> indices in equivalenceClasses.Values)
            {
                result += $"Class {classNum}: ";

                foreach (int index in indices)
                {
                    result += $"{index}, ";
                }
                result += "\n";
                classNum++;
            }

            MessageBox.Show(result, "Factorization");
        }
    }

}
