using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using TechNewsUI.Models;

namespace TechNewsUI.Controllers
{
    public class LoginController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:5000/");
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginDto login)
        {
            if (ModelState.IsValid)
            {
                string data = JsonConvert.SerializeObject(login);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpClient client = new HttpClient();
                client.BaseAddress = baseAddress;
                HttpResponseMessage response = client.PostAsync(client.BaseAddress + "api/v1/Login/Login", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    string responseData = response.Content.ReadAsStringAsync().Result;
                    LoginResponseDto AuthData = JsonConvert.DeserializeObject<LoginResponseDto>(responseData);

                    HttpContext.Session.SetString("UserId", AuthData.Id);
                    HttpContext.Session.SetString("Email", AuthData.Email);
                    HttpContext.Session.SetString("Token", AuthData.Token);
                    HttpContext.Session.SetString("Role", AuthData.Role);
                    //SessionHelper.SetObjectAsJson(HttpContext.Session, "user", AuthData);
                    if (AuthData.Role == "Admin")
                    {
                        return RedirectToAction("GetSectionMasterList", "Admin", new { section = "Article" });
                    }
                    TempData["LoginUser"] = $"Login with : {login.Email}";
                    
                    return RedirectToRoute(new { controller = "HomePage", action = "GetBreakingNews" });
                }
                TempData["Error"] = "Invalid Email or Password";
                return View();
            }
            return View();
        }

        [HttpGet]
        public IActionResult GuestLogin()
        {
            return View();
        }
        [HttpPost]
        public IActionResult GuestLogin(GuestLoginDto loginDto)
        {
            if (ModelState.IsValid)
            {
                string data = JsonConvert.SerializeObject(loginDto);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpClient client = new HttpClient();
                client.BaseAddress = baseAddress;
                HttpResponseMessage response = client.PostAsync(client.BaseAddress + "api/v3/GuestUser", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    string responseData = response.Content.ReadAsStringAsync().Result;
                    GuestResponseDto AuthData = JsonConvert.DeserializeObject<GuestResponseDto>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<ResponseVM>(responseData).Data));

                    HttpContext.Session.SetInt32("UserId", AuthData.GuestUserId);
                    HttpContext.Session.SetString("Email", AuthData.Email);
                    TempData["LoginGuest"] = "Login as a guest user";
                    return RedirectToRoute(new { controller = "HomePage", action = "GetBreakingNews" });
                }
                TempData["Error"] = "Faild to Login User";
                return View();
            }
            return View();
        }
        public ActionResult Logut()
        {
            HttpContext.Session.Remove("UserId");
            HttpContext.Session.Remove("Email");
            HttpContext.Session.Remove("Token");
            HttpContext.Session.Remove("Role");

            return RedirectToRoute(new { controller = "Login", action = "Login" });
        }
        //asp-action="LoginUser" asp-controller="Login"



    }
}
