using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    internal class RatioMatrix : Ratio
    {
        private TextBox[,] matrix;
        public TextBox[,] Matrix { get { return matrix; } set { matrix = value; } }

        public RatioMatrix() { }

        public RatioMatrix(TextBox[,] elements)
        {
            matrix = elements;
        }

        // Functions

        /// <summary>
        /// Fills selected matrix with 0 and 1 by selected operation
        /// </summary>
        /// <param name="matrixToFill"></param>
        /// <param name="operation">Available: "full", "empty", "diagonal", "anti-diagonal"</param>
        /// <exception cref="Exception">If operation does not exist</exception>
        private void FillMatrix(TextBox[,] matrixToFill, string operation)
        {
            switch (operation)
            {
                case "full":
                    ChangeState(true, false);
                    break;
                case "diagonal":
                    ChangeState(true, true);
                    break;
                case "empty":
                    ChangeState(false, false);
                    break;
                case "anti-diagonal":
                    ChangeState(false, true);
                    break;
                default:
                    throw new Exception("'" + operation + "' operation is not exist.");
            }

            if (DiagonalOperation)
            {
                DiagonalFill(matrixToFill, operation);
            }
            else
            {
                SimpleFill(matrixToFill, operation);
            }
        }

        /// <summary>
        /// Fills matrix diagonally
        /// </summary>
        /// <param name="matrixToFill"></param>
        /// <param name="operation">Diagonal or antidiagonal</param>
        private void DiagonalFill(TextBox[,] matrixToFill, string operation)
        {
            int step = 0;

            for (int i = 0; i < matrixToFill.GetLength(0); i++)
            {
                for (int y = 0; y < matrixToFill.GetLength(1); y++)
                {
                    if (step == y)
                    {
                        matrixToFill[i, y].Text = BoolToString(MatrixValue);
                    }
                    else
                    {
                        matrixToFill[i, y].Text = BoolToString(!MatrixValue);
                    }
                }
                step++;
            }
        }

        /// <summary>
        /// Fully fills matrix just with one value "0" or "1"
        /// </summary>
        /// <param name="matrixToFill"></param>
        /// <param name="operation">"full" or "empty"</param>
        private void SimpleFill(TextBox[,] matrixToFill, string operation)
        {
            foreach (TextBox matrix_value in matrixToFill)
            {
                matrix_value.Text = BoolToString(MatrixValue);
            }
        }

    }
}
