using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    internal class RatioCut
    {

        // ---- Functions ----

        /// <summary>
        /// Shows result matrix cuts
        /// </summary>
        /// <param name="cutMatrix">result matrix</param>
        /// <param name="mode">0 - vertical, 1 - horizontal</param>
        private void ShowCut(TextBox[,] cutMatrix, int mode)
        {
            string message;
            List<string> result = CalculateCuts(cutMatrix, mode);

            message = !result.Any() ? $"No {(mode == 0 ? "vertical" : "horizontal")} cuts founded!" : String.Join("\n", result.ToArray());
            MessageBox.Show(message, $"{(mode == 0 ? "Vertical" : "Horizontal")} cuts");
        }

        /// <summary>
        /// Calculates cuts
        /// </summary>
        /// <param name="cutMatrix">result matrix</param>
        /// <param name="mode">0 - vertical, 1 - horizontal</param>
        private List<string> CalculateCuts(TextBox[,] cutMatrix, int mode)
        {
            int step = 0;
            bool hasOne = false; // if cut founded
            List<string> result = new List<string>();

            do
            {
                for (int i = 0; i < cutMatrix.GetLength(0); i++)
                {
                    int num = Convert.ToInt32(mode == 0 ? cutMatrix[i, step].Text : cutMatrix[step, i].Text);

                    if (num == 1) hasOne = true;
                    // if 1 add to results
                    if (hasOne)
                    {
                        result.Add($"R({step}) = ({i})");
                        break;
                    }

                }

                step++;
                hasOne = false;

            } while (step != cutMatrix.GetLength(0));

            return result;
        }


    }
}
