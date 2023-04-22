using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    internal class Simmilarity
    {
        int[,] R1;
        int[,] R2;
        int[,] diff;
        double simmilarity;

        public Simmilarity()
        {
            R1 = new int[5, 5];
            R2 = new int[5, 5];
            diff = new int[5, 5];
            simmilarity = 0;
        }

        private bool fillMatrixes(TextBox[][,] allMatrixes, List<int> selectedMatrixes)
        {
            for (int i = 0; i < R1.GetLength(0); i++)
            {
                for (int j = 0; j < R1.GetLength(1); j++)
                {
                    try
                    {
                        R1[i, j] = (Convert.ToInt32(allMatrixes[selectedMatrixes[0]][i, j].Text) == 1) ? 1 : 0;
                        R2[i, j] = (Convert.ToInt32(allMatrixes[selectedMatrixes[1]][i, j].Text) == 1) ? 1 : 0;
                    } catch
                    {
                        MessageBox.Show("Please, fill selected matrixes", "Wait!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
            }
            return true;
        }

        private void calculateDifference()
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    diff[i, j] = Math.Abs(R1[i, j] - R2[i, j]);
                }
            }

        }

        private void calculateSimmilarity()
        {
            int diffSum = 0;

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    diffSum += diff[i, j];
                }
            }

            simmilarity = 1 - (double)diffSum / (5 * 5);

        }

        public bool ShowResult(TextBox[][,] allMatrixes, List<int> selectedMatrixes)
        {
            if (selectedMatrixes.Count != 2)
            {
                MessageBox.Show("Please, select 2 matrixes for simmilarity finding.", "Wait!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!fillMatrixes(allMatrixes, selectedMatrixes)) return false;
            calculateDifference();
            calculateSimmilarity();

            MessageBox.Show($"Simmilarity = {simmilarity}", "Simmilarity of matrixes", MessageBoxButtons.OK);
            return true;
        }

    }
}
