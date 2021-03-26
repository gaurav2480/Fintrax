using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;

public partial class WebSite5_production_mail_sending : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        



    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            string to = "gauravdessai18@gmail.com"; //To address    
            string from = "gauravdessai18@gmail.com"; //From address    
            MailMessage message = new MailMessage(from, to, "<p>Fintrax", "HELLO GAURAV, Please check the attached file</p>");
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
            client.EnableSsl = true;
            client.Credentials = new System.Net.NetworkCredential("gauravdessai18@gmail.com", "gauravdeelipphaldessai12345");
            client.Send(message);
        }

        catch (Exception ex)
        {
            Label1.Text = ex.StackTrace;
        }
    }
}