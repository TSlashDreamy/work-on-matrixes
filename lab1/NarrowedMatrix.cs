using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab1
{
    public partial class NarrowedMatrix : Form
    {
        public NarrowedMatrix(List<string> data)
        {
            InitializeComponent();

            int counter = 0;

            TextBox[,] narrowed = new TextBox[,] { 
            { narrow_text1, narrow_text2, narrow_text3 },
            { narrow_text4, narrow_text5, narrow_text6 },
            { narrow_text7, narrow_text8, narrow_text9 } };

            for (int i = 0; i < narrowed.GetLength(0); i++)
            {
                for (int y = 0; y < narrowed.GetLength(1); y++)
                {
                    narrowed[i, y].Text = data[counter];
                    counter++;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
