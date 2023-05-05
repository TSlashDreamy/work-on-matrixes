using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab1
{
    public partial class DecisionUI : Form
    {
        private static int defaultISize = 4;
        private static int defaultYSize = 4;

        TextBox[] Qa1 = new TextBox[3];
        TextBox[] Qa2 = new TextBox[3];
        TextBox[] Qa3 = new TextBox[3];
        TextBox[] Qa4 = new TextBox[3];
        TextBox[] Qe = new TextBox[3];

        List<TextBox[]> variables = new List<TextBox[]>();

        Right right = new Right();
        RatioMatrix resultMatrix = new RatioMatrix(defaultISize, defaultYSize);

        // min/max search
        MinMaxFind minMax = new MinMaxFind();

        public DecisionUI()
        {
            InitializeComponent();
            right.SafeCheck(rights.Text);

            TextBox[] Qa1_init = { qa1_1, qa1_2, qa1_3 };
            Qa1 = Qa1_init;
            variables.Add(Qa1);

            TextBox[] Qa2_init = { qa2_1, qa2_2, qa2_3 };
            Qa2 = Qa2_init;
            variables.Add(Qa2);

            TextBox[] Qa3_init = { qa3_1, qa3_2, qa3_3 };
            Qa3 = Qa3_init;
            variables.Add(Qa3);

            TextBox[] Qa4_init = { qa4_1, qa4_2, qa4_3 };
            Qa4 = Qa4_init;
            variables.Add(Qa4);

            TextBox[] Qe_init = { qe_1, qe_2, qe_3 };
            Qe = Qe_init;
            variables.Add(Qe);

            TextBox[,] result_init_elements = new TextBox[4, 4] {
                { result_value1, result_value2, result_value3, result_value4 },
                { result_value6, result_value7, result_value8, result_value9 },
                { result_value11, result_value12, result_value13, result_value14 },
                { result_value16, result_value17, result_value18, result_value19 }
            };

            resultMatrix.Matrix = result_init_elements;
        }

        private void DecisionUI_Load(object sender, EventArgs e)
        {
            try
            {
                right.SafeWarning(decision_btn, rights);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString().Split('\n')[1], exc.Message, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void decision_btn_Click(object sender, EventArgs e)
        {
            right.SafeExit();

            int selectedMechanism = decisionMode_box.SelectedIndex;

            switch (selectedMechanism)
            {
                case 0:
                    parettoOperation();
                    break;
                case 1:
                    slaterOperation();
                    break;
                case 2:
                    etalonOperation();
                    break;
                default:
                    MessageBox.Show("Please, select Decision mechanism!", "Wait!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
            }

            // searching min/max
            if (minmax_CheckBox.Checked)
            {
                int minMaxOperation = minmax_box.SelectedIndex;
                minMax.ShowResult(resultMatrix.Matrix, minMaxOperation, true);
            }
        }

        private void parettoOperation()
        {
            bool allMoreOrEqual = true;
            bool oneStrictMore = false;

            for (int i = 0; i < resultMatrix.Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < resultMatrix.Matrix.GetLength(1); j++)
                {
                    for (int k = 0; k < Qa1.Length; k++)
                    {
                        if (!(Convert.ToInt32(variables[i][k].Text) >= Convert.ToInt32(variables[j][k].Text))) allMoreOrEqual = false;
                        if (Convert.ToInt32(variables[i][k].Text) > Convert.ToInt32(variables[j][k].Text)) oneStrictMore = true;
                    }
                    if (allMoreOrEqual && oneStrictMore) resultMatrix.Matrix[i, j].Text = "1";
                    else resultMatrix.Matrix[i, j].Text = "0";
                    allMoreOrEqual = true;
                    oneStrictMore = false;
                }
            }
        }

        private void slaterOperation()
        {
            bool allStrictMore = true;

            for (int i = 0; i < resultMatrix.Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < resultMatrix.Matrix.GetLength(1); j++)
                {
                    for (int k = 0; k < Qa1.Length; k++)
                    {
                        if (!(Convert.ToInt32(variables[i][k].Text) > Convert.ToInt32(variables[j][k].Text))) allStrictMore = false;
                    }
                    if (allStrictMore) resultMatrix.Matrix[i, j].Text = "1";
                    else resultMatrix.Matrix[i, j].Text = "0";
                    allStrictMore = true;
                }
            }
        }

        private void etalonOperation()
        {
            int firstSum = 0;
            int secondSum = 0;

            for (int i = 0; i < resultMatrix.Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < resultMatrix.Matrix.GetLength(1); j++)
                {
                    for (int k = 0; k < Qa1.Length; k++)
                    {
                        int firstNum = Convert.ToInt32(variables[i][k].Text);
                        int secondNum = Convert.ToInt32(variables[j][k].Text);
                        int etalonNum = Convert.ToInt32(Qe[k].Text);

                        firstSum += Math.Abs(firstNum - etalonNum);
                        secondSum += Math.Abs(secondNum - etalonNum);

                        if (k == Qa1.Length - 1)
                        {
                            if (firstSum <= secondSum) resultMatrix.Matrix[i, j].Text = "1";
                            else resultMatrix.Matrix[i, j].Text = "0";
                            firstSum = 0;
                            secondSum = 0;
                        }

                    }
                }
            }
        }

        private void switchEtalonElements(bool state)
        {
            label5.Enabled = state;
            qe_1.Enabled = state;
            qe_2.Enabled = state;
            qe_3.Enabled = state;
        }

        private void decisionMode_box_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int selectedMechanism = decisionMode_box.SelectedIndex;

            if (selectedMechanism == 2) switchEtalonElements(true);
            else switchEtalonElements(false);

        }

        private void minmax_CheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            // oh my god, what have i done ⚆_⚆
            label22.Enabled = resultMatrix.StringToBool(sender.ToString()[^1..]);
            minmax_box.Enabled = resultMatrix.StringToBool(sender.ToString()[^1..]);
        }

        private void clearValues_button_Click(object sender, EventArgs e)
        {
            foreach (TextBox[] variable in variables)
            {
                foreach (TextBox box in variable)
                {
                    box.Text = "";
                }
            }
        }

        private void values_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool isValid = true;

            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == 8))
            {
                isValid = false;
                MessageBox.Show($"Please, enter only positive integer numbers!", "Hey", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            e.Handled = !(char.IsDigit(e.KeyChar) || e.KeyChar == 8);

            if (!isValid) return;
            switchToNext(sender);
        }

        private void switchToNext(object sender)
        {
            for (int arrI = 0; arrI < variables.Count; arrI++)
            {
                for (int i = 0; i < variables[arrI].Length; i++)
                {
                    if (sender == variables[arrI][i])
                    {
                        if (i < variables[arrI].Length - 1)
                        {
                            variables[arrI][i + 1].Select();
                        }
                        else
                        {
                            if (variables.Count > arrI + 1) variables[arrI + 1][0].Select();
                            else variables[0][0].Select();
                        }
                    }
                }
            }
        }

    }
}
