﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    internal class Ratio
    {
        // ---- Matrix states ----

        private bool matrixValue = false; // for matrix it would be like "0" and "1"
        public bool MatrixValue { get { return matrixValue; } set { matrixValue = value; } }

        private bool diagonalOperation = false;
        public bool DiagonalOperation { get { return diagonalOperation; } set { diagonalOperation = value; } }

        // matrix per operation
        private static int[] matrixesPerOperation = { 2, 2, 1, 1, 2, 2, 2, 1 };
        public int[] MatrixesPerOperation { get { return matrixesPerOperation; } }

        private static int[] decimalMatrixesPerOperation = { 2, 2, 2, 1, 1, 2 };
        public int[] DecimalMatrixesPerOperation { get { return decimalMatrixesPerOperation; } }

        // ---- Functions ----

        /// <summary>
        /// Converts boolean to string
        /// </summary>
        /// <param name="value">convertable string</param>
        /// <returns><b>bool</b></returns>
        protected string BoolToString(bool value)
        {
            return Convert.ToInt32(value).ToString();
        }

        /// <summary>
        /// Converts boolean to string
        /// </summary>
        /// <param name="value"></param>
        /// <returns><b>boolean</b></returns>
        public bool StringToBool(string value)
        {
            return Convert.ToBoolean(Convert.ToInt32(value));
        }

        /// <summary>
        /// Changes global states to fill matrix
        /// </summary>
        /// <param name="valueToChange">For matrix it would be like "0"(false) and "1"(true)</param>
        /// <param name="operationToChange">Simple(false) or diagonal(true) operation</param>
        public void ChangeState(bool valueToChange, bool operationToChange)
        {
            MatrixValue = valueToChange;
            DiagonalOperation = operationToChange;
        }

        /// <summary>
        /// Checks if matrix is fully filled 
        /// </summary>
        private bool FillCheck(TextBox[][,] allMatrixes, List<int> selectedMatrixes, ref bool firstMatrixValue, ref bool secondMatrixValue, int i, int y)
        {
            try
            {
                if (selectedMatrixes.Count == 2)
                {
                    firstMatrixValue = StringToBool(allMatrixes[selectedMatrixes[0]][i, y].Text);
                    secondMatrixValue = StringToBool(allMatrixes[selectedMatrixes[1]][i, y].Text);
                }
                else
                {
                    firstMatrixValue = StringToBool(allMatrixes[selectedMatrixes[0]][i, y].Text);
                }
                return true;
            }
            catch
            {
                MessageBox.Show("Please, fill selected matrixes", "Wait!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        /// <summary>
        /// Checks if matrix is fully filled 
        /// </summary>
        private bool FillDecimalCheck(TextBox[][,] allMatrixes, List<int> selectedMatrixes, ref int firstMatrixValue, ref int secondMatrixValue, int i, int y)
        {
            try
            {
                if (selectedMatrixes.Count == 2)
                {
                    firstMatrixValue = Convert.ToInt32(allMatrixes[selectedMatrixes[0]][i, y].Text);
                    secondMatrixValue = Convert.ToInt32(allMatrixes[selectedMatrixes[1]][i, y].Text);
                }
                else
                {
                    firstMatrixValue = Convert.ToInt32(allMatrixes[selectedMatrixes[0]][i, y].Text);
                }
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
        public bool DoOperation(TextBox[][,] allMatrixes, List<int> selectedMatrixes, int selectedOperation, int i, int y, TextBox[,] resultMatrix)
        {
            bool firstMatrixValue = false;
            bool secondMatrixValue = false;

            if (!FillCheck(allMatrixes, selectedMatrixes, ref firstMatrixValue, ref secondMatrixValue, i, y)) return false;

            TextBox[,] operatedMatrix = allMatrixes[selectedMatrixes[0]];

            switch (selectedOperation)
            {
                case 0:
                    resultMatrix[i, y].Text = BoolToString(firstMatrixValue && secondMatrixValue);
                    break;
                case 1:
                    resultMatrix[i, y].Text = BoolToString(firstMatrixValue || secondMatrixValue);
                    break;
                case 2:
                    resultMatrix[i, y].Text = BoolToString(!firstMatrixValue);
                    break;
                case 3:
                    resultMatrix[y, i].Text = BoolToString(firstMatrixValue);
                    break;
                case 4:
                    resultMatrix[i, y].Text = BoolToString(firstMatrixValue && !secondMatrixValue);
                    break;
                case 5:
                    bool firstPart = firstMatrixValue && !secondMatrixValue;
                    bool secondPart = secondMatrixValue && !firstMatrixValue;

                    resultMatrix[i, y].Text = BoolToString(firstPart || secondPart);
                    break;
                case 6:
                    bool founded = false;

                    for (int k = 0; k < resultMatrix.GetLength(0); k++)
                    {
                        if (Convert.ToInt32(operatedMatrix[i, k].Text) == 1 && Convert.ToInt32(operatedMatrix[k, y].Text) == 1)
                        {
                            founded = true;
                            break;
                        }
                    }

                    resultMatrix[i, y].Text = founded ? "1" : "0";
                    break;
                case 7:
                    resultMatrix[i, y].Text = BoolToString(firstMatrixValue);
                    break;
            }
            return true;
        }

        public bool DoDecimalOperation(TextBox[][,] allMatrixes, List<int> selectedMatrixes, int selectedOperation, int i, int y, TextBox[,] resultMatrix, AttributesCheck attributes)
        {
            int firstMatrixValue = 0;
            int secondMatrixValue = 0;

            if (!FillDecimalCheck(allMatrixes, selectedMatrixes, ref firstMatrixValue, ref secondMatrixValue, i, y)) return false;

            resultMatrix[i, y].Text = Convert.ToString(firstMatrixValue + secondMatrixValue);

            switch (selectedOperation)
            {
                case 0:
                    if (attributes.IsAdditive) resultMatrix[i, y].Text = Convert.ToString((firstMatrixValue + secondMatrixValue) / 2);
                    else if (attributes.IsMultiplicative) resultMatrix[i, y].Text = Convert.ToString(Math.Sqrt(firstMatrixValue * secondMatrixValue));
                    else
                    {
                        MessageBox.Show("Your matrixes is not additive or multiplicative!", "Whoops", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    break;
                case 1:
                    if (attributes.IsAdditive)
                    {
                        if (firstMatrixValue > 0 && secondMatrixValue > 0) resultMatrix[i, y].Text = Convert.ToString((firstMatrixValue + secondMatrixValue) / 2);
                        else if (firstMatrixValue > 0) resultMatrix[i, y].Text = Convert.ToString(firstMatrixValue);
                        else if (secondMatrixValue > 0) resultMatrix[i, y].Text = Convert.ToString(secondMatrixValue);
                        else resultMatrix[i, y].Text = "0";
                    }
                    else if (attributes.IsMultiplicative)
                    {
                        if (firstMatrixValue > 0 && secondMatrixValue > 0) resultMatrix[i, y].Text = Convert.ToString(Math.Sqrt(firstMatrixValue * secondMatrixValue));
                        else if (firstMatrixValue > 0) resultMatrix[i, y].Text = Convert.ToString(firstMatrixValue);
                        else if (secondMatrixValue > 0) resultMatrix[i, y].Text = Convert.ToString(secondMatrixValue);
                        else resultMatrix[i, y].Text = "0";
                    }
                    else
                    {
                        MessageBox.Show("Your matrixes is not additive or multiplicative!", "Whoops", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    break;
                case 2:
                    if (attributes.IsAdditive || attributes.IsMultiplicative) resultMatrix[i, y].Text = Convert.ToString(firstMatrixValue - secondMatrixValue);
                    else
                    {
                        MessageBox.Show("Your matrixes is not additive or multiplicative!", "Whoops", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    break;
                case 3:
                    if (attributes.IsAdditive || attributes.IsMultiplicative) resultMatrix[i, y].Text = firstMatrixValue > 0 ? "0" : "1";
                    else
                    {
                        MessageBox.Show("Your matrixes is not additive or multiplicative!", "Whoops", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    break;
                case 4:
                    if (attributes.IsAdditive || attributes.IsMultiplicative) resultMatrix[y, i].Text = Convert.ToString(firstMatrixValue);
                    else
                    {
                        MessageBox.Show("Your matrixes is not additive or multiplicative!", "Whoops", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    break;
                case 5:
                    TextBox[,] firstMatrix = allMatrixes[selectedMatrixes[0]];
                    TextBox[,] secondMatrix = allMatrixes[selectedMatrixes[1]];

                    if (attributes.IsAdditive)
                    {
                        int sum = 0;

                        for (int k = 0; k < firstMatrix.GetLength(0); k++)
                        {
                            sum += Convert.ToInt32(firstMatrix[i, k].Text) + Convert.ToInt32(secondMatrix[k, y].Text);
                        }

                        resultMatrix[i, y].Text = Convert.ToString(1 / 5 + sum);
                    }
                    else if (attributes.IsMultiplicative)
                    {
                        int sum = 0;

                        for (int k = 0; k < firstMatrix.GetLength(0); k++)
                        {
                            sum += Convert.ToInt32(firstMatrix[i, k].Text) * Convert.ToInt32(secondMatrix[k, y].Text);
                        }

                        resultMatrix[i, y].Text = Convert.ToString(sum);
                    }
                    else
                    {
                        MessageBox.Show("Your matrixes is not additive or multiplicative!", "Whoops", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    break;
            }

            return true;
        }
    }
}
