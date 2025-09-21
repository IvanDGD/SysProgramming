using Microsoft.Win32;
using System;
using System.Numerics; // для больших факториалов
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private const string RegistryPath = @"SOFTWARE\FontSizeApp";
        private const string RegistryKeyName = "FontSize";

        public Form1()
        {
            InitializeComponent();
            LoadFontSizeFromRegistry();
            button1.Click += Button1_Click;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            float fontSize = (float)numericUpDown1.Value;
            ApplyFontSize(fontSize);
            SaveFontSizeToRegistry(fontSize);
        }

        private void ApplyFontSize(float size)
        {
            this.Font = new Font(this.Font.FontFamily, size);
            label1.Font = new Font(this.Font.FontFamily, size);
        }

        private void LoadFontSizeFromRegistry()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(RegistryPath))
            {
                if (key != null)
                {
                    object value = key.GetValue(RegistryKeyName);
                    if (value != null && float.TryParse(value.ToString(), out float fontSize))
                    {
                        numericUpDown1.Value = (decimal)fontSize;
                        ApplyFontSize(fontSize);
                    }
                }
            }
        }

        private void SaveFontSizeToRegistry(float size)
        {
            using (RegistryKey key = Registry.CurrentUser.CreateSubKey(RegistryPath))
            {
                key.SetValue(RegistryKeyName, size, RegistryValueKind.String);
            }
        }
    }
}
