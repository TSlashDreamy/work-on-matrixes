using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    internal class AttributesCheck
    {
        private Ratio converts = new Ratio();

        private bool isReflexive;
        private bool isAntiReflexive;
        private bool isSymmetric;
        private bool isAsymmetric;
        private bool isAntiSymmetric;

        public AttributesCheck()
        {
            isReflexive = false;
            isAntiReflexive = false;
            isSymmetric = false;
            isAsymmetric = false;
            isAntiSymmetric = false;
        }

        private int[,] diagonal = { 
            { 1, 0, 0, 0, 0 },
            { 0, 1, 0, 0, 0 },
            { 0, 0, 1, 0, 0 },
            { 0, 0, 0, 1, 0 },
            { 0, 0, 0, 0, 1 }
        };

        private int[,] antiDiagonal = {
            { 0, 1, 1, 1, 1 },
            { 1, 0, 1, 1, 1 },
            { 1, 1, 0, 1, 1 },
            { 1, 1, 1, 0, 1 },
            { 1, 1, 1, 1, 0 }
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

        private void checkAll(TextBox[,] checkMatrix)
        {
            isReflexive = reflexiveCheck(checkMatrix, diagonal);
            isAntiReflexive = antiReflexiveCheck(checkMatrix, diagonal);
            isSymmetric = symmetricCheck(checkMatrix);
            isAsymmetric = asymetricCheck(checkMatrix);
            isAntiSymmetric = antiSymmetricCheck(checkMatrix, diagonal);
        }

        public void ShowAttributes(TextBox[,] checkMatrix)
        {
            checkAll(checkMatrix);

            string reflexiveMsg = $"{(isReflexive ? "✔️" : "❌")} - Reflexive\n";
            string antiReflexiveMsg = $"{(isAntiReflexive ? "✔️" : "❌")} - Antireflexive\n";
            string symmetricMsg = $"{(isSymmetric ? "✔️" : "❌")} - Symmetric\n";
            string asymmetricMsg = $"{(isAsymmetric ? "✔️" : "❌")} - Asymmetric\n";
            string antiSymmetricMsg = $"{(isAntiSymmetric ? "✔️" : "❌")} - Antisymmetric\n";

            string message = reflexiveMsg + antiReflexiveMsg + symmetricMsg + asymmetricMsg + antiSymmetricMsg;
            MessageBox.Show(message, "Attribute check");
        }


    }
}
