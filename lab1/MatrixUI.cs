using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab1
{
    public partial class MatrixUI : Form
    {
        public MatrixUI(string[,] foundedMatrix, string operation)
        {
            InitializeComponent();

            TextBox[,] founded_elements = new TextBox[5, 5] {
                { result_value1, result_value2, result_value3, result_value4, result_value5 },
                { result_value6, result_value7, result_value8, result_value9, result_value10 },
                { result_value11, result_value12, result_value13, result_value14, result_value15 },
                { result_value16, result_value17, result_value18, result_value19, result_value20 },
                { result_value21, result_value22, result_value23, result_value24, result_value25 }
            };

            for (int i = 0; i < foundedMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < foundedMatrix.GetLength(1); j++)
                {
                    founded_elements[i, j].Text = foundedMatrix[i, j];
                }
            }

            operation_label.Text += operation;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
