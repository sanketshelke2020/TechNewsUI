using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using TechNewsUI.Models;

namespace TechNewsUI.Controllers
{
    public class ForgetController : Controller
    {
        private readonly ILogger<ForgetController> _logger;
        Uri baseAddress = new Uri("https://localhost:5000/api/v1/");
        HttpClient client;

        public ForgetController(ILogger<ForgetController> logger)
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        [Route("Forget/ForgetPassword")]
        //[ValidateAntiForgeryToken]
        public ActionResult ForgetPassword(ForgetPassword forgetPassword)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }
            string data = JsonConvert.SerializeObject(forgetPassword);
            StringContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync("ForgotPassword/ForgotUserPassword", content).Result;
            HttpContext.Session.SetString("Email", forgetPassword.Email);
            TempData["SentMessage"] = "Link has been sent to your mail";
            return View();



        }
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ResetPassword(string emailId)
        {
            ResetPassword resetPassword = new ResetPassword();
            resetPassword.Email = emailId;
            ViewBag.email = emailId;
            return View(resetPassword);
        }

        [HttpPost]
        public ActionResult ResetPasswordMail([FromForm] string emailId)
        {
            ResetPassword resetPassword = new ResetPassword();
            resetPassword.Email = emailId;
            ViewBag.email = emailId;
            return View("ResetPassword", resetPassword);
        }

        [HttpPost]
        [Route("ResetUserPassword")]
        public ActionResult ResetPassword(ResetPassword resetPassword)
        {
            var result = EmailsDoesNotExists(resetPassword.Email);
            if (result == false)
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                var value = HttpContext.Session.GetString("Email");

                Console.WriteLine(value);
                SessionData sessionData = new SessionData();
                sessionData.Email = resetPassword.Email;
                sessionData.NewPassword = resetPassword.NewPassword;
                sessionData.ConfirmPassword = resetPassword.ConfirmPassword;
                string data = JsonConvert.SerializeObject(sessionData);
                StringContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync("ForgotPassword/ResetUserPassword", content).Result;
                if (response != null)
                {
                    TempData["ChangedSuccessfully"] = "Password Changed succesfully, please click on link to redirect";

                }

            }
            else
            {

                TempData["EmailIdDoesntExist"] = "Email Id Doesn't Exist!!";
                return View();
            }
            return View();

        }
        public bool EmailsDoesNotExists(string email)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = baseAddress;
            HttpResponseMessage response = client.GetAsync($"https://localhost:5000/api/Register/EmailsDoesNotExists?email={email}"+"&api-version=1").Result;
            string data = response.Content.ReadAsStringAsync().Result;
            bool isEmailExist = JsonConvert.DeserializeObject<bool>(data);
            return isEmailExist;
        }


        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        //[Route("Forget/ChangePassword")]
        public ActionResult ChangePassword(ChangePassword changePassword)
        {
            changePassword.Email = HttpContext.Session.GetString("Email");
            ModelState.Remove(changePassword.Email);
            if (ModelState.IsValid)
            {

                //changePassword.Email = HttpContext.Session.GetString("Email");
                string data = JsonConvert.SerializeObject(changePassword);
                StringContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync("ForgotPassword/ChangePassword", content).Result;
                if (response != null)
                {
                    TempData["AddedSuccessful"] = "Password Changed succesfully";
                    return View();
                }

                return RedirectToAction("ChangePassword");

            }
            return View();
        }

        //remote validation for Old password
        [HttpPost]
        public JsonResult OldPasswordDoesNotExists(string OldPassword)
        {

            OldPasswordExists oldPasswordExists = new OldPasswordExists();

            oldPasswordExists.Email = HttpContext.Session.GetString("Email");
            oldPasswordExists.OldPassword = OldPassword;
            string data = JsonConvert.SerializeObject(oldPasswordExists);
            StringContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync("ForgotPassword/OldPasswordDoesNotExists", content).Result;
            string data1 = response.Content.ReadAsStringAsync().Result;
            bool isOldPasswordExist = JsonConvert.DeserializeObject<bool>(data1);
            return Json(isOldPasswordExist);


        }
    }
}
