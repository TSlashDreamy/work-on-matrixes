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
        public RatioNarrowing() { }
        
        /// <summary>
        /// Checks if user typed something into textBoxes
        /// </summary>
        public bool checkSelection(TextBox narrow1, TextBox narrow2, TextBox narrow3, ref int value1, ref int value2, ref int value3)
        {
            try
            {
                value1 = Convert.ToInt32(narrow1.Text);
                value2 = Convert.ToInt32(narrow2.Text);
                value3 = Convert.ToInt32(narrow3.Text);

                return true;
            } catch
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if element from matrix is in narrow operation
        /// </summary>
        private bool checkNarrows(int narrow1, int narrow2, int narrow3, int i, int y)
        {
            bool state = false;

            if (i + 1 != narrow1 && i + 1 != narrow2 && i + 1 != narrow3)
            {
                if (y + 1 != narrow1 && y + 1 != narrow2 && y + 1 != narrow3)
                {
                    return false;
                }
                return false;
            }
            if (y + 1 != narrow1 && y + 1 != narrow2 && y + 1 != narrow3)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Shows narrowed matrix
        /// </summary>
        public void showNarrowing(TextBox[,] resultMatrix, TextBox narrow1, TextBox narrow2, TextBox narrow3)
        {
            List<string> values = new List<string>();
            int value1 = 0;
            int value2 = 0;
            int value3 = 0;

            if(!checkSelection(narrow1, narrow2, narrow3, ref value1, ref value2, ref value3))
            {
                MessageBox.Show("Please, fill narrow fields", "Wait!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            } 
               

            for (int i = 0; i < resultMatrix.GetLength(0); i++)
            {
                for (int y = 0; y < resultMatrix.GetLength(1); y++)
                {
                    if (checkNarrows(value1, value2, value3, i, y))
                    {
                        values.Add(resultMatrix[i, y].Text);
                    }
                }
            }

            NarrowedMatrix newForm = new NarrowedMatrix(values);
            newForm.ShowDialog();
        }


    }
}
