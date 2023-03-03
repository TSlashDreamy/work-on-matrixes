using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    internal class RatioCut
    {
        private List<string> cutList;
        public List<string> CutList { get { return cutList; } set { cutList = value; } }

        public RatioCut()
        {
            cutList = new List<string>();
        }

        public RatioCut(List<string> cuts)
        {
            cutList = cuts;
        }

        // ---- Functions ----

        /// <summary>
        /// Return matrix cuts
        /// </summary>
        /// <param name="cutMatrix">result matrix</param>
        /// <param name="mode">0 - vertical, 1 - horizontal</param>
        public string GetCuts(TextBox[,] cutMatrix, int mode)
        {
            string message;
            List<string> result = CalculateCuts(cutMatrix, mode);
            
            message = !result.Any() ? $"No {(mode == 0 ? "vertical" : "horizontal")} cuts founded!" : String.Join("\n", result.ToArray());
            
            return message;
        }

        /// <summary>
        /// Calculates cuts
        /// </summary>
        /// <param name="cutMatrix">result matrix</param>
        /// <param name="mode">0 - vertical, 1 - horizontal</param>
        public List<string> CalculateCuts(TextBox[,] cutMatrix, int mode)
        {
            int step = 0;
            List<int> temp = new List<int>();
            List<string> empty = new List<string>();
            List<string> cuts = new List<string>();
            List<string> result = new List<string>();
            
            do
            {
                for (int i = 0; i < cutMatrix.GetLength(0); i++)
                {
                    int num = Convert.ToInt32(mode == 0 ? cutMatrix[i, step].Text : cutMatrix[step, i].Text);

                    if (num == 1) temp.Add(i);
                }
                
                if (!temp.Any())
                {
                    empty.Add($"R({step})");
                } else
                {
                    cuts.Add($"R({step}) = ({String.Join(", ", temp.ToArray())})");
                }

                step++;
                temp.Clear();
                
            } while (step != cutMatrix.GetLength(0));

            result.Add($"{String.Join(", ", empty.ToArray())} = (Empty)\n{String.Join("\n", cuts.ToArray())}");
            cutList = result;
            return result;
        }


    }
}
