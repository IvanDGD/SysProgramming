using System;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinFormsApp1
{
    public partial class Form2 : Form
    {
        private System.Windows.Forms.Timer timer;  

        public Form2()
        {
            InitializeComponent();

            hScrollBar1.Minimum = 1;
            hScrollBar1.Maximum = 50;
            hScrollBar1.Value = 10;
            hScrollBar1.SmallChange = 1;
            hScrollBar1.LargeChange = 1;

            timer = new System.Windows.Forms.Timer();
            timer.Interval = 30;      
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            int speed = hScrollBar1.Value;
            button1.Left -= speed;

            if (button1.Right < 0)
            {
                button1.Left = this.ClientSize.Width;
            }
        }
    }
}
