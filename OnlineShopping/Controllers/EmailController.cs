using OnlineShopping.DomainModel;                       //Database Model
using System;                                           //Exception Handling
using System.Linq;                                      //where function used
using System.Net.Mail;                                  //MailAddress
using System.Text;                                      //String Builder
using System.Web.Hosting;                               // Hosting Environment
using System.Web.Mvc;                                   //Controller,ActionResult,TempData,RedirectToAction

namespace OnlineShopping.Controllers
{

    /// <summary>
    /// Email Controller : This controller works for sending product confirmation mail to the customer
    /// </summary>
    public class EmailController : Controller
    {
        
        public ActionResult BuildEmailTemplate(int id)
        {
            try
            {
                OnlineShoppingDbcontext onlineShoppingDbcontext = new OnlineShoppingDbcontext();
                string body = System.IO.File.ReadAllText(HostingEnvironment.MapPath("~/EmailTemplate/") + "Text" + ".cshtml");
                var regInfo = onlineShoppingDbcontext.BuyRequests.Where(x => x.RequestId == id).FirstOrDefault();
                //body = body.ToString();
                BuildEmailTemplate("Product-Shippment Confirmation", body, regInfo.Email);
                TempData["BuyerEmail"] = "Email sent to the Buyer";
                return RedirectToAction("CompletedOrder", "Product", new { @id = id });
               
            }
            catch (Exception ex)
            {
               
                return View("Error", new HandleErrorInfo(ex, "BuildEmailTemplate", "Email"));
            }
        }

        public static void BuildEmailTemplate(string subjectText, string bodyText, string sendTo)
        {
            string from, to, bcc, cc, subject;
            from = "ecommercesample86@gmail.com";
            to = sendTo.Trim();
            bcc = "";
            cc = "";
            subject = subjectText;
            StringBuilder sb = new StringBuilder();
            sb.Append(bodyText);
            //body = sb.ToString();
            MailMessage mail = new MailMessage();
            mail.From=new MailAddress(from);
            mail.To.Add(new MailAddress(to));
            if (!string.IsNullOrEmpty(bcc))
            {
                mail.Bcc.Add(new MailAddress(bcc));
            }
            if (!string.IsNullOrEmpty(cc))
            {
                mail.CC.Add(new MailAddress(cc));
            }
            mail.Subject = subject;
            mail.Body = bodyText;
            mail.IsBodyHtml = true;
            SendEmail(mail);
        }

        public static void SendEmail(MailMessage mail)
        {
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.Port = 587;

            client.UseDefaultCredentials = false;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new System.Net.NetworkCredential("ecommercesample86@gmail.com", "sam@8765");
            client.EnableSsl = true;
            //try
            //{
                client.Send(mail);
            //}
            
        }
    }
}