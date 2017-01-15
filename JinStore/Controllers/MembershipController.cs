 
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
using System.Security;


namespace JinStore.Controllers
{
    [Authorize]
    public class MembershipController : Controller
    {
        public ActionResult Index()
        {
            //if (User.Identity.IsAuthenticated == false)     //This works, but it's verbose.  Use an attribute!
            //    return RedirectToAction("Login");
            MyAccountModel model = new MyAccountModel();
            model.EmailAddress = User.Identity.Name;

            string publicKey = ConfigurationManager.AppSettings["Braintree.PublicKey"];
            string privateKey = ConfigurationManager.AppSettings["Braintree.PrivateKey"];
            string environment = ConfigurationManager.AppSettings["Braintree.Environment"];
            string merchantId = ConfigurationManager.AppSettings["Braintree.MerchantID"];

            Braintree.BraintreeGateway braintree = new Braintree.BraintreeGateway(environment, merchantId, publicKey, privateKey);
            int userId = -1;
            using (MemberEntities1 e = new MemberEntities1())
            {
                userId = e.CustomerLists.Single(x => x.EmailAddress == User.Identity.Name).ID;
            }
            var customer = braintree.Customer.Find(userId.ToString());
            model.FirstName = customer.FirstName;
            model.LastName = customer.LastName;
            model.Phone = customer.Phone;
            model.Company = customer.Company;
            model.Fax = customer.Fax;
            model.Website = customer.Website;
            return View(model);
        }

        // POST: Index
        [HttpPost]
        public ActionResult Index(MyAccountModel model)
        {


            string publicKey = ConfigurationManager.AppSettings["Braintree.PublicKey"];
            string privateKey = ConfigurationManager.AppSettings["Braintree.PrivateKey"];
            string environment = ConfigurationManager.AppSettings["Braintree.Environment"];
            string merchantId = ConfigurationManager.AppSettings["Braintree.MerchantId"];

            Braintree.BraintreeGateway braintree = new Braintree.BraintreeGateway(environment, merchantId, publicKey, privateKey);
            int userId = -1;
            using (MemberEntities1 e = new MemberEntities1())
            {
                userId = e.CustomerLists.Single(x => x.EmailAddress == User.Identity.Name).ID;
            }
            Braintree.CustomerRequest update = new Braintree.CustomerRequest();
            update.FirstName = model.FirstName;
            update.LastName = model.LastName;
            update.Phone = model.Phone;
            update.Company = model.Company;
            update.Fax = model.Fax;
            update.Website = model.Website;
            braintree.Customer.Update(userId.ToString(), update);

            return View(model);
        }

        // GET: Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            LoginModel model = new LoginModel();
            return View(model);
        }

        // POST: Login

        [HttpPost]
        [AllowAnonymous]
       public ActionResult Login(Models.LoginModel model, string returnUrl = "/")
        {
            if (ModelState.IsValid)
            {
                if (WebSecurity.Login(model.EmailAddress, model.Password, model.PersistCookie))
                {
                    return Redirect(returnUrl);
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
        [AllowAnonymous]

        public ActionResult Registration()
        {
            RegistrationModel model = new RegistrationModel();
            return View(model);
        }

        // POST: Registration
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Registration(RegistrationModel model)
        {
            if (ModelState.IsValid)
            {
                if (WebSecurity.UserExists(model.EmailAddress))
                {
                    ModelState.AddModelError("EmailAddress", "Username already in user");
                }
                else
                {
                    string confirmationToken = WebSecurity.CreateUserAndAccount(model.EmailAddress, model.Password, null, true);
                    string publicKey = ConfigurationManager.AppSettings["Braintree.PublicKey"];
                    string privateKey = ConfigurationManager.AppSettings["Braintree.PrivateKey"];
                    string environment = ConfigurationManager.AppSettings["Braintree.Environment"];
                    string merchantId = ConfigurationManager.AppSettings["Braintree.MerchantID"];
                    
                    
                    Braintree.BraintreeGateway braintree = new Braintree.BraintreeGateway(environment, merchantId, publicKey, privateKey);
                    Braintree.CustomerRequest request = new Braintree.CustomerRequest();
                    request.Email = model.EmailAddress;
                    using (MemberEntities1 entities = new MemberEntities1())
                    {
                        request.Id = entities.CustomerLists.Single(x => x.EmailAddress == model.EmailAddress).ID.ToString();
                    }
                    request.CreditCard = new Braintree.CreditCardRequest();
                    
                    
                    
                    var customerResult = braintree.Customer.Create(request);

                    string confirmationUrl = Request.Url.GetLeftPart(UriPartial.Authority) + "/Membership/Confirm?confirmationToken=" + confirmationToken;
                    
                    string sendGridApiKey = ConfigurationManager.AppSettings["SendGrid.ApiKey"];
                    
                    SendGrid.SendGridAPIClient client = new SendGrid.SendGridAPIClient(sendGridApiKey);
                    
                    Email from = new Email("admin@codingtemple.com");
                    string subject = "Confirm your new account";
                    Email to = new Email(model.EmailAddress);
                    Content content = new Content("text/html", string.Format("<a href=\"{0}\">Confirm</a>", confirmationUrl));
                    
                    Mail mail = new Mail(from, subject, to, content);
                    mail.TemplateId = "00aaf54f-cf22-4cfe-98b5-b20d3cd72354";
                    mail.Personalization[0].AddSubstitution("-link-", confirmationUrl);
                    var response = await client.client.mail.send.post(requestBody: mail.Get());
                    
                    string message = await response.Body.ReadAsStringAsync();
                    return RedirectToAction("ConfirmationSent");
                }
            }
            return View(model);
        }

        // GET: ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            var model = new ForgotPasswordModel();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
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

        [AllowAnonymous]
        public ActionResult ResetSent()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Reset(string resetToken)
        {
            ResetPasswordModel model = new ResetPasswordModel();
            model.ResetToken = resetToken;
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Reset(ResetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                WebSecurity.ResetPassword(model.ResetToken, model.Password);
                TempData["PasswordChanged"] = true;
                return RedirectToAction("Login","Membership");

            }
            return View(model);
        }

        public ActionResult ConfirmationSent()
         {
             return View();
         }

        [AllowAnonymous]
        public ActionResult Confirm(string confirmationToken)
         {
             if (WebSecurity.ConfirmAccount(confirmationToken))
             {
                 TempData["AccountConfirmed"] = true;
                 return RedirectToAction("Login");
             }
             return RedirectToAction("Index", "Home");
         }
    }
}