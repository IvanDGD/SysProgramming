using System;
using System.Windows.Forms;

namespace WinFormsApp1
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Form1 mainForm = new Form1();
            Form2 extraForm = new Form2();
            extraForm.Show();
            Application.Run(mainForm);
        }
    }
}
