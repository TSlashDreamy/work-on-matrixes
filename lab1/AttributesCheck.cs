using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    internal class AttributesCheck
    {
        private Ratio converts = new Ratio();

        // ----- Variables -----
        private bool isReflexive;
        public bool IsReflexive { get { return isReflexive; } }

        private bool isAntiReflexive;
        public bool IsAntiReflexive { get { return isAntiReflexive; } }

        private bool isSymmetric;
        public bool IsSymmetric { get { return isSymmetric; } }

        private bool isAsymmetric;
        public bool IsAsymmetric { get { return isAsymmetric; } }

        private bool isAntiSymmetric;
        public bool IsAntiSymmetric { get { return isAntiSymmetric; } }

        private bool isTransitive;
        public bool IsTransitive { get { return isTransitive; } }

        private bool isAcyclic;
        public bool IsAcyclic { get { return isAcyclic; } }

        private bool isCoherent;
        public bool IsCoherent { get { return isCoherent; } }

        public AttributesCheck()
        {
            isReflexive = false;
            isAntiReflexive = false;
            isSymmetric = false;
            isAsymmetric = false;
            isAntiSymmetric = false;
            isTransitive = false;
            isAcyclic = false;
            isCoherent = false;
        }

        // ----- Basic matrixes -----
        private int[,] diagonal = { 
            { 1, 0, 0, 0, 0 },
            { 0, 1, 0, 0, 0 },
            { 0, 0, 1, 0, 0 },
            { 0, 0, 0, 1, 0 },
            { 0, 0, 0, 0, 1 }
        };

        // ----- Functions -----

        private bool reflexiveCheck(TextBox[,] checkMatrix, int[,] basicMatrix)
        {
            for (int i = 0; i < basicMatrix.GetLength(0); i++)
                if (!(basicMatrix[i, i] == Convert.ToInt32(checkMatrix[i, i].Text))) return false;
                
            return true;
        }

        private bool antiReflexiveCheck(TextBox[,] checkMatrix, int[,] basicMatrix)
        {
            for (int i = 0; i < basicMatrix.GetLength(0); i++)
                if (Convert.ToBoolean(basicMatrix[i, i]) && converts.StringToBool(checkMatrix[i, i].Text)) return false;

            return true;
        }

        private bool symmetricCheck(TextBox[,] checkMatrix)
        {
            for (int i = 0; i < checkMatrix.GetLength(0); i++)
                for (int j = 0; j < checkMatrix.GetLength(1); j++)
                    if (!(checkMatrix[i, j].Text == checkMatrix[j, i].Text)) return false;

            return true;
        }

        private bool asymetricCheck(TextBox[,] checkMatrix)
        {
            for (int i = 0; i < checkMatrix.GetLength(0); i++)
                for (int j = 0; j < checkMatrix.GetLength(1); j++) 
                    if (converts.StringToBool(checkMatrix[i, j].Text) && converts.StringToBool(checkMatrix[j, i].Text)) return false;

            return true;
        }

        private bool antiSymmetricCheck(TextBox[,] checkMatrix, int[,] basicMatrix)
        {
            for (int i = 0; i < checkMatrix.GetLength(0); i++)
                for (int j = 0; j < checkMatrix.GetLength(1); j++)
                {
                    if (i == j) continue;
                    if (converts.StringToBool(checkMatrix[i, j].Text) && converts.StringToBool(checkMatrix[j, i].Text)) return false;
                }

            return true;
        }

        private bool transitiveCheck(TextBox[,] checkMatrix)
        {
            bool isTransitive = true;

            for (int i = 0; i < checkMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < checkMatrix.GetLength(1); j++)
                {
                    if (checkMatrix[i, j].Text == "1")
                    {
                        for (int k = 0; k < checkMatrix.GetLength(1); k++)
                        {
                            if (checkMatrix[j, k].Text == "1" && checkMatrix[i, k].Text != "1")
                            {
                                isTransitive = false;
                                break;
                            }
                        }
                    }
                }
            }

            return isTransitive;
        }

        private bool acyclicCheck(TextBox[,] checkMatrix)
        {
            int n = checkMatrix.GetLength(0);
            int[] indegrees = new int[n];
            List<int> sorted = new List<int>();

            // Calculate in-degrees for all vertices
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (Convert.ToInt32(checkMatrix[i, j].Text) != 0)
                    {
                        indegrees[j]++;
                    }
                }
            }

            // Find all vertices with no incoming edges
            Queue<int> queue = new Queue<int>();
            for (int i = 0; i < n; i++)
            {
                if (indegrees[i] == 0)
                {
                    queue.Enqueue(i);
                }
            }

            // Perform topological sort
            while (queue.Count > 0)
            {
                int vertex = queue.Dequeue();
                sorted.Add(vertex);

                for (int j = 0; j < n; j++)
                {
                    if (Convert.ToInt32(checkMatrix[vertex, j].Text) != 0)
                    {
                        indegrees[j]--;
                        if (indegrees[j] == 0)
                        {
                            queue.Enqueue(j);
                        }
                    }
                }
            }

            // If not all vertices were sorted, there is a cycle
            return sorted.Count == n;
        }

        private bool coherentCheck(TextBox[,] checkMatrix)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    // If the element on the main diagonal is not equal to 1, the ratio is not coherent
                    if (i == j && Convert.ToInt32(checkMatrix[i, j].Text) != 1) return false; 
                    // if the element is not on the main diagonal and is not equal to 0 or 1, the ratio is not coherent
                    if (i != j && Convert.ToInt32(checkMatrix[i, j].Text) != 0 && Convert.ToInt32(checkMatrix[i, j].Text) != 1) return false;
                }
            }

            return true;
        }

        public void CheckAll(TextBox[,] checkMatrix)
        {
            isReflexive = reflexiveCheck(checkMatrix, diagonal);
            isAntiReflexive = antiReflexiveCheck(checkMatrix, diagonal);
            isSymmetric = symmetricCheck(checkMatrix);
            isAsymmetric = asymetricCheck(checkMatrix);
            isAntiSymmetric = antiSymmetricCheck(checkMatrix, diagonal);
            isTransitive = transitiveCheck(checkMatrix);
            isAcyclic = acyclicCheck(checkMatrix);
            isCoherent = coherentCheck(checkMatrix);
        }

        public void ShowAttributes(TextBox[,] checkMatrix)
        {
            CheckAll(checkMatrix);

            string reflexiveMsg = $"{(isReflexive ? "✔️" : "❌")} - Reflexive\n";
            string antiReflexiveMsg = $"{(isAntiReflexive ? "✔️" : "❌")} - Antireflexive\n";
            string symmetricMsg = $"{(isSymmetric ? "✔️" : "❌")} - Symmetric\n";
            string asymmetricMsg = $"{(isAsymmetric ? "✔️" : "❌")} - Asymmetric\n";
            string antiSymmetricMsg = $"{(isAntiSymmetric ? "✔️" : "❌")} - Antisymmetric\n";
            string transitiveMsg = $"{(isTransitive ? "✔️" : "❌")} - Transitive\n";
            string acyclicMsg = $"{(isAcyclic ? "✔️" : "❌")} - Acyclic\n";
            string coherentMsg = $"{(isCoherent ? "✔️" : "❌")} - Coherent\n";

            string message = reflexiveMsg + antiReflexiveMsg + symmetricMsg + asymmetricMsg + antiSymmetricMsg + transitiveMsg + acyclicMsg + coherentMsg;
            MessageBox.Show(message, "Attribute check");
        }


    }
}
