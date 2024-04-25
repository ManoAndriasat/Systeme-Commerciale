using Npgsql;
using System;
using tools;
using EASendMail;

namespace tools
{
    public class Fournisseur
    {
        public int id_fournisseur;
        public String nom_fournisseur;
        public String contact;
        public String email;
        
        public void setNomFournisseur(String n){
            nom_fournisseur = n;
        }
        public String getNomFournisseur(){
            return nom_fournisseur ;
        }
        public void setContact(String n){
            contact = n;
        }
        public String getContact(){
            return contact ;
        }
        public void setEmail(String n){
            email = n;
        }
        public String getEmail(){
            return email ;
        }

        
        public void setIdFournisseur(int n){
            id_fournisseur = n;
        }
        public int getIdFournisseur(){
            return id_fournisseur;
        }


        public Fournisseur(){}

        public static void sendProformaEmail(string Subject, string TextBody, string To, IFormFile Attachment)
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
            }
            catch (Exception ep)
            {
                Console.WriteLine(ep.Message);
            }
        }
    }
}
