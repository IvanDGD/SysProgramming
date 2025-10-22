using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void SendButton_Click(object sender, EventArgs e)
        {
            if (RecipientTextBox.Items.Count > 0)
            {
                Message message = new Message()
                {
                    Subject = SubjectTextBox.Text,
                    Body = MessageTextBox.Text,
                    Credentials = new Credentials
                    {
                        Port = (int)CredentialPortNumeric.Value,
                        Smtp = CredentialSmtpTextBox.Text,
                        Email = CredentialEmailTextBox.Text,
                        Password = CredentialPasswordTextBox.Text
                    }
                };
                foreach (var item in AttachmentsListbox.Items)
                {
                    message.Attachments.Add(new Attachment(item.ToString()));
                }
                foreach (var item in RecipientTextBox.Items)
                {
                    message.Recipients.Add(item.ToString());
                }

                //Проверка для объекта Message
                var results = new List<ValidationResult>();
                var context = new ValidationContext(message);
                if (!Validator.TryValidateObject(message, context, results, true))
                {
                    var st = new StringBuilder();
                    foreach (var error in results)
                    {
                        st.Append(error.ErrorMessage + Environment.NewLine);
                    }
                    MessageBox.Show(st.ToString());
                    return;
                }

                //Проверка для объекта Credentials
                context = new ValidationContext(message.Credentials);
                if (!Validator.TryValidateObject(message.Credentials, context, results, true))
                {
                    var st = new StringBuilder();
                    foreach (var error in results)
                    {
                        st.Append(error.ErrorMessage + Environment.NewLine);
                    }
                    MessageBox.Show(st.ToString());
                    return;
                }

                //Отправка сообщения
                await SendMessage(message);
            }
            else
            {
                MessageBox.Show("Add recipients.");
            }
        }
        private async Task SendMessage(Message message)
        {
            MailAddress from = new MailAddress(message.Credentials.Email, message.Credentials.Email);
            MailMessage m = new MailMessage();
            m.From = from;
            foreach (string recipient in message.Recipients)
            {
                m.To.Add(recipient);
            }

            m.Subject = message.Subject;
            m.Body = message.Body;
            m.IsBodyHtml = message.IsBodyHtml;

            foreach (Attachment attachment in message.Attachments)
            {
                m.Attachments.Add(attachment);
            }

            SmtpClient smtp = new SmtpClient(message.Credentials.Smtp, message.Credentials.Port);
            smtp.Credentials = new NetworkCredential(message.Credentials.Email, message.Credentials.Password);
            smtp.EnableSsl = true;

            try
            {
                SendButton.Enabled = false;
                SendButton.Text = "Sending..";

                await smtp.SendMailAsync(m);
                MessageBox.Show("The letter has been successfully sent.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error when sending an e-mail: {ex.Message}");
            }

            SendButton.Enabled = true;
            SendButton.Text = "Send";
        }

        private void AddRecipientButton_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(RecipientTextBox.Text))
            {
                RecipientTextBox.Items.Add(RecipientTextBox.Text);
                RecipientTextBox.Text = String.Empty;
            }
        }

        private void RemoveRecipientButton_Click(object sender, EventArgs e)
        {
            if (RecipientTextBox.SelectedIndex >= 0)
            {
                RecipientTextBox.Items.RemoveAt(RecipientTextBox.SelectedIndex);
            }
        }

        private void ClearRecipientsButton_Click(object sender, EventArgs e)
        {
            RecipientTextBox.Items.Clear();
        }

        private void AddAttachmentButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "C:\\";
                openFileDialog.Filter = "All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string fileName = openFileDialog.FileName;
                    AttachmentsListbox.Items.Add(fileName);
                }
            }
        }

        private void RemoveAttachmentButton_Click(object sender, EventArgs e)
        {
            if (AttachmentsListbox.SelectedIndex >= 0)
            {
                AttachmentsListbox.Items.RemoveAt(AttachmentsListbox.SelectedIndex);
            }
        }

        private void RecipientsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
    public class Credentials
    {
        [Required]
        public string Smtp { get; set; } = "smtp.gmail.com";

        [Required]
        [Range(1, 10_000)]
        public int Port { get; set; } = 587;

        [Required]
        [RegularExpression("^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\\.[a-zA-Z0-9-.]+$", ErrorMessage = "Not valid email")]
        public string Email { get; set; } = ".gmail.com";

        [Required]
        public string Password { get; set; } = "";
    }
    public class Message
    {
        public Credentials Credentials { get; set; }

        [Required]
        [MinLength(5)]
        public string Subject { get; set; }

        [Required]
        [StringLength(1000, MinimumLength = 5)]
        public string Body { get; set; }
        public bool IsBodyHtml { get; set; } = true;
        public List<string> Recipients { get; set; }
        public List<Attachment> Attachments { get; set; }

        public Message()
        {
            Recipients = new List<string>();
            Attachments = new List<Attachment>();
        }
    }
}
