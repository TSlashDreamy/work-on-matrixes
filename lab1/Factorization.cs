using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    internal class Factorization
    {
        public void Factorize(string[,] equivalentMatrix, TextBox[,] derivativeMatrix = null)
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
           // int[][] classesArr2 = classes.Select(a => a.ToArray()).ToArray();
            string newMatrixResult = "";

            // classes 1
            for (int i = 0; i < classesArr1.GetLength(0); i++)
            {
                // classes 2
                for (int j = 0; j < classesArr1.GetLength(0); j++)
                {
                    getNumbers(classesArr1[i], classesArr1[j]);
                }
            }


        }

        private void getNumbers(int[] firstClass, int[] secondClass)
        {
            // num 1 
            for (int i = 0; i < firstClass.GetLength(0); i++)
            {
                // num 2
                for (int j = 0; j < secondClass.GetLength(0); j++)
                {
                    MessageBox.Show($"{firstClass[i]} - {secondClass[j]}");
                }
            }
        }
    }

}
