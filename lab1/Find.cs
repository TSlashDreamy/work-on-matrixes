using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    internal class Find
    {
        private Ratio convert = new Ratio();

        private string[,] Ps = new string[5, 5];
        private string[,] Pa = new string[5, 5];
        private string[,] transitiveMatrix = new string[5, 5];
        private string[,] reachMatrix = new string[5, 5];
        private string[,] mutualReachMatrix = new string[5, 5];

        private string[,] diagonalMatrix =
        {
            { "1", "0", "0", "0", "0" },
            { "0", "1", "0", "0", "0" },
            { "0", "0", "1", "0", "0" },
            { "0", "0", "0", "1", "0" },
            { "0", "0", "0", "0", "1" }
        };

        public void FindPS(TextBox[,] checkMatrix)
        {
            for (int i = 0; i < checkMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < checkMatrix.GetLength(1); j++)
                {
                    bool currentState = convert.StringToBool(checkMatrix[i, j].Text) && convert.StringToBool(checkMatrix[j, i].Text);
                    Ps[i, j] = Convert.ToString(Convert.ToInt32(currentState));
                }
            }

            // show matrix
            MatrixUI matrixForm = new MatrixUI(Ps, "symmetrical component");
            matrixForm.Show();
        }

        public void FindPA(TextBox[,] checkMatrix)
        {
            for (int i = 0; i < checkMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < checkMatrix.GetLength(1); j++)
                {
                    if (Ps[i, j] == null) FindPS(checkMatrix);
                    Pa[i, j] = checkMatrix[i, j].Text == Ps[i, j] && Ps[i, j] == "1" ? "0" : checkMatrix[i, j].Text;
                }
            }

            // show matrix
            MatrixUI matrixForm = new MatrixUI(Pa, "asymmetrical component");
            matrixForm.Show();
        }

        // using Floyd Warshall method
        public void TransitiveClosure(TextBox[,] checkMatrix)
        {
            // Create a copy of the matrix for transitive closure
            string[,] closure = cloneMatrix(checkMatrix);

            // Pass through all possible peaks
            for (int k = 0; k < checkMatrix.GetLength(0); k++)
            {
                for (int i = 0; i < checkMatrix.GetLength(0); i++)
                {
                    for (int j = 0; j < checkMatrix.GetLength(1); j++)
                    {
                        // If the vertices i and j are not connected, but are connected through the vertex k,
                        // then add the edge between i and j
                        if (closure[i, k] == "1" && closure[k, j] == "1" && closure[i, j] == "0")
                        {
                            closure[i, j] = "1";
                        }
                    }
                }
            }

            transitiveMatrix = closure;

            // show matrix
            MatrixUI matrixForm = new MatrixUI(closure, "transitive closure");
            matrixForm.Show();
        }

        private string[,] cloneMatrix(TextBox[,] matrixToClone)
        {
            string[,] matrixToReturn = new string[5, 5];

            for (int i = 0; i < matrixToClone.GetLength(0); i++)
            {
                for (int j = 0; j < matrixToClone.GetLength(1); j++)
                {
                    matrixToReturn[i, j] = matrixToClone[i, j].Text;
                }
            }

            return matrixToReturn;
        }

        public void Reach(TextBox[,] checkMatrix)
        {
            for (int i = 0; i < transitiveMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < transitiveMatrix.GetLength(1); j++)
                {
                    if (transitiveMatrix[i, j] == null) TransitiveClosure(checkMatrix);
                    bool state = Convert.ToBoolean(Convert.ToInt32(diagonalMatrix[i, j])) || Convert.ToBoolean(Convert.ToInt32(transitiveMatrix[i, j]));
                    reachMatrix[i, j] = Convert.ToString(Convert.ToInt32(state));
                }
            }

            // show matrix
            MatrixUI matrixForm = new MatrixUI(reachMatrix, "reach");
            matrixForm.Show();
        }

        public void MutualRich(TextBox[,] checkMatrix)
        {
            for (int i = 0; i < reachMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < reachMatrix.GetLength(1); j++)
                {
                    if (reachMatrix[i, j] == null) Reach(checkMatrix);
                    bool state = Convert.ToBoolean(Convert.ToInt32(reachMatrix[i, j])) && Convert.ToBoolean(Convert.ToInt32(reachMatrix[j, i]));
                    mutualReachMatrix[i, j] = Convert.ToString(Convert.ToInt32(state));
                }
            }

            // show matrix
            MatrixUI matrixForm = new MatrixUI(mutualReachMatrix, "mutual reach");
            matrixForm.Show();
        }

        private void checkEmpty(TextBox[,] checkMatrix)
        {
            if (transitiveMatrix[0, 0] == null) TransitiveClosure(checkMatrix);
            if (reachMatrix[0, 0] == null) Reach(checkMatrix);
            if (mutualReachMatrix[0, 0] == null) MutualRich(checkMatrix);
        }

        public string[,] GetEquivalentRatio(TextBox[,] checkMatrix)
        {
            // transitive -> reach -> mutual reach
            checkEmpty(checkMatrix);

            return mutualReachMatrix;
        }

    }
}
