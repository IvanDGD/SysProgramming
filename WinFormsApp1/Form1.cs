using System;
using System.Numerics; // для больших факториалов
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            button1.Click += button1_Click;

            numericUpDown1.Minimum = 0;
            numericUpDown1.Maximum = 100;
            progressBar1.Minimum = 0;
            progressBar1.Step = 1;
            textBox1.ReadOnly = true;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            int n = (int)numericUpDown1.Value;
            progressBar1.Maximum = n;
            progressBar1.Value = 0;
            button1.Enabled = false;
            textBox1.Clear();

            ulong result = await Task.Run(() =>
            {
                ulong fact = 1;
                for (int i = 1; i <= n; i++)
                {
                    fact *= (ulong)i;
                    this.Invoke(new Action(() =>
                    {
                        progressBar1.Value = i;
                    }));
                }
                return fact;
            });

            textBox1.Text = result.ToString();
            button1.Enabled = true;
        }
    }
}
