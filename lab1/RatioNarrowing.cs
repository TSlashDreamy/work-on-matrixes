using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    internal class RatioNarrowing : Ratio
    {
        private TextBox[,] narrowMatrix;
        public TextBox[,] NarrowMatrix { get { return narrowMatrix; } set { narrowMatrix = value; } }

        public RatioNarrowing(int sizeX, int sizeY) { narrowMatrix = new TextBox[sizeX, sizeY]; }

        public RatioNarrowing(TextBox[,] elements)
        {
            narrowMatrix = elements;
        }
        
        /// <summary>
        /// Checks if element from matrix is in narrow operation
        /// </summary>
        private bool checkNarrows(int narrow1, int narrow2, int narrow3, int i, int y)
        {
            if (i != narrow1 && i != narrow2 && i != narrow3)
            {
                if (y != narrow1 && y != narrow2 && y != narrow3)
                {
                    return false;
                }
                return true;
            }
            return true;
        }

        /// <summary>
        /// Shows narrowed matrix
        /// </summary>
        public void showNarrowing(TextBox[,] resultMatrix, int narrow1, int narrow2, int narrow3)
        {
            List<int> newMatrix = new List<int>();

            for (int i = 0; i < resultMatrix.GetLength(0); i++)
            {
                for (int y = 0; y < resultMatrix.GetLength(1); y++)
                {
                    if (checkNarrows(narrow1, narrow2, narrow3, i, y))
                    {
                        int num = Convert.ToInt32(resultMatrix[i, y].Text);

                        newMatrix.Add(num);
                    }
                }
            }

            MessageBox.Show(String.Join("\n", newMatrix.ToArray()), "Narrowed matrix");
        }


    }
}
