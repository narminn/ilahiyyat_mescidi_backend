using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Ilahiyyat.Models;
using Ilahiyyat.ViewModel;

namespace Ilahiyyat.Areas.Admin_Panel.Controllers
{
    [FilterAdmin]
    public class HomeController : Controller
    {
        ilahiyyetEntities db = new ilahiyyetEntities();
        MessageModel mm = new MessageModel();
       
       
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(FormCollection frm)
        {
            string email = frm["a_email"];
            string password = frm["a_password"];
            var ad = db.Admins.Where(e => e.admin_email == email && e.admin_şifrəsi == password).FirstOrDefault();
            if (ad != null)
            {
                var admin = db.Admins.First(p => p.admin_email == email && p.admin_şifrəsi == password);
                Session["AdminEmail"] = ad.admin_email;
                Session["id"] = ad.id;
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Message = "Yanlis email və ya şifrə!";
                return View();
            }
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login");
        }
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(FormCollection frm)
        {
            Admin admn = db.Admins.Find(1);

            string pass = admn.admin_şifrəsi;
            string oldPass = frm["old_password"];
            string newPass = frm["new_password"];
            string confPass = frm["conf_password"];
            if (oldPass == pass && confPass==newPass)
            {
                admn.admin_şifrəsi = newPass;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Message = "Yanlis şifrə!";
                return View();
            }
           
        }
        [AllowAnonymous]
        public ActionResult Forgot()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Forgot(string a_email)
        {
           
            if (a_email =="mezahir.meherrem@mail.ru") {
                try
                {
                    if (ModelState.IsValid)
                    {
                        Admin admn = db.Admins.Find(1);

                        string passw = admn.admin_şifrəsi;
                        MailMessage mailmessage = new MailMessage();
                        SmtpClient connect = new SmtpClient("smpt.gmail.com", 587);
                        mailmessage.From = new MailAddress("ilahiyyat254@gmail.com", "Admin Ilahiyyat Mescidi", System.Text.Encoding.UTF8);
                        mailmessage.To.Add(a_email);
                        mailmessage.Subject = "Şifrəniz";
                        Random random = new Random();
                        mailmessage.Body = passw;
                        connect.Credentials = new NetworkCredential("ilahiyyat254@gmail.com", "5664640hh");
                        connect.EnableSsl = true;
                        connect.Host = "smtp.gmail.com";
                        connect.Send(mailmessage);
                        return RedirectToAction("Login");
                    }
                }
                catch (Exception)
                {
                    ViewBag.Error = "Some Error";
                }
                return View();
            }
            else
            {
                ViewBag.Error = "Some Error";
                return View();
            }
            
        }
        //public ActionResult SendPassword()
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            Admin admn = db.Admins.Find(1);

        //            string passw = admn.admin_şifrəsi;
        //            string rcvr = "narminrn@code.edu.az";
        //            MailMessage mailmessage = new MailMessage();
        //            SmtpClient connect = new SmtpClient("smpt.gmail.com", 587);
        //            mailmessage.From = new MailAddress("ilahiyyat254@gmail.com", "Admin Ilahiyyat Mescidi", System.Text.Encoding.UTF8);
        //            mailmessage.To.Add(rcvr);
        //            mailmessage.Subject ="Şifrəniz";
        //            Random random = new Random();
        //            mailmessage.Body =passw;
        //            connect.Credentials = new NetworkCredential("ilahiyyat254@gmail.com", "5664640hh");
        //            connect.EnableSsl = true;
        //            connect.Host = "smtp.gmail.com";
        //            connect.Send(mailmessage);
        //            return RedirectToAction("Login");
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        ViewBag.Error = "Some Error";
        //    }
        //    return View();
        //}
        public ActionResult Messages()
        {
            mm._contact = db.Contacts.ToList();
            return View(mm);
        }
        public ActionResult DeleteMessage(int?id)
        {
            Contact cnt = db.Contacts.Find(id);
            db.Contacts.Remove(cnt);
            db.SaveChanges();
            return RedirectToAction("Messages");
        }
        public ActionResult SendEmail()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SendEmail(string receiver, string subject, string message)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    MailMessage mailmessage = new MailMessage();
                    SmtpClient connect = new SmtpClient("smpt.gmail.com", 587);
                    mailmessage.From = new MailAddress("ilahiyyat254@gmail.com", "Admin Ilahiyyat Mescidi", System.Text.Encoding.UTF8);
                    mailmessage.To.Add(receiver);
                    mailmessage.Subject = subject;
                    Random random = new Random();
                    mailmessage.Body = message;
                    connect.Credentials = new NetworkCredential("ilahiyyat254@gmail.com", "5664640hh");
                    connect.EnableSsl = true;
                    connect.Host = "smtp.gmail.com";
                    connect.Send(mailmessage);
                    return RedirectToAction("Messages");
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "Some Error";
            }
            return View();
        }
        public ActionResult Quest()
        {
            mm._quest = db.Questions.ToList();
            return View(mm);
        }
        public ActionResult DeleteQuest(int?id)
        {
            Question questions = db.Questions.Find(id);
            db.Questions.Remove(questions);
            db.SaveChanges();
            return RedirectToAction("Quest");
        }
        public ActionResult QuestSendEmail()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult QuestSendEmail(string receiver, string subject, string message)
        {
            
            try
            {
                if (ModelState.IsValid)
                {
                    MailMessage mailmessage = new MailMessage();
                    SmtpClient connect = new SmtpClient("smpt.gmail.com", 587);
                    mailmessage.From = new MailAddress("ilahiyyat254@gmail.com", "Admin Ilahiyyat Mescidi", System.Text.Encoding.UTF8);
                    mailmessage.To.Add(receiver);
                    mailmessage.Subject = subject;
                    Random random = new Random();
                    mailmessage.Body = message;
                    connect.Credentials = new NetworkCredential("ilahiyyat254@gmail.com", "5664640hh");
                    connect.EnableSsl = true;
                    connect.Host = "smtp.gmail.com";
                    connect.Send(mailmessage);
                    var quest_user = db.Questions.Where(q => q.sual_user_email == receiver).FirstOrDefault();
                    Session["quest_id"] = quest_user.id;
                    Question qst = db.Questions.Find(Session["quest_id"]);
                    qst.cavab = message;
                    db.SaveChanges();
                    return RedirectToAction("Quest");

                }
            }
            catch (Exception)
            {
                ViewBag.Error = "Some Error";
            }
           

            return View();
        }

       
    }
}