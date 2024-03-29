﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace lab1
{
    public partial class Form1 : Form
    {
        // default matrix sizes (5 x 5)
        private static int defaultISize = 5;
        private static int defaultYSize = 5;

        Right right = new Right();

        // matrix's
        RatioMatrix matrixP = new RatioMatrix(defaultISize, defaultYSize);
        RatioMatrix matrixQ = new RatioMatrix(defaultISize, defaultYSize);
        RatioMatrix matrixR = new RatioMatrix(defaultISize, defaultYSize);
        RatioMatrix resultMatrix = new RatioMatrix(defaultISize, defaultYSize);

        // cuts
        RatioCut cuts = new RatioCut();

        // narrowing
        RatioNarrowing narrowing = new RatioNarrowing();

        // attributes 
        AttributesCheck attributes = new AttributesCheck();

        // type
        TypeCheck type = new TypeCheck();

        // find methods
        Find find = new Find();

        // factorisation
        Factorization factorization = new Factorization();

        // distance between matrixes
        Distance distance = new Distance();

        // simmilarity between matrixes
        Simmilarity simmilarity = new Simmilarity();

        // min/max search
        MinMaxFind minMax = new MinMaxFind();

        public Form1()
        {
            InitializeComponent();

            // check screen resolution
            Screen myScreen = Screen.FromControl(this);
            Rectangle area = myScreen.WorkingArea;
            if (area.Width < 3840 || area.Height < 2040) // 2040 - because work space = screen size - windows navbar
            {
                MessageBox.Show($"This program was developed for displays with a resolution of 3840x2160 (Developer is lazy to rework this).\nThe resolution of your work space is {area.Width}x{area.Height}, which can cause visual bugs inside the application, such as: cropped elements or elements overlapping each other."
                    , "Wait!"
                    , MessageBoxButtons.OK
                    , MessageBoxIcon.Warning);
            }

            right.SafeCheck(rights.Text);

            // all matrixes
            TextBox[,] matrixP_init_elements = new TextBox[5, 5] {
                { matrixP_value1, matrixP_value2, matrixP_value3, matrixP_value4, matrixP_value5 },
                { matrixP_value6, matrixP_value7, matrixP_value8, matrixP_value9, matrixP_value10 },
                { matrixP_value11, matrixP_value12, matrixP_value13, matrixP_value14, matrixP_value15 },
                { matrixP_value16, matrixP_value17, matrixP_value18, matrixP_value19, matrixP_value20 },
                { matrixP_value21, matrixP_value22, matrixP_value23, matrixP_value24, matrixP_value25 }
            };

            TextBox[,] matrixQ_init_elements = new TextBox[5, 5] {
                { matrixQ_value1, matrixQ_value2, matrixQ_value3, matrixQ_value4, matrixQ_value5 },
                { matrixQ_value6, matrixQ_value7, matrixQ_value8, matrixQ_value9, matrixQ_value10 },
                { matrixQ_value11, matrixQ_value12, matrixQ_value13, matrixQ_value14, matrixQ_value15 },
                { matrixQ_value16, matrixQ_value17, matrixQ_value18, matrixQ_value19, matrixQ_value20 },
                { matrixQ_value21, matrixQ_value22, matrixQ_value23, matrixQ_value24, matrixQ_value25 }
            };

            TextBox[,] matrixR_init_elements = new TextBox[5, 5] {
                { matrixR_value1, matrixR_value2, matrixR_value3, matrixR_value4, matrixR_value5 },
                { matrixR_value6, matrixR_value7, matrixR_value8, matrixR_value9, matrixR_value10 },
                { matrixR_value11, matrixR_value12, matrixR_value13, matrixR_value14, matrixR_value15 },
                { matrixR_value16, matrixR_value17, matrixR_value18, matrixR_value19, matrixR_value20 },
                { matrixR_value21, matrixR_value22, matrixR_value23, matrixR_value24, matrixR_value25 }
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
            matrixQ.Matrix = matrixQ_init_elements;
            matrixR.Matrix = matrixR_init_elements;
            resultMatrix.Matrix = result_init_elements;
        }

        // ------- Functions -------

        private void fillMatrix(RatioMatrix matrixToFill)
        {
            for (int i = 0; i < resultMatrix.Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < resultMatrix.Matrix.GetLength(1); j++)
                {
                    matrixToFill.Matrix[i, j].Text = resultMatrix.Matrix[i, j].Text;
                }
            }
        }

        private bool processResult(int cutMode)
        {
            bool[] matrixesChecked = { matrixP.IsChecked, matrixQ.IsChecked, matrixR.IsChecked };
            TextBox[][,] matrixes = { matrixP.Matrix, matrixQ.Matrix, matrixR.Matrix, };
            List<int> selectedMatrixes = new List<int>();

            // adding selected matrixes
            for (int i = 0; i < matrixesChecked.Length; i++) if (matrixesChecked[i] == true) selectedMatrixes.Add(i);

            // calculating distance between matrixes
            if (distance_checkBox.Checked) if (!distance.ShowResult(matrixes, selectedMatrixes)) return false;

            // simmilarity calculation
            if (similarity_checkbox.Checked) if (!simmilarity.ShowResult(matrixes, selectedMatrixes)) return false;

            if (operation_check.Checked)
            {
                int selectedOperation = operations_list.SelectedIndex;

                if (!resultMatrix.SafeCheck(selectedOperation, cutMode, selectedMatrixes, slice_check.Checked, similarity_checkbox.Checked)) return false;

                // calculate operation in cycle
                for (int i = 0; i < resultMatrix.Matrix.GetLength(0); i++)
                {
                    for (int y = 0; y < resultMatrix.Matrix.GetLength(1); y++)
                    {
                        if (!resultMatrix.DoOperation(matrixes, selectedMatrixes, selectedOperation, i, y, resultMatrix.Matrix)) return false;
                    }
                }

                return true;
            }
            else if (!distance_checkBox.Checked)
            {
                int selectedOperation = 7;

                if (!resultMatrix.SafeCheck(selectedOperation, cutMode, selectedMatrixes, slice_check.Checked, similarity_checkbox.Checked)) return false;

                // just type matrix in result
                for (int i = 0; i < resultMatrix.Matrix.GetLength(0); i++)
                {
                    for (int y = 0; y < resultMatrix.Matrix.GetLength(1); y++)
                    {
                        if (!resultMatrix.DoOperation(matrixes, selectedMatrixes, selectedOperation, i, y, resultMatrix.Matrix)) return false;
                    }
                }

                return true;
            }

            return true;
        }

        // ------- Buttons interaction -------

        // ---- Matrix P ----
        private void matrixPCheck_CheckStateChanged(object sender, EventArgs e) { matrixP.IsChecked = matrixP.StringToBool(sender.ToString()[^1..]); }
        // P buttons
        private void matrixP_FullBtn_Click(object sender, EventArgs e) { matrixP.FillMatrix("full"); }
        private void matrixP_EmptyBtn_Click(object sender, EventArgs e) { matrixP.FillMatrix("empty"); }
        private void matrixP_DiagonalBtn_Click(object sender, EventArgs e) { matrixP.FillMatrix("diagonal"); }
        private void matrixP_AntiDiagonalBtn_Click(object sender, EventArgs e) { matrixP.FillMatrix("anti-diagonal"); }
        private void fillP_button_Click(object sender, EventArgs e) { fillMatrix(matrixP); }

        // ---- Matrix Q ----
        private void matrixQCheck_CheckStateChanged(object sender, EventArgs e) { matrixQ.IsChecked = matrixQ.StringToBool(sender.ToString()[^1..]); }
        // Q buttons
        private void matrixQ_FullBtn_Click(object sender, EventArgs e) { matrixQ.FillMatrix("full"); }
        private void matrixQ_EmptyBtn_Click(object sender, EventArgs e) { matrixQ.FillMatrix("empty"); }
        private void matrixQ_DiagonalBtn_Click(object sender, EventArgs e) { matrixQ.FillMatrix("diagonal"); }
        private void matrixQ_AntiDiagonalBtn_Click(object sender, EventArgs e) { matrixQ.FillMatrix("anti-diagonal"); }
        private void fillQ_button_Click(object sender, EventArgs e) { fillMatrix(matrixQ); }

        // ---- Matrix R ----
        private void matrixRCheck_CheckStateChanged(object sender, EventArgs e) { matrixR.IsChecked = matrixR.StringToBool(sender.ToString()[^1..]); }
        // R buttons
        private void matrixR_FullBtn_Click(object sender, EventArgs e) { matrixR.FillMatrix("full"); }
        private void matrixR_EmptyBtn_Click(object sender, EventArgs e) { matrixR.FillMatrix("empty"); }
        private void matrixR_DiagonalBtn_Click(object sender, EventArgs e) { matrixR.FillMatrix("diagonal"); }
        private void matrixR_AntiDiagonalBtn_Click(object sender, EventArgs e) { matrixR.FillMatrix("anti-diagonal"); }
        private void fillR_button_Click(object sender, EventArgs e) { fillMatrix(matrixR); }

        // ---- Matrix B (Result matrix) ---- 
        private void showResult_btn_Click(object sender, EventArgs e)
        {
            right.SafeExit();

            int selectedCutMode = sliceMode_box.SelectedIndex;
            if (!processResult(selectedCutMode)) return;

            // showing cuts
            if (slice_check.Checked)
            {
                string message = cuts.GetCuts(resultMatrix.Matrix, selectedCutMode);
                string title = $"{(selectedCutMode == 0 ? "Vertical" : "Horizontal")} cuts";
                MessageBox.Show(message, title);

            }

            // showing narrowing
            if (narrowing_check.Checked) narrowing.showNarrowing(resultMatrix.Matrix, narrowValue1, narrowValue2, narrowValue3);

            // showing attributes
            if (attributes_check.Checked) attributes.ShowAttributes(resultMatrix.Matrix);

            // showing matrix type
            if (type_check.Checked) type.ShowType(attributes, resultMatrix.Matrix);

            // using methods
            if (symmetrial_check.Checked) find.FindPS(resultMatrix.Matrix);
            if (assymetrical_check.Checked) find.FindPA(resultMatrix.Matrix);
            if (transitiveClosure_check.Checked) find.TransitiveClosure(resultMatrix.Matrix);
            if (reach_check.Checked) find.Reach(resultMatrix.Matrix);
            if (mutualReach_check.Checked) find.MutualRich(resultMatrix.Matrix);
            if (factorize_check.Checked) factorization.Factorize(find.GetEquivalentRatio(resultMatrix.Matrix, attributes), resultMatrix.Matrix);

            // searching min/max
            if (minmax_CheckBox.Checked)
            {
                int minMaxOperation = minmax_box.SelectedIndex;
                minMax.ShowResult(resultMatrix.Matrix, minMaxOperation);
            }

        }


        // key interaction
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool isValid = true;

            // 8 - backspace, 48 - 0, 49 - 1
            if (!(e.KeyChar == 48 || e.KeyChar == 49 || e.KeyChar == 8))
            {
                isValid = false;
                MessageBox.Show("Please enter only '0' or '1'.", "Hey", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            e.Handled = !(e.KeyChar == 48 || e.KeyChar == 49 || e.KeyChar == 8);

            if (!isValid) return;
            switchToNext(sender);
        }

        private void switchToNext(object sender)
        {
            TextBox[][,] matrixesArray = new TextBox[3][,] { matrixP.Matrix, matrixQ.Matrix, matrixR.Matrix };

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

        private void narrowRestriction(object sender, KeyPressEventArgs e)
        {
            // 49 - 1, 50 - 2, 51 - 3, 52 - 4, 53 - 5, 8 - backspace,
            if (!(e.KeyChar == 49 || e.KeyChar == 50 || e.KeyChar == 51 || e.KeyChar == 52 || e.KeyChar == 53 || e.KeyChar == 8))
            {
                MessageBox.Show($"Please enter only numbers from 0 to 5\n{e.KeyChar}", "Hey", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            e.Handled = !(e.KeyChar == 49 || e.KeyChar == 50 || e.KeyChar == 51 || e.KeyChar == 52 || e.KeyChar == 53 || e.KeyChar == 8);
        }

        // safe switch
        private void Form1_Load(object sender, EventArgs e)
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

        private void modeSwitch_btn_Click(object sender, EventArgs e)
        {
            DecimalMode newMode = new DecimalMode();
            this.Hide();
            newMode.ShowDialog();
            if (newMode.ModeChange) this.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(new ProcessStartInfo("https://github.com/TSlashDreamy/work-on-matrixes/issues/new") { UseShellExecute = true });
        }

        private void decisionButton_Click(object sender, EventArgs e)
        {
            DecisionUI decisionUI = new DecisionUI();
            this.Enabled = false;
            decisionUI.ShowDialog();
            this.Enabled = true;
        }

        private void operation_check_CheckStateChanged(object sender, EventArgs e)
        {
            // oh my god, what have i done ⚆_⚆
            label1.Enabled = matrixP.StringToBool(sender.ToString()[^1..]);
            operations_list.Enabled = matrixP.StringToBool(sender.ToString()[^1..]);
        }

        private void slice_check_CheckStateChanged(object sender, EventArgs e)
        {
            // oh my god, what have i done ⚆_⚆
            label7.Enabled = matrixP.StringToBool(sender.ToString()[^1..]);
            sliceMode_box.Enabled = matrixP.StringToBool(sender.ToString()[^1..]);
        }

        private void narrowing_check_CheckStateChanged(object sender, EventArgs e)
        {
            // oh my god, what have i done ⚆_⚆
            label8.Enabled = matrixP.StringToBool(sender.ToString()[^1..]);
            narrowValue1.Enabled = matrixP.StringToBool(sender.ToString()[^1..]);
            narrowValue2.Enabled = matrixP.StringToBool(sender.ToString()[^1..]);
            narrowValue3.Enabled = matrixP.StringToBool(sender.ToString()[^1..]);
        }

        private void minmax_CheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            // oh my god, what have i done ⚆_⚆
            label22.Enabled = matrixP.StringToBool(sender.ToString()[^1..]);
            minmax_box.Enabled = matrixP.StringToBool(sender.ToString()[^1..]);
        }
    }
}
