namespace WinFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            numericUpDown1 = new NumericUpDown();
            button1 = new Button();
            textBox1 = new TextBox();
            progressBar1 = new ProgressBar();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            SuspendLayout();
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(108, 55);
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(195, 27);
            numericUpDown1.TabIndex = 0;
            // 
            // button1
            // 
            button1.Location = new Point(108, 88);
            button1.Name = "button1";
            button1.Size = new Size(195, 29);
            button1.TabIndex = 1;
            button1.Text = "Calculate Factorial";
            button1.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(108, 123);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(195, 27);
            textBox1.TabIndex = 2;
            textBox1.Text = "Result";
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(108, 156);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(195, 29);
            progressBar1.TabIndex = 3;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(422, 351);
            Controls.Add(progressBar1);
            Controls.Add(textBox1);
            Controls.Add(button1);
            Controls.Add(numericUpDown1);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private NumericUpDown numericUpDown1;
        private Button button1;
        private TextBox textBox1;
        private ProgressBar progressBar1;
    }
}