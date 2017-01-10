 
using JinStore.Models;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace JinStore.Controllers
{
    public class MembershipController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            LoginModel model = new LoginModel();
            return View(model);
        }

        // POST: Login

        [HttpPost]
        public ActionResult Login(JinStore.Models.LoginModel model)
        {
            if (ModelState.IsValid)
            {
                if (WebSecurity.Login(model.EmailAddress, model.Password, model.PersistCookie))
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("EmailAddress", "Username or password was incorrect.");

            }
            return View(model);
        }

        // GET: Logout
        public ActionResult Logout()
        {
            WebSecurity.Logout();
            return RedirectToAction("Index", "Home");
        }

        // GET: Registration
        public ActionResult Registration()
        {
            RegistrationModel model = new RegistrationModel();
            return View(model);
        }

        // POST: Registration
        [HttpPost]
        public ActionResult Registration(RegistrationModel model)
        {
            if (ModelState.IsValid)
            {
                if (WebSecurity.UserExists(model.EmailAddress))
                {
                    ModelState.AddModelError("EmailAddress", "Username already in user");
                }
                else
                {
                    WebSecurity.CreateUserAndAccount(model.EmailAddress, model.Password, null, false);
                    WebSecurity.Login(model.EmailAddress, model.Password, true);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        // GET: ForgotPassword
        public ActionResult ForgotPassword()
        {
            var model = new ForgotPasswordModel();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> ForgotPassword(JinStore.Models.ForgotPasswordModel model)
        {
            if (WebSecurity.UserExists(model.EmailAddress))
            {
                string resetToken = WebSecurity.GeneratePasswordResetToken(model.EmailAddress);
                string resetUrl = Request.Url.GetLeftPart(UriPartial.Authority) + "/Membership/Reset?resetToken=" + resetToken;

                string sendGridApiKey = ConfigurationManager.AppSettings["SendGrid.ApiKey"];

                SendGrid.SendGridAPIClient client = new SendGrid.SendGridAPIClient(sendGridApiKey);

                Email from = new Email("admin@codingtemple.com");
                string subject = "Instructions for Resetting Your Password";
                Email to = new Email(model.EmailAddress);
                Content content = new Content("text/html", string.Format("<a href=\"{0}\">Reset your password</a>", resetUrl));
                Mail mail = new Mail(from, subject, to, content);

                var response = await client.client.mail.send.post(requestBody: mail.Get());

                string message = await response.Body.ReadAsStringAsync();
            }

            return RedirectToAction("ResetSent");
        }

        public ActionResult ResetSent()
        {
            return View();
        }

        public ActionResult Reset(string resetToken)
        {
            ResetPasswordModel model = new ResetPasswordModel();
            model.ResetToken = resetToken;
            return View(model);
        }

        [HttpPost]
        public ActionResult Reset(ResetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                WebSecurity.ResetPassword(model.ResetToken, model.Password);
                TempData["PasswordChanged"] = true;
                return RedirectToAction("Login");

            }
            return View(model);
        }
    }
}