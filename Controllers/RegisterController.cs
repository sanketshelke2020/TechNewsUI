using Microsoft.AspNetCore.Mvc;
using TechNewsUI.Models;
using System.Net.Http;
using Newtonsoft.Json;
using NuGet.Protocol.Plugins;
using System.Text;
using System.Net.Mail;

namespace TechNewsUI.Controllers
{
    public class RegisterController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:5000/");
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> AddUser()
        {
            return  View();
        }
        [HttpPost]
        public async Task<IActionResult> AddUser(RegisterDto registerDto)
        {
            if (ModelState.IsValid)
            {
                string data = JsonConvert.SerializeObject(registerDto);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
               

                HttpClient client = new HttpClient();

                client.BaseAddress = baseAddress;
                HttpResponseMessage response = client.PostAsync(client.BaseAddress + "api/Register", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    string responseData = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine(responseData);
                   
                    //TempData["success"] = "Register Successfull";
                    TempData["successRegister"] = "Register Successfull";
                    return RedirectToRoute(new { controller = "Login", action = "Login" });
                }
                TempData["Error"] = "Fail3d to Register User";
                return View();
            }
            return View();


        }
     

        [HttpPost]
        public JsonResult GetAllCountries()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = baseAddress;
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "api/Register/GetAllCountry").Result;
            string data = response.Content.ReadAsStringAsync().Result;
            List<Country> countries = JsonConvert.DeserializeObject<List<Country>>(data);
            return Json(countries);
        }
        [HttpPost]
        public JsonResult GetAllStates(int countryId)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = baseAddress;
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "api/Register/GetAllState?countryId=" + countryId.ToString()).Result;
            string data = response.Content.ReadAsStringAsync().Result;
            List<State> states = JsonConvert.DeserializeObject<List<State>>(data);
            return Json(states);
        }

        [HttpPost]
        public JsonResult GetAllCities(int stateId)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = baseAddress;
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "api/Register/GetAllCity?stateId=" + stateId.ToString()).Result;
            string data = response.Content.ReadAsStringAsync().Result;
            List<City> states = JsonConvert.DeserializeObject<List<City>>(data);
            return Json(states);
        }
        [HttpPost]
        public JsonResult GetAllUserRoles()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = baseAddress;
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "api/Register/GetAllUserRoles").Result;
            string data = response.Content.ReadAsStringAsync().Result;
            List<UserRole> states = JsonConvert.DeserializeObject<List<UserRole>>(data);
            return Json(states);
        }

        [HttpGet]
        public JsonResult EmailsDoesNotExists(string email)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = baseAddress;
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + $"api/Register/EmailsDoesNotExists?email={email}&api-version=1").Result;
            string data = response.Content.ReadAsStringAsync().Result;
            bool isEmailExist = JsonConvert.DeserializeObject<bool>(data);
            return Json(isEmailExist);
        }
    }
}
