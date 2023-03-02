using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    internal class RatioMatrix
    {
        // matrix states
        bool value = false; // for matrix it would be like "0" and "1"
        bool diagonalOperation = false;

        private TextBox[,] matrix;
        public TextBox[,] Matrix { get { return matrix; } }

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

            if (diagonalOperation)
            {
                DiagonalFill(matrixToFill, operation);
            }
            else
            {
                SimpleFill(matrixToFill, operation);
            }
        }

        /// <summary>
        /// Changes global states to fill matrix
        /// </summary>
        /// <param name="valueToChange">For matrix it would be like "0"(false) and "1"(true)</param>
        /// <param name="operationToChange">Simple(false) or diagonal(true) operation</param>
        private void ChangeState(bool valueToChange, bool operationToChange)
        {
            value = valueToChange;
            diagonalOperation = operationToChange;
        }



    }
}
