using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    class MinMaxFind
    {
        List<int> results;

        int[,] diagonalMatrix =
        {
            { 1, 0 ,0 ,0, 0 }, 
            { 0, 1, 0, 0, 0 }, 
            { 0, 0, 1, 0, 0 }, 
            { 0, 0, 0, 1, 0 }, 
            { 0, 0, 0, 0, 1 },
        };

        int[,] antiDiagonalMatrix =
        {
            { 0, 1 ,1 ,1, 1 },
            { 1, 0, 1, 1, 1 },
            { 1, 1, 0, 1, 1 },
            { 1, 1, 1, 0, 1 },
            { 1, 1, 1, 1, 0 },
        };

        public MinMaxFind() 
        { 
            results = new List<int>();
        }

        private int[,] mixWithDiagonal(TextBox[,] resultMatrix, bool smaller = false) 
        {
            int[,] mixedMatrix;

                        if (smaller) mixedMatrix = new int[4, 4];
            else mixedMatrix = new int[5, 5];

            for (int i = 0; i < resultMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < resultMatrix.GetLength(1); j++)
                {
                    mixedMatrix[i, j] = Convert.ToInt32(
                        Convert.ToBoolean(Convert.ToInt32(resultMatrix[i, j].Text)) || Convert.ToBoolean(diagonalMatrix[i, j])
                        );
                }
            }

            return mixedMatrix;
        }

        private int[,] mixWithAntiDiagonal(TextBox[,] resultMatrix, bool smaller = false)
        {
            int[,] mixedMatrix;

            if (smaller) mixedMatrix = new int[4, 4];
            else mixedMatrix = new int[5, 5];

            for (int i = 0; i < resultMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < resultMatrix.GetLength(1); j++)
                {
                    mixedMatrix[i, j] = Convert.ToInt32(
                        Convert.ToBoolean(Convert.ToInt32(resultMatrix[i, j].Text)) && Convert.ToBoolean(antiDiagonalMatrix[i, j])
                        );
                }
            }

            return mixedMatrix;
        }

        private void findMaximum(TextBox[,] resultMatrix, bool smaller = false)
        {
            bool noZeros = true;
            int[,] workMatrix = mixWithDiagonal(resultMatrix, smaller);

            for (int i = 0; i < workMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < workMatrix.GetLength(1); j++)
                {
                    if (workMatrix[i, j] == 0)
                    {
                        noZeros = false;
                        break;
                    }
                }
                if (noZeros) results.Add(i + 1);
                else noZeros = true;  
            }
        }

        private void findMinimum(TextBox[,] resultMatrix, bool smaller = false) 
        {
            bool noZeros = true;
            int[,] workMatrix = mixWithDiagonal(resultMatrix, smaller);

            for (int i = 0; i < workMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < workMatrix.GetLength(1); j++)
                {
                    if (workMatrix[j, i] == 0)
                    {
                        noZeros = false;
                        break;
                    }
                }
                if (noZeros) results.Add(i + 1);
                else noZeros = true;
            }
        }

        private void findMinor(TextBox[,] resultMatrix, bool smaller = false)
        {
            bool noOnes = true;
            int[,] workMatrix = mixWithAntiDiagonal(resultMatrix, smaller);

            for (int i = 0; i < workMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < workMatrix.GetLength(1); j++)
                {
                    if (workMatrix[i, j] == 1)
                    {
                        noOnes = false;
                        break;
                    }
                }
                if (noOnes) results.Add(i + 1);
                else noOnes = true;
            }
        }

        private void findMajor(TextBox[,] resultMatrix, bool smaller = false)
        {
            bool noOnes = true;
            int[,] workMatrix = mixWithAntiDiagonal(resultMatrix, smaller);

            for (int i = 0; i < workMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < workMatrix.GetLength(1); j++)
                {
                    if (workMatrix[j, i] == 1)
                    {
                        noOnes = false;
                        break;
                    }
                }
                if (noOnes) results.Add(i + 1);
                else noOnes = true;
            }
        }

        public void ShowResult(TextBox[,] resultMatrix, int operation, bool smaller = false)
        {
            string operationName;

            results.Clear();

            switch (operation)
            {
                case 0:
                    findMaximum(resultMatrix, smaller);
                    operationName = "maximum";
                    break;
                case 1:
                    findMinimum(resultMatrix, smaller);
                    operationName = "minimum";
                    break;
                case 2:
                    findMinor(resultMatrix, smaller);
                    operationName = "minority";
                    break;
                case 3:
                    findMajor(resultMatrix, smaller);
                    operationName = "majorette";
                    break;
                default:
                    MessageBox.Show("Please, select Min/Max functionality!", "Wait!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
            }

            string resultMsg = $"{operationName}: ";
            results.ForEach(result => resultMsg += $"| a{result} ");
            if (!results.Any()) resultMsg = $"{operationName} not founded!";
            else resultMsg += "|";

            MessageBox.Show(resultMsg.ToString(), $"Finding {operationName}");
        }


    }
}
