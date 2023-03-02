using System.Collections.Generic;

namespace lab1
{
    public partial class Form1 : Form
    {
        Right right = new Right();

        // states
        bool value = false; // for matrix it would be like "0" and "1"
        bool diagonalOperation = false;
        bool safe_to_launch = false;

        // matrix's
        TextBox[,] matrixP_elements;
        TextBox[,] matrixQ_elements;
        TextBox[,] matrixR_elements;
        TextBox[,] result_elements;

        // matrix checks
        bool matrixP_checked;
        bool matrixQ_checked;
        bool matrixR_checked;

        // matrix per operation
        int[] matrixesForOperation = { 2, 2, 1, 1, 2, 2, 2 };

        public Form1()
        {
            InitializeComponent();

            safe_to_launch = rights.Text == right.RightText;

            // all matrixes
            TextBox[,] matrixP_init_elements = new TextBox[5, 5] {
                { matrixP_value1, matrixP_value2, matrixP_value3, matrixP_value4, matrixP_value5 },
                { matrixP_value6, matrixP_value7, matrixP_value8, matrixP_value9, matrixP_value10 },
                { matrixP_value11, matrixP_value12, matrixP_value13, matrixP_value14, matrixP_value15 },
                { matrixP_value16, matrixP_value17, matrixP_value18, matrixP_value19, matrixP_value20 },
                { matrixP_value21, matrixP_value22, matrixP_value23, matrixP_value24, matrixP_value25 }
            };
            matrixP_elements = matrixP_init_elements;

            TextBox[,] matrixQ_init_elements = new TextBox[5, 5] {
                { matrixQ_value1, matrixQ_value2, matrixQ_value3, matrixQ_value4, matrixQ_value5 },
                { matrixQ_value6, matrixQ_value7, matrixQ_value8, matrixQ_value9, matrixQ_value10 },
                { matrixQ_value11, matrixQ_value12, matrixQ_value13, matrixQ_value14, matrixQ_value15 },
                { matrixQ_value16, matrixQ_value17, matrixQ_value18, matrixQ_value19, matrixQ_value20 },
                { matrixQ_value21, matrixQ_value22, matrixQ_value23, matrixQ_value24, matrixQ_value25 }
            };
            matrixQ_elements = matrixQ_init_elements;

            TextBox[,] matrixR_init_elements = new TextBox[5, 5] {
                { matrixR_value1, matrixR_value2, matrixR_value3, matrixR_value4, matrixR_value5 },
                { matrixR_value6, matrixR_value7, matrixR_value8, matrixR_value9, matrixR_value10 },
                { matrixR_value11, matrixR_value12, matrixR_value13, matrixR_value14, matrixR_value15 },
                { matrixR_value16, matrixR_value17, matrixR_value18, matrixR_value19, matrixR_value20 },
                { matrixR_value21, matrixR_value22, matrixR_value23, matrixR_value24, matrixR_value25 }
            };
            matrixR_elements = matrixR_init_elements;

            TextBox[,] result_init_elements = new TextBox[5, 5] {
                { result_value1, result_value2, result_value3, result_value4, result_value5 },
                { result_value6, result_value7, result_value8, result_value9, result_value10 },
                { result_value11, result_value12, result_value13, result_value14, result_value15 },
                { result_value16, result_value17, result_value18, result_value19, result_value20 },
                { result_value21, result_value22, result_value23, result_value24, result_value25 }
            };
            result_elements = result_init_elements; 
        }

        // ------- Functions -------

        /// <summary>
        /// Fills selected matrix with 0 and 1 by selected operation
        /// </summary>
        /// <param name="matrixToFill"></param>
        /// <param name="operation">Available: "full", "empty", "diagonal", "anti-diagonal"</param>
        /// <exception cref="Exception">If operation does not exist</exception>
        private void FillMatrix(TextBox[,] matrixToFill, string operation)
        {
            switch (operation)
            {
                case "full":
                    ChangeState(true, false);
                    break;
                case "diagonal":
                    ChangeState(true, true);
                    break;
                case "empty":
                    ChangeState(false, false);
                    break;
                case "anti-diagonal":
                    ChangeState(false, true);
                    break;
                default:
                    throw new Exception("'" + operation + "' operation is not exist.");
            }

            if (diagonalOperation)
            {
                DiagonalFill(matrixToFill, operation);
            } else
            {
                SimpleFill(matrixToFill, operation);
            }

        }

        /// <summary>
        /// Changes global states to fill matrix
        /// </summary>
        /// <param name="valueToChange">For matrix it would be like "0"(false) and "1"(true)</param>
        /// <param name="operationToChange">Simple(false) or diagonal(true) operation</param>
        private void ChangeState(bool valueToChange, bool operationToChange)
        {
            value = valueToChange;
            diagonalOperation = operationToChange;
        }

