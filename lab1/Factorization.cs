using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    internal class Factorization
    {
        public void Factorize(string[,] equivalentMatrix, TextBox[,] derivativeMatrix)
        {
            // Dividing classes 
            List<List<int>> classes = new List<List<int>>();

            for (int i = 0; i < equivalentMatrix.GetLength(0); i++)
            {
                bool exist = false;
                List<int> tempIndexes = new List<int>();

                foreach (List<int> indexes in classes)
                {
                    if (exist) break;

                    foreach (int index in indexes)
                    {
                        if (index == i) { 
                            exist = true; 
                            break; 
                        }
                    }
                }

                if (exist) continue;
                
                for (int j = 0; j < equivalentMatrix.GetLength(1); j++)
                {
                    if (Convert.ToInt32(equivalentMatrix[i, j]) == 1)
                    {
                        tempIndexes.Add(j);
                    }
                }
                classes.Add(tempIndexes);
            }

            int classIter = 1;
            string result = "";

            foreach (List<int> indexes in classes)
            {
                result += $"Class {classIter}: ";
                foreach (int index in indexes)
                {
                    result += $"{index}, ";
                }
                result += "\n";
                classIter++;
            }

            MessageBox.Show(result, "Factorization (Classes division)");

            // ----- Forming Pd matrix ----- 
            int[][] classesArr1 = classes.Select(a => a.ToArray()).ToArray();
            List<int> arrIndexes = new List<int>();
            string newMatrixResult = "";

            // classes 1
            for (int i = 0; i < classesArr1.GetLength(0); i++)
            {
                // classes 2
                for (int j = 0; j < classesArr1.GetLength(0); j++)
                {
                   newMatrixResult += getNumbers(classesArr1[i], classesArr1[j], derivativeMatrix);
                }
                newMatrixResult += "\n";
            }

            MessageBox.Show(newMatrixResult, "Matrix");
        }

        private string getNumbers(int[] firstClass, int[] secondClass, TextBox[,] derivativeMatrix)
        {
            // num 1 
            for (int i = 0; i < firstClass.GetLength(0); i++)
            {
                // num 2
                for (int j = 0; j < secondClass.GetLength(0); j++)
                {
                    if(derivativeMatrix[firstClass[i], secondClass[j]].Text == "1") return "1\t";
                }
            }
            return "0\t";
        }
    }

}
