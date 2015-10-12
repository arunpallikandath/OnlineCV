using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Web.UI;

namespace OnlineCV.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Skills()
        {
            return View();
        }

        public ActionResult Experience()
        {
            return View();
        }

        public ActionResult Contact()
        {
           
               return View();
            
        }

        [HttpPost]
        public String ContactSave()
        {
            String msgDesc = Request.Form["comments"];
            String msgFName = Request.Form["fullName"];
            String msgEmail = Request.Form["email"];
            String msgPhone = Request.Form["phone"];
            String msgSubject = Request.Form["subject"];
            //
            SmtpClient client = new SmtpClient();
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            client.Host = "smtp.gmail.com";
            client.Port = 587;

            // setup Smtp authentication
            client.UseDefaultCredentials = false;
            System.Net.NetworkCredential credentials =
            new System.Net.NetworkCredential("aruncvonline@gmail.com", "aruncv1234");
            client.Credentials = credentials;

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("aruncvonline@gmail.com");
            msg.To.Add(new MailAddress(msgEmail));
            msg.Subject = msgSubject;
            msg.IsBodyHtml = true;
            msg.Body = string.Format("<html><head></head><body><b>" + msgFName + "</b>" +
                "<p></p>" + msgDesc + "</body>");
            try
            {
                client.Send(msg);
                ViewBag.Message = "Your message has been successfully sent.";
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error occured while sending your message." + ex.Message;
    
            }
            return ViewBag.Message;
        }

    }
}