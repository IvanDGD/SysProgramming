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
            StartServerButton = new Button();
            SendMessageButton = new Button();
            StopServerButton = new Button();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            SuspendLayout();
            // 
            // StartServerButton
            // 
            StartServerButton.Location = new Point(15, 16);
            StartServerButton.Name = "StartServerButton";
            StartServerButton.Size = new Size(161, 29);
            StartServerButton.TabIndex = 0;
            StartServerButton.Text = "Start server";
            StartServerButton.UseVisualStyleBackColor = true;
            // 
            // SendMessageButton
            // 
            SendMessageButton.Location = new Point(15, 450);
            SendMessageButton.Name = "SendMessageButton";
            SendMessageButton.Size = new Size(161, 29);
            SendMessageButton.TabIndex = 1;
            SendMessageButton.Text = "Send message";
            SendMessageButton.UseVisualStyleBackColor = true;
            // 
            // StopServerButton
            // 
            StopServerButton.Location = new Point(182, 16);
            StopServerButton.Name = "StopServerButton";
            StopServerButton.Size = new Size(161, 29);
            StopServerButton.TabIndex = 2;
            StopServerButton.Text = "Stop server";
            StopServerButton.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(15, 51);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(584, 264);
            textBox1.TabIndex = 3;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(15, 321);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(584, 123);
            textBox2.TabIndex = 4;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(611, 491);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(StopServerButton);
            Controls.Add(SendMessageButton);
            Controls.Add(StartServerButton);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button StartServerButton;
        private Button SendMessageButton;
        private Button StopServerButton;
        private TextBox textBox1;
        private TextBox textBox2;
    }
}