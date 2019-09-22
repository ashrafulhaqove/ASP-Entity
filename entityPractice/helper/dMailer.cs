using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace entityPractice.helper
{
    public class dMailer
    {
        protected void SendMail()
        {

            MailMessage msg = new MailMessage();
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
            try
            {
                msg.Subject = "Add Subject";
                msg.Body = "Add Email Body Part";
                msg.From = new MailAddress("websolutionforpharmacies@gmail.com");
                msg.To.Add("dipsgalaxy01@gmail.com");
                msg.IsBodyHtml = true;
                client.Host = "smtp.gmail.com";
                System.Net.NetworkCredential basicauthenticationinfo = new System.Net.NetworkCredential("websolutionforpharmacies@gmail.com", "123websolutionforpharmacies");
                client.Port = int.Parse("587");
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = basicauthenticationinfo;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(msg);
            }
            catch (Exception ex)
            {
                //Response.Write("Main sent failure");
                //log.Error(ex.MeMessagessage);
            }

        }

        internal static void EmailVerificationMail(string to, string token)
        {
            Uri uri = HttpContext.Current.Request.Url;
            string host = uri.Scheme + Uri.SchemeDelimiter + uri.Host + ":" + uri.Port;

            /*string uril = HttpContext.Current.Request.Url.Host;
            //string uril = HttpContext.Current.Request.Url.AbsoluteUri;
            string[] uriArr = uril.Split('/');
            string url = uriArr[0] + "/email/verify/"+token;
            //Response.Write(uri[0]);*/

            host += "/email/verify/" + token;

            string Body = @"
                <style>
                    .jumbotron.text-center {
                        height: 17em;
                    }

                    input.form-control.col-md-6 {
                        width: 50%;
                        margin-right: 1em;
                        display: inline;
                    }

                    div#notes {
                        margin-top: 2%;
                        width: 98%;
                        margin-left: 1%;
                    }

                    @media (min-width: 991px) {
	                    .col-md-9.col-sm-12 {
    	                    margin-left: 12%;
	                    }
                    }
                </style>

                    <div class='container'>
                      <!-- Instructions -->
                      <div class='row' >
                        <div class='alert alert-success col-md-12' role ='alert' id ='notes' >
                          <h4>NOTES</h4>
                          <ul>
                            <li>Click here to verify your email.</li>
    ";
            Body += $"<a href='{host}' '>Click here .</li>";
            Body += @"
                          </ul>
                        </div>
                      </div>
                    </div>
    ";


            MailMessage msg = new MailMessage();
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
            try
            {
                msg.Subject = "Email Verification";
                msg.Body = Body;
                msg.From = new MailAddress("websolutionforpharmacies@gmail.com");
                msg.To.Add(to);
                msg.IsBodyHtml = true;
                client.Host = "smtp.gmail.com";
                System.Net.NetworkCredential basicauthenticationinfo = new System.Net.NetworkCredential("websolutionforpharmacies@gmail.com", "123websolutionforpharmacies");
                client.Port = int.Parse("587");
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = basicauthenticationinfo;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(msg);
            }
            catch (Exception ex)
            {
                //Response.Write("Main sent failure");
                //log.Error(ex.MeMessagessage);
            }



        }



    }
}