        /// <summary>
        /// Fills matrix diagonally
        /// </summary>
        /// <param name="matrixToFill"></param>
        /// <param name="operation">Diagonal or antidiagonal</param>
        private void DiagonalFill(TextBox[,] matrixToFill, string operation)
        {
            int step = 0;

            for (int i = 0; i < matrixToFill.GetLength(0); i++)
            {
                for (int y = 0; y < matrixToFill.GetLength(1); y++)
                {
                    if (step == y)
                    {
                        matrixToFill[i, y].Text = BoolToString(value);
                    }
                    else
                    {
                        matrixToFill[i, y].Text = BoolToString(!value);
                    }
                }
                step++;
            }
        }

        /// <summary>
        /// Converts boolean to string
        /// </summary>
        /// <param name="value">convertable string</param>
        /// <returns><b>bool</b></returns>
        private string BoolToString(bool value)
        {
            return Convert.ToInt32(value).ToString();
        }

        /// <summary>
        /// Fully fills matrix just with one value "0" or "1"
        /// </summary>
        /// <param name="matrixToFill"></param>
        /// <param name="operation">"full" or "empty"</param>
        private void SimpleFill(TextBox[,] matrixToFill, string operation)
        {
            foreach (TextBox matrix_value in matrixToFill)
            {
                matrix_value.Text = BoolToString(value);
            }
        }

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

        /// <summary>
        /// Converts boolean to string
        /// </summary>
        /// <param name="value"></param>
        /// <returns><b>string</b></returns>
        private bool StringToBool(string value)
        {
            return Convert.ToBoolean(Convert.ToInt32(value));
        }

        /// <summary>
        /// Safely exits app in case the rights was corrupted
        /// </summary>
        private void SafeExit()
        {
            if (!safe_to_launch)
            {
                Application.Exit();
            }
        }

        /// <summary>
        /// Warns in case the rights was corrupted
        /// </summary>
        /// <exception cref="Exception"></exception>
        private void SafeWarning()
        {
            if (!safe_to_launch)
            {
                showResult_btn.Enabled = false;
                rights.Text = "⚠️ Rights corrupted!";
                throw new Exception("Oh no! It's look's like the rights is corrupted. Please contact with the developer!");
            }
        }

        /// <summary>
        /// Calculates matrixes via selected operation
        /// </summary>
        private void DoOperation(TextBox[][,] matrixes, List<int> selectedMatrixes, int selectedOperation, int i, int y, bool firstMatrix, bool secondMatrix)
        {
            switch (selectedOperation)
            {
                case 0:
                    result_elements[i, y].Text = BoolToString(firstMatrix && secondMatrix);
                    break;
                case 1:
                    result_elements[i, y].Text = BoolToString(firstMatrix || secondMatrix);
                    break;
                case 2:
                    result_elements[i, y].Text = BoolToString(!firstMatrix);
                    break;
                case 3:
                    result_elements[y, i].Text = BoolToString(firstMatrix);
                    break;
                case 4:
                    result_elements[i, y].Text = BoolToString(firstMatrix && !secondMatrix);
                    break;
                case 5:
                    bool firstPart = firstMatrix && !secondMatrix;
                    bool secondPart = secondMatrix && !firstMatrix;

                    result_elements[i, y].Text = BoolToString(firstPart || secondPart);
                    break;
                case 6:
                    bool founded = false;

                    for (int k = 0; k < result_elements.GetLength(0); k++)
                    {
                        if (Convert.ToInt32(matrixes[selectedMatrixes[0]][i, k].Text) == 1 && Convert.ToInt32(matrixes[selectedMatrixes[0]][k, y].Text) == 1)
                        {
                            founded = true;
                            break;
                        }
                    }

                    result_elements[i, y].Text = founded ? "1" : "0";
                    break;
            }
        }



        // ------- Buttons interaction -------

        // Matrix P
        private void matrixPCheck_CheckStateChanged(object sender, EventArgs e)
        {
            matrixP_checked = StringToBool(sender.ToString()[^1..]);
        }

        // --- P buttons ---
        private void matrixP_FullBtn_Click(object sender, EventArgs e)
        {
            FillMatrix(matrixP_elements, "full");
        }

        private void matrixP_EmptyBtn_Click(object sender, EventArgs e)
        {
            FillMatrix(matrixP_elements, "empty");
        }

        private void matrixP_DiagonalBtn_Click(object sender, EventArgs e)
        {
            FillMatrix(matrixP_elements, "diagonal");
        }

