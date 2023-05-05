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
    public partial class DecimalMode : Form
    {
        bool modeChange = false;
        public bool ModeChange { get { return modeChange; } }

        // default matrix sizes (5 x 5)
        private static int defaultISize = 5;
        private static int defaultYSize = 5;

        Right right = new Right();

        // attributes 
        AttributesCheck attributes = new AttributesCheck();

        // matrix's
        RatioMatrix matrixP = new RatioMatrix(defaultISize, defaultYSize);
        RatioMatrix matrixQ = new RatioMatrix(defaultISize, defaultYSize);
        RatioMatrix matrixP1 = new RatioMatrix(defaultISize, defaultYSize);
        RatioMatrix matrixQ1 = new RatioMatrix(defaultISize, defaultYSize);
        RatioMatrix resultMatrix = new RatioMatrix(defaultISize, defaultYSize);

        // searching distance
        Distance distance = new Distance();


        public DecimalMode()
        {
            InitializeComponent();
            right.SafeCheck(rights.Text);

            // all matrixes
            TextBox[,] matrixP_init_elements = new TextBox[5, 5] {
                { matrixP_value1, matrixP_value2, matrixP_value3, matrixP_value4, matrixP_value5 },
                { matrixP_value6, matrixP_value7, matrixP_value8, matrixP_value9, matrixP_value10 },
                { matrixP_value11, matrixP_value12, matrixP_value13, matrixP_value14, matrixP_value15 },
                { matrixP_value16, matrixP_value17, matrixP_value18, matrixP_value19, matrixP_value20 },
                { matrixP_value21, matrixP_value22, matrixP_value23, matrixP_value24, matrixP_value25 }
            };

            TextBox[,] matrixP1_init_elements = new TextBox[5, 5] {
                { matrixP1_value1, matrixP1_value2, matrixP1_value3, matrixP1_value4, matrixP1_value5 },
                { matrixP1_value6, matrixP1_value7, matrixP1_value8, matrixP1_value9, matrixP1_value10 },
                { matrixP1_value11, matrixP1_value12, matrixP1_value13, matrixP1_value14, matrixP1_value15 },
                { matrixP1_value16, matrixP1_value17, matrixP1_value18, matrixP1_value19, matrixP1_value20 },
                { matrixP1_value21, matrixP1_value22, matrixP1_value23, matrixP1_value24, matrixP1_value25 }
            };

            TextBox[,] matrixQ_init_elements = new TextBox[5, 5] {
                { matrixQ_value1, matrixQ_value2, matrixQ_value3, matrixQ_value4, matrixQ_value5 },
                { matrixQ_value6, matrixQ_value7, matrixQ_value8, matrixQ_value9, matrixQ_value10 },
                { matrixQ_value11, matrixQ_value12, matrixQ_value13, matrixQ_value14, matrixQ_value15 },
                { matrixQ_value16, matrixQ_value17, matrixQ_value18, matrixQ_value19, matrixQ_value20 },
                { matrixQ_value21, matrixQ_value22, matrixQ_value23, matrixQ_value24, matrixQ_value25 }
            };

            TextBox[,] matrixQ1_init_elements = new TextBox[5, 5] {
                { matrixQ1_value1, matrixQ1_value2, matrixQ1_value3, matrixQ1_value4, matrixQ1_value5 },
                { matrixQ1_value6, matrixQ1_value7, matrixQ1_value8, matrixQ1_value9, matrixQ1_value10 },
                { matrixQ1_value11, matrixQ1_value12, matrixQ1_value13, matrixQ1_value14, matrixQ1_value15 },
                { matrixQ1_value16, matrixQ1_value17, matrixQ1_value18, matrixQ1_value19, matrixQ1_value20 },
                { matrixQ1_value21, matrixQ1_value22, matrixQ1_value23, matrixQ1_value24, matrixQ1_value25 }
            };

            TextBox[,] result_init_elements = new TextBox[5, 5] {
                { result_value1, result_value2, result_value3, result_value4, result_value5 },
                { result_value6, result_value7, result_value8, result_value9, result_value10 },
                { result_value11, result_value12, result_value13, result_value14, result_value15 },
                { result_value16, result_value17, result_value18, result_value19, result_value20 },
                { result_value21, result_value22, result_value23, result_value24, result_value25 }
            };

            // matrixes
            matrixP.Matrix = matrixP_init_elements;
            matrixP1.Matrix = matrixP1_init_elements;
            matrixQ.Matrix = matrixQ_init_elements;
            matrixQ1.Matrix = matrixQ1_init_elements;
            resultMatrix.Matrix = result_init_elements;
        }

        private void DecimalMode_Load(object sender, EventArgs e)
        {
            try
            {
                right.SafeWarning(showResult_btn, rights);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString().Split('\n')[1], exc.Message, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        // ----- Functions -----

        private bool processResult()
        {
            bool[] matrixesChecked = { matrixP.IsChecked, matrixP1.IsChecked, matrixQ.IsChecked, matrixQ1.IsChecked };
            TextBox[][,] matrixes = { matrixP.Matrix, matrixP1.Matrix, matrixQ.Matrix, matrixQ1.Matrix };
            List<int> selectedMatrixes = new List<int>();

            int selectedOperation = operations_list.SelectedIndex;

            // adding selected matrixes
            for (int i = 0; i < matrixesChecked.Length; i++) if (matrixesChecked[i] == true) selectedMatrixes.Add(i);
            if (!resultMatrix.DecimalSafeCheck(selectedMatrixes, selectedOperation, operation_checkBox.Checked)) return false;

            if (distance_checkbox.Checked) distance.ShowDecimalResult(matrixes, selectedMatrixes);

            if (operation_checkBox.Checked)
            {
                // calculate operation in cycle
                for (int i = 0; i < resultMatrix.Matrix.GetLength(0); i++)
                {
                    for (int y = 0; y < resultMatrix.Matrix.GetLength(1); y++)
                    {
                        if (!resultMatrix.DoDecimalOperation(matrixes, selectedMatrixes, selectedOperation, i, y, resultMatrix.Matrix, attributes)) return false;
                    }
                }
            }

            return true;
        }

        // ----- Button functionality -----

        private void matrixPCheck_CheckStateChanged(object sender, EventArgs e) { matrixP.IsChecked = matrixP.StringToBool(sender.ToString()[^1..]); }
        private void matrixP1Check_CheckStateChanged(object sender, EventArgs e) { matrixP1.IsChecked = matrixP1.StringToBool(sender.ToString()[^1..]); }
        private void matrixQCheck_CheckStateChanged(object sender, EventArgs e) { matrixQ.IsChecked = matrixQ.StringToBool(sender.ToString()[^1..]); }
        private void matrixQ1Check_CheckStateChanged(object sender, EventArgs e) { matrixQ1.IsChecked = matrixQ1.StringToBool(sender.ToString()[^1..]); }

        private void modeSwitch_btn_Click(object sender, EventArgs e)
        {
            modeChange = true;
            this.Hide();
            this.Close();
        }

        private void DecimalMode_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!modeChange) Application.Exit();
        }

        private void matrixR_value25_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool isValid = true;

            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !(e.KeyChar == '-'))
            {
                isValid = false;
                MessageBox.Show($"Please, enter only positive integer numbers!", "Hey", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !(e.KeyChar == '-');

            if (!isValid) return;
            switchToNext(sender);
        }

        private void switchToNext(object sender)
        {
            TextBox[][,] matrixesArray = new TextBox[4][,] { matrixP.Matrix, matrixP1.Matrix, matrixQ.Matrix, matrixQ1.Matrix };

            for (int arrI = 0; arrI < matrixesArray.Length; arrI++)
            {
                for (int i = 0; i < matrixesArray[arrI].GetLength(0); i++)
                {
                    for (int j = 0; j < matrixesArray[arrI].GetLength(1); j++)
                    {
                        if (sender == matrixesArray[arrI][i, j])
                        {
                            if (j < matrixP.Matrix.GetLength(1) - 1)
                            {
                                matrixesArray[arrI][i, j + 1].Select();
                            }
                            else if (i < matrixesArray[arrI].GetLength(0) - 1)
                            {
                                matrixesArray[arrI][i + 1, 0].Select();
                            }
                            else
                            {
                                if (matrixesArray.Length > arrI + 1) matrixesArray[arrI + 1][0, 0].Select();
                                else matrixesArray[0][0, 0].Select();
                            }
                        }
                    }
                }
            }

        }

        private void showResult_btn_Click(object sender, EventArgs e)
        {
            right.SafeExit();

            if (showAttributesCheck.Checked) attributes.ShowDecimalAttributes(matrixP.Matrix);
            if (!processResult()) return;
        }

    }
}
