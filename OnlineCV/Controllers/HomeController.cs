using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Web.UI;
/*
    Main controller of this project, it handles all server actions required for this
 */
namespace OnlineCV.Controllers
{
    public class HomeController : Controller
    {
        /*
         * Function which calls at the startup and its view is home page for this project.
         * This function has been set as default page in RouteConfig.cs
         * -------------------------------------------------------------------------------
         * Description       Author      Date           Comments
         * -------------------------------------------------------------------------------
         * Created           Arun        12-Oct-2015    
         * -------------------------------------------------------------------------------
         */
        public ActionResult Index(String cvName)
        {
            ViewBag.cvName = cvName;
            if(String.IsNullOrWhiteSpace(cvName))
            {
                return View("Home");
            }
            return View();
        }
        /*
         * Landing page
         * 
         * -------------------------------------------------------------------------------
         * Description       Author      Date           Comments
         * -------------------------------------------------------------------------------
         * Created           Arun        17-Oct-2015    
         * -------------------------------------------------------------------------------
         */
        public ActionResult Home()
        {
            return View();
        }

        /*
        * Skill page
        * This function has been called when the menu SKILLS is clicked. 
        * -------------------------------------------------------------------------------
        * Description       Author      Date           Comments
        * -------------------------------------------------------------------------------
        * Created           Arun        12-Oct-2015    
        * -------------------------------------------------------------------------------
        */
        [ChildActionOnly]
        public ActionResult Skills()
        {
            return View();
        }

        /*
        * Experience page
        * This function has been called when the menu Experience is clicked. 
        * -------------------------------------------------------------------------------
        * Description       Author      Date           Comments
        * -------------------------------------------------------------------------------
        * Created           Arun        12-Oct-2015    
        * -------------------------------------------------------------------------------
        */
        [ChildActionOnly]
        public ActionResult Experience()
        {
            return View();
        }

        /*
        * Contact page
        * This function has been called when the menu Contact is clicked. 
        * Allows the user to submit queries and it will send as email to owner mail id 
        * -------------------------------------------------------------------------------
        * Description       Author      Date           Comments
        * -------------------------------------------------------------------------------
        * Created           Arun        12-Oct-2015    
        * -------------------------------------------------------------------------------
        */
        [ChildActionOnly]
        public ActionResult Contact()
        {
             return View();

        }
        /*
        * 
        * This function has been called when the submit button is clicked from contact form 
        * Function to send email using smtp client. 
        * -------------------------------------------------------------------------------
        * Description       Author      Date           Comments
        * -------------------------------------------------------------------------------
        * Created           Arun        12-Oct-2015    
        * -------------------------------------------------------------------------------
        */
        [HttpPost]
        public String ContactSave()
        {
            String msgDesc = Request.Form["comments"];
            String msgFName = Request.Form["fullName"];
            String msgEmail = Request.Form["email"];
            String msgPhone = Request.Form["phone"];
            String msgSubject = "OnlineCV - Enquiry";
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
            msg.From = new MailAddress(msgEmail);
            msg.To.Add(new MailAddress("aruncvonline@gmail.com"));
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