        private void matrixP_AntiDiagonalBtn_Click(object sender, EventArgs e)
        {
            FillMatrix(matrixP_elements, "anti-diagonal");
        }
        // --- ~P buttons ---


        // Matrix Q
        private void matrixQCheck_CheckStateChanged(object sender, EventArgs e)
        {
            matrixQ_checked = StringToBool(sender.ToString()[^1..]);
        }

        // --- Q buttons --- 
        private void matrixQ_FullBtn_Click(object sender, EventArgs e)
        {
            FillMatrix(matrixQ_elements, "full");
        }

        private void matrixQ_EmptyBtn_Click(object sender, EventArgs e)
        {
            FillMatrix(matrixQ_elements, "empty");
        }

        private void matrixQ_DiagonalBtn_Click(object sender, EventArgs e)
        {
            FillMatrix(matrixQ_elements, "diagonal");
        }

        private void matrixQ_AntiDiagonalBtn_Click(object sender, EventArgs e)
        {
            FillMatrix(matrixQ_elements, "anti-diagonal");
        }
        // --- ~Q buttons ---


        // Matrix R
        private void matrixRCheck_CheckStateChanged(object sender, EventArgs e)
        {
            matrixR_checked = StringToBool(sender.ToString()[^1..]);
        }

        // --- R buttons ---
        private void matrixR_FullBtn_Click(object sender, EventArgs e)
        {
            FillMatrix(matrixR_elements, "full");
        }

        private void matrixR_EmptyBtn_Click(object sender, EventArgs e)
        {
            FillMatrix(matrixR_elements, "empty");
        }

        private void matrixR_DiagonalBtn_Click(object sender, EventArgs e)
        {
            FillMatrix(matrixR_elements, "diagonal");
        }

        private void matrixR_AntiDiagonalBtn_Click(object sender, EventArgs e)
        {
            FillMatrix(matrixR_elements, "anti-diagonal");
        }
        // --- ~R buttons ---


        // Matrix B (Result matrix)
        private void showResult_btn_Click(object sender, EventArgs e)
        {
            bool[] matrixesChecked = { matrixP_checked, matrixQ_checked, matrixR_checked };
            TextBox[][,] matrixes = { matrixP_elements, matrixQ_elements, matrixR_elements, };
            List<int> selectedMatrixes = new List<int>();
            
            SafeExit();

            int selectedOperation = operations_list.SelectedIndex;
            int selectedCutMode = sliceMode_box.SelectedIndex;

            // checking if operation and cut mode selected
            if (selectedOperation == -1 || selectedCutMode == -1)
            {
                MessageBox.Show("Please, select operation and cut mode to continue", "Wait!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // clearing previoud matrixes
            selectedMatrixes.Clear();

            // adding selected matrixes
            for (int i = 0; i < matrixesChecked.Length; i++) if (matrixesChecked[i] == true) selectedMatrixes.Add(i);

            // check if needed amount of matrixes selected
            if (selectedMatrixes.Count != matrixesForOperation[selectedOperation])
            {
                MessageBox.Show($"For this operation select {matrixesForOperation[selectedOperation]} matrixes.", "Hey", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // calculate operation in cycle
            for (int i = 0; i < result_elements.GetLength(0); i++)
            {
                for (int y = 0; y < result_elements.GetLength(1); y++)
                {
                    bool firstMatrix = false;
                    bool secondMatrix = false;

                    // check if matrix is fully filled
                    try
                    {
                        if (selectedMatrixes.Count == 2)
                        {
                            firstMatrix = Convert.ToBoolean(Convert.ToInt32(matrixes[selectedMatrixes[0]][i, y].Text));
                            secondMatrix = Convert.ToBoolean(Convert.ToInt32(matrixes[selectedMatrixes[1]][i, y].Text));
                        }
                        else
                        {
                            firstMatrix = Convert.ToBoolean(Convert.ToInt32(matrixes[selectedMatrixes[0]][i, y].Text));
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Please, fill selected matrixes", "Wait!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    DoOperation(matrixes, selectedMatrixes, selectedOperation, i, y, firstMatrix, secondMatrix);
                }
            }
            ShowCut(result_elements, selectedCutMode);
        }


        // key interaction
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 8 - backspace, 48 - 0, 49 - 1
            if (!(e.KeyChar == 48 || e.KeyChar == 49 || e.KeyChar == 8)) MessageBox.Show("Please enter only '0' or '1'.", "Hey", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            e.Handled = !(e.KeyChar == 48 || e.KeyChar == 49 || e.KeyChar == 8);
        }

        // safe switch
        private void Form1_Load(object sender, EventArgs e)
        {
            SafeWarning();
        }

    }
}
