using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    internal class Distance
    {
        int[,] matrix1;
        int[,] matrix2;
        bool[,] matrix1IsEmpty;
        bool[,] matrix2IsEmpty;

        public Distance()
        {
            matrix1 = new int[5, 5];
            matrix2 = new int[5, 5];
            matrix1IsEmpty = new bool[5, 5];
            matrix2IsEmpty = new bool[5, 5];
        }

        /// <summary>
        /// Converts boolean to string
        /// </summary>
        public bool StringToBool(string value)
        {
            return Convert.ToBoolean(Convert.ToInt32(value));
        }

        /// <summary>
        /// Checks if matrix is fully filled 
        /// </summary>
        private bool FillCheck(TextBox[][,] allMatrixes, List<int> selectedMatrixes, ref bool firstMatrixValue, ref bool secondMatrixValue, int i, int j)
        {
            try
            {
                firstMatrixValue = StringToBool(allMatrixes[selectedMatrixes[0]][i, j].Text);
                secondMatrixValue = StringToBool(allMatrixes[selectedMatrixes[1]][i, j].Text);
    
                return true;
            }
            catch
            {
                MessageBox.Show("Please, fill selected matrixes", "Wait!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        /// <summary>
        /// Calculates matrixes via selected operation
        /// </summary>
        private bool doOperation(TextBox[][,] allMatrixes, List<int> selectedMatrixes)
        {
            if (selectedMatrixes.Count != 2)
            {
                MessageBox.Show("Please, select 2 matrixes for distance finding.", "Wait!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            bool firstMatrixValue = false;
            bool secondMatrixValue = false;

            for (int i = 0; i < matrix1.GetLength(0); i++)
            {
                for (int j = 0; j < matrix1.GetLength(1); j++)
                {
                    if (!FillCheck(allMatrixes, selectedMatrixes, ref firstMatrixValue, ref secondMatrixValue, i, j)) return false;

                    matrix1[i, j] = Convert.ToInt32(firstMatrixValue);
                    matrix2[i, j] = Convert.ToInt32(secondMatrixValue);
                }
            }
            
            return true;
        }

        private void transformMatrixes(int[,] operatedMatrix)
        {
            for (int i = 0; i < operatedMatrix.GetLength(0); i++) 
            {
                operatedMatrix[i, i] = 0;

                for (int j = 0; j < operatedMatrix.GetLength(1); j++)
                {
                    if (i == j) continue;

                    if (operatedMatrix[i, j] == 1)
                    {
                        if (operatedMatrix[j, i] == 0) operatedMatrix[j, i] = -1;
                        else
                        {
                            operatedMatrix[i, j] = 0;
                            operatedMatrix[j, i] = 0;
                        }
                    }
                }

            }
        }


        public bool ShowResult(TextBox[][,] allMatrixes, List<int> selectedMatrixes)
        {
            if(!doOperation(allMatrixes, selectedMatrixes)) return false;
            transformMatrixes(matrix1);
            transformMatrixes(matrix2);

            int diagonalCounter = 1;
            int sum = 0;

            for (int i = 0; i < matrix1.GetLength(0); i++)
            {
                for (int j = 0; j < matrix1.GetLength(1) - diagonalCounter; j++)
                {
                    sum += Math.Abs(matrix1[i, j + diagonalCounter] - matrix2[i, j + diagonalCounter]);
                }

                diagonalCounter++;
            }

            MessageBox.Show($"Distance = {sum}", "Distance between matrixes", MessageBoxButtons.OK);
            return true;
        }

        private bool MinusCheck(TextBox[][,] allMatrixes, List<int> selectedMatrixes, ref int matrixValue, int i, int j, int matrixNum)
        {
            try
            {
                matrixValue = Convert.ToInt32(allMatrixes[selectedMatrixes[matrixNum]][i, j].Text);
                return true;
            }
            catch
            {
                MessageBox.Show("Please, fill selected matrixes", "Wait!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        private bool FillDecimalCheck(TextBox[][,] allMatrixes, List<int> selectedMatrixes, ref int firstMatrixValue, ref int secondMatrixValue, int i, int j)
        {
            const int firstMatrixNum = 0;
            const int secondMatrixNum = 1;

            try
            {
                firstMatrixValue = Convert.ToInt32(allMatrixes[selectedMatrixes[firstMatrixNum]][i, j].Text);
                secondMatrixValue = Convert.ToInt32(allMatrixes[selectedMatrixes[secondMatrixNum]][i, j].Text);

                return true;
            }
            catch
            {
                if (allMatrixes[selectedMatrixes[firstMatrixNum]][i, j].Text == "-")
                {
                    matrix1IsEmpty[i, j] = true;
                } 
                else
                {
                    if(!MinusCheck(allMatrixes, selectedMatrixes, ref firstMatrixValue, i, j, firstMatrixNum)) return false;
                }

                if (allMatrixes[selectedMatrixes[secondMatrixNum]][i, j].Text == "-")
                {
                    matrix2IsEmpty[i, j] = true;
                }
                else
                {
                    if(!MinusCheck(allMatrixes, selectedMatrixes, ref secondMatrixValue, i, j, secondMatrixNum)) return false;
                }

                return true;
            }
        }

        private bool doDecimalOperation(TextBox[][,] allMatrixes, List<int> selectedMatrixes)
        {
            if (selectedMatrixes.Count != 2)
            {
                MessageBox.Show("Please, select 2 matrixes for distance finding.", "Wait!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            int firstMatrixValue = 0;
            int secondMatrixValue = 0;

            for (int i = 0; i < matrix1.GetLength(0); i++)
            {
                for (int j = 0; j < matrix1.GetLength(1); j++)
                {
                    if (!FillDecimalCheck(allMatrixes, selectedMatrixes, ref firstMatrixValue, ref secondMatrixValue, i, j)) return false;

                    matrix1[i, j] = firstMatrixValue;
                    matrix2[i, j] = secondMatrixValue;
                }
            }

            return true;
        }

        public bool ShowDecimalResult(TextBox[][,] allMatrixes, List<int> selectedMatrixes)
        {
            int sum = 0;
            int wCount = 0;

            if (!doDecimalOperation(allMatrixes, selectedMatrixes)) return false;

            for (int i = 0; i < matrix1.GetLength(0); i++)
            {
                for (int j = 0; j < matrix1.GetLength(1); j++)
                {

                    if (i == j) continue;

                    if (matrix1IsEmpty[i, j] && matrix2IsEmpty[i, j]) sum += 0;
                    else if (matrix1IsEmpty[i, j] || matrix2IsEmpty[i, j]) wCount++;
                    else
                    {
                        sum += Math.Abs(matrix1[i, j] - matrix2[i, j]);
                        sum += Math.Abs(matrix1[j, i] - matrix2[j, i]);
                    }


                }

            }

            MessageBox.Show($"Distance = {(wCount > 0 ? $"1/2({sum} + {wCount}W)" : $"{sum/2}")}", "Distance between matrixes", MessageBoxButtons.OK);
            return true;
        }

    }
}
