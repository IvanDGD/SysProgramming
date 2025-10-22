using MailKit.Net.Pop3;
using System.Net;
using System.Net.Mail;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Введите ваш email: ");
        string fromEmail = Console.ReadLine();

        Console.Write("Введите пароль: ");
        string password = Console.ReadLine();

        Console.Write("Введите тему письма: ");
        string subject = Console.ReadLine();

        Console.Write("Введите текст письма: ");
        string body = Console.ReadLine();

        Console.Write("Введите email получателей через запятую: ");
        string recipientsInput = Console.ReadLine();
        string[] recipients = recipientsInput.Split(',');

        MailMessage mail = new MailMessage();
        mail.From = new MailAddress(fromEmail);
        foreach (string recipient in recipients)
        {
            mail.To.Add(recipient.Trim());
        }
        mail.Subject = subject;
        mail.Body = body;

        SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
        smtp.Credentials = new NetworkCredential(fromEmail, password);
        smtp.EnableSsl = true;

        smtp.Send(mail);
        Console.WriteLine("Письмо отправленно");

        Console.ReadKey();
    }
}
