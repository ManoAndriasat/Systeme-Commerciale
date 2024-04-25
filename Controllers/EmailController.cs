using System;
using EASendMail;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO; // Add this namespace for Path

public class Email : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Insert(string Subject, string TextBody, string To, IFormFile Attachment)
    {
        try
        {
            SmtpMail oMail = new SmtpMail("TryIt");
            // Set sender email address, display name, user name, and password.
            oMail.From = "manooandriasat@gmail.com";
            oMail.To = To;
            oMail.Subject = Subject;
            oMail.TextBody = TextBody;

            // Attach a file to the email.
            if (Attachment != null && Attachment.Length > 0)
            {
                string fileName = Path.GetFileName(Attachment.FileName);
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Attachments", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    Attachment.CopyTo(stream);
                }
                oMail.AddAttachment(filePath);
            }

            SmtpServer oServer = new SmtpServer("smtp.gmail.com");
            oServer.User = "manooandriasat@gmail.com";
            oServer.Password = "bdos uqsb xzot njbe";
            oServer.Port = 587; // Use 465 for SSL connection.

            // Enable SSL/TLS connection; most email servers require this option.
            oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

            SmtpClient oSmtp = new SmtpClient();
            oSmtp.SendMail(oServer, oMail);

            Console.WriteLine("Email sent successfully!");
            return RedirectToAction("Index", "Email");
        }
        catch (Exception ep)
        {
            Console.WriteLine(ep.Message);
            return RedirectToAction("Index", "Email");
        }
    }

}
