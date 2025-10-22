namespace WinFormsApp2
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label8 = new Label();
            label9 = new Label();
            label1 = new Label();
            SendButton = new Button();
            button2 = new Button();
            button3 = new Button();
            MessageTextBox = new TextBox();
            RecipientTextBox = new ListBox();
            CredentialPortNumeric = new NumericUpDown();
            button6 = new Button();
            button4 = new Button();
            button5 = new Button();
            AttachmentsListbox = new ListBox();
            SubjectTextBox = new TextBox();
            CredentialSmtpTextBox = new TextBox();
            CredentialPasswordTextBox = new TextBox();
            CredentialEmailTextBox = new TextBox();
            ((System.ComponentModel.ISupportInitialize)CredentialPortNumeric).BeginInit();
            SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 317);
            label2.Name = "label2";
            label2.Size = new Size(90, 20);
            label2.TabIndex = 1;
            label2.Text = "Smtp server:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(484, 370);
            label3.Name = "label3";
            label3.Size = new Size(95, 20);
            label3.TabIndex = 2;
            label3.Text = "Attachments:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(484, 115);
            label4.Name = "label4";
            label4.Size = new Size(70, 20);
            label4.TabIndex = 3;
            label4.Text = "Message:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 474);
            label5.Name = "label5";
            label5.Size = new Size(49, 20);
            label5.TabIndex = 4;
            label5.Text = "Email:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 389);
            label6.Name = "label6";
            label6.Size = new Size(38, 20);
            label6.TabIndex = 5;
            label6.Text = "Port:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(484, 34);
            label8.Name = "label8";
            label8.Size = new Size(61, 20);
            label8.TabIndex = 7;
            label8.Text = "Subject:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(12, 34);
            label9.Name = "label9";
            label9.Size = new Size(80, 20);
            label9.TabIndex = 8;
            label9.Text = "Recipients:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 546);
            label1.Name = "label1";
            label1.Size = new Size(73, 20);
            label1.TabIndex = 9;
            label1.Text = "Password:";
            // 
            // SendButton
            // 
            SendButton.Location = new Point(12, 612);
            SendButton.Name = "SendButton";
            SendButton.Size = new Size(306, 29);
            SendButton.TabIndex = 10;
            SendButton.Text = "Send";
            SendButton.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(769, 612);
            button2.Name = "button2";
            button2.Size = new Size(279, 29);
            button2.TabIndex = 11;
            button2.Text = "Remove";
            button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(484, 612);
            button3.Name = "button3";
            button3.Size = new Size(279, 29);
            button3.TabIndex = 12;
            button3.Text = "Add";
            button3.UseVisualStyleBackColor = true;
            // 
            // MessageTextBox
            // 
            MessageTextBox.Location = new Point(484, 138);
            MessageTextBox.Multiline = true;
            MessageTextBox.Name = "MessageTextBox";
            MessageTextBox.Size = new Size(564, 217);
            MessageTextBox.TabIndex = 13;
            // 
            // RecipientTextBox
            // 
            RecipientTextBox.FormattingEnabled = true;
            RecipientTextBox.Location = new Point(12, 57);
            RecipientTextBox.Name = "RecipientTextBox";
            RecipientTextBox.Size = new Size(306, 184);
            RecipientTextBox.TabIndex = 14;
            RecipientTextBox.SelectedIndexChanged += RecipientsListBox_SelectedIndexChanged;
            // 
            // CredentialPortNumeric
            // 
            CredentialPortNumeric.Location = new Point(12, 412);
            CredentialPortNumeric.Name = "CredentialPortNumeric";
            CredentialPortNumeric.Size = new Size(306, 27);
            CredentialPortNumeric.TabIndex = 15;
            // 
            // button6
            // 
            button6.Location = new Point(232, 258);
            button6.Name = "button6";
            button6.Size = new Size(86, 29);
            button6.TabIndex = 18;
            button6.Text = "button6";
            button6.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            button4.Location = new Point(120, 258);
            button4.Name = "button4";
            button4.Size = new Size(86, 29);
            button4.TabIndex = 19;
            button4.Text = "button4";
            button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            button5.Location = new Point(12, 258);
            button5.Name = "button5";
            button5.Size = new Size(86, 29);
            button5.TabIndex = 20;
            button5.Text = "button5";
            button5.UseVisualStyleBackColor = true;
            // 
            // AttachmentsListbox
            // 
            AttachmentsListbox.FormattingEnabled = true;
            AttachmentsListbox.Location = new Point(484, 393);
            AttachmentsListbox.Name = "AttachmentsListbox";
            AttachmentsListbox.Size = new Size(564, 204);
            AttachmentsListbox.TabIndex = 21;
            // 
            // SubjectTextBox
            // 
            SubjectTextBox.Location = new Point(484, 70);
            SubjectTextBox.Name = "SubjectTextBox";
            SubjectTextBox.Size = new Size(564, 27);
            SubjectTextBox.TabIndex = 22;
            // 
            // CredentialSmtpTextBox
            // 
            CredentialSmtpTextBox.Location = new Point(12, 340);
            CredentialSmtpTextBox.Name = "CredentialSmtpTextBox";
            CredentialSmtpTextBox.Size = new Size(306, 27);
            CredentialSmtpTextBox.TabIndex = 23;
            // 
            // CredentialPasswordTextBox
            // 
            CredentialPasswordTextBox.Location = new Point(12, 569);
            CredentialPasswordTextBox.Name = "CredentialPasswordTextBox";
            CredentialPasswordTextBox.Size = new Size(306, 27);
            CredentialPasswordTextBox.TabIndex = 24;
            // 
            // CredentialEmailTextBox
            // 
            CredentialEmailTextBox.Location = new Point(12, 497);
            CredentialEmailTextBox.Name = "CredentialEmailTextBox";
            CredentialEmailTextBox.Size = new Size(306, 27);
            CredentialEmailTextBox.TabIndex = 25;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1132, 653);
            Controls.Add(CredentialEmailTextBox);
            Controls.Add(CredentialPasswordTextBox);
            Controls.Add(CredentialSmtpTextBox);
            Controls.Add(SubjectTextBox);
            Controls.Add(AttachmentsListbox);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button6);
            Controls.Add(CredentialPortNumeric);
            Controls.Add(RecipientTextBox);
            Controls.Add(MessageTextBox);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(SendButton);
            Controls.Add(label1);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)CredentialPortNumeric).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label8;
        private Label label9;
        private Label label1;
        private Button SendButton;
        private Button button2;
        private Button button3;
        private TextBox MessageTextBox;
        private ListBox RecipientTextBox;
        private NumericUpDown CredentialPortNumeric;
        private Button button6;
        private Button button4;
        private Button button5;
        private ListBox AttachmentsListbox;
        private TextBox SubjectTextBox;
        private TextBox CredentialSmtpTextBox;
        private TextBox CredentialPasswordTextBox;
        private TextBox CredentialEmailTextBox;
    }
}
