using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JinStore.Models
{
    public class ForgotPasswordController : Controller
    {
        // GET: ForgotPassword
        public ActionResult Index()
        {
            var model = new Models.ForgotPasswordModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(Models.ForgotPasswordModel model)
        {
            if (WebMatrix.WebData.WebSecurity.UserExists(model.EmailAddress))
            {
                string resetToken = WebMatrix.WebData.WebSecurity.GeneratePasswordResetToken(model.EmailAddress);
                string resetUrl = Request.Url.GetLeftPart(UriPartial.Authority) + "ForgotPassword/Reset?resetToken=" + resetToken;
                //TODO: Send this to the email address when we get email working!
            }

            return RedirectToAction("ResetSent");
        }

        public ActionResult ResetSent()
        {
            return View();
        }

        public ActionResult Reset(string resetToken)
        {
            Models.ResetPasswordModel model = new Models.ResetPasswordModel();
            model.ResetToken = resetToken;
            return View(model);
        }

        [HttpPost]
        public ActionResult Reset(Models.ResetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                WebMatrix.WebData.WebSecurity.ResetPassword(model.ResetToken, model.Password);
                return RedirectToAction("Index", "Login");
            }
            return View(model);
        }
    }
}