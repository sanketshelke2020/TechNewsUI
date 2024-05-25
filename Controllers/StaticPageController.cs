using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using TechNewsUI.Models;

namespace TechNewsUI.Controllers
{
    public class StaticPageController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:5000/");

        [HttpGet]
        public IActionResult AboutUs()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddUserQuery()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddUserQuery(UserQuery userQuery)
        {
            if (HttpContext.Session.GetString("Token") != null)
            {
                userQuery.UserId = int.Parse(HttpContext.Session.GetString("UserId"));
                string data = JsonConvert.SerializeObject(userQuery);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");


                HttpClient client = new HttpClient();

                client.BaseAddress = baseAddress;
                HttpResponseMessage response = client.PostAsync(client.BaseAddress + "api/v3/Admin/AddUserQuery", content).Result;
                string responseData = response.Content.ReadAsStringAsync().Result;
                TempData["querySubmit"] = "Your Query has been Submitted";

                return RedirectToRoute(new { controller = "StaticPage", action = "AddUserQuery" });

            }
            return View();
               


        }
        public ActionResult FAQ()
        {
            return View();
        }
        public ActionResult TermsAndCondition()
        {
            return View();
          
        }
        public ActionResult PrivacyPolicy()
        {
            return View();

        }
        public ActionResult Career()
        {
            return View();

        }
    }
}
