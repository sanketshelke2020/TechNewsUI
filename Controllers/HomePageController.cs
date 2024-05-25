using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using TechNewsUI.Models;
using TechNewsUI.Services;

namespace TechNewsUI.Controllers
{
    [UserSessionCheck]
    public class HomePageController : Controller
    {

        Uri baseAddress = new Uri("https://localhost:5000/");

        public IActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> GetBreakingNews()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "api/v3/HomePage/BreakingNews").Result;
                var responseData = response.Content.ReadAsStringAsync().Result;
                var res = JsonConvert.DeserializeObject<ResponseVM>(responseData);

                List<BreakingNews> breakingNews = JsonConvert.DeserializeObject<List<BreakingNews>>(JsonConvert.SerializeObject(res.Data));
                HttpResponseMessage response2 = client.GetAsync(client.BaseAddress + "api/v3/HomePage/TreandingVideos").Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseData2 = response2.Content.ReadAsStringAsync().Result;
                    var res2 = JsonConvert.DeserializeObject<ResponseVM>(responseData2);

                    List<BreakingNews> treandingVedios = JsonConvert.DeserializeObject<List<BreakingNews>>(JsonConvert.SerializeObject(res2.Data));
                    ViewBag.treands = treandingVedios;

                }
                return View(breakingNews);


            }
        }
        [HttpGet]
        public async Task<ActionResult> GetBreakingNewsDetail()
        {
            HttpContext.Session.GetString("SectionMasterId");
            int sectionMasterId = int.Parse(HttpContext.Session.GetString("SectionMasterId") ?? "0");
            if (sectionMasterId == 0)
            {
                return RedirectToAction("GetAllArticle");
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "api/v3/HomePage/BreakingNews").Result;
                var responseData = response.Content.ReadAsStringAsync().Result;
                var res = JsonConvert.DeserializeObject<ResponseVM>(responseData);

                List<BreakingNews> breakingNews = JsonConvert.DeserializeObject<List<BreakingNews>>(JsonConvert.SerializeObject(res.Data));
                HttpResponseMessage response2 = client.GetAsync(client.BaseAddress + "api/v3/HomePage/TreandingVideos").Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseData2 = response2.Content.ReadAsStringAsync().Result;
                    var res2 = JsonConvert.DeserializeObject<ResponseVM>(responseData2);

                    List<BreakingNews> treandingVedios = JsonConvert.DeserializeObject<List<BreakingNews>>(JsonConvert.SerializeObject(res2.Data));
                    ViewBag.treands = treandingVedios;

                }
                return View(breakingNews);


            }
        }
        [HttpPost]
        public async Task<ActionResult> PostBreakingNewsDetail(int SectionMasterId)
        {
            HttpContext.Session.SetString("SectionMasterId", SectionMasterId.ToString());
            return RedirectToAction("GetBreakingNewsDetail");
        }


        public async Task<ActionResult> TreandingVideos()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "api/v3/HomePage/TreandingVideos").Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseData = response.Content.ReadAsStringAsync().Result;
                    var res = JsonConvert.DeserializeObject<ResponseVM>(responseData);

                    List<BreakingNews> treandingVedios = JsonConvert.DeserializeObject<List<BreakingNews>>(JsonConvert.SerializeObject(res.Data));
                    ViewBag.treands = treandingVedios;
                    return View(treandingVedios);
                }
                return View();


            }
        }
        [HttpGet]
        public async Task<ActionResult> GetTrendingVideosDetail()
        {
            HttpContext.Session.GetString("SectionMasterId");
            int sectionMasterId = int.Parse(HttpContext.Session.GetString("SectionMasterId") ?? "0");
            if (sectionMasterId == 0)
            {
                return RedirectToAction("GetAllYoutube");
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "api/v3/HomePage/TreandingVideos").Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseData = response.Content.ReadAsStringAsync().Result;
                    var res = JsonConvert.DeserializeObject<ResponseVM>(responseData);

                    List<BreakingNews> treandingVedios = JsonConvert.DeserializeObject<List<BreakingNews>>(JsonConvert.SerializeObject(res.Data));
                    ViewBag.treands = treandingVedios;
                    return View("TreandingVideos",treandingVedios);
                }
                return View("TreandingVideos");


            }
        }
        [HttpPost]
        public async Task<ActionResult> PostTrendingVideosDetail(int SectionMasterId)
        {
            HttpContext.Session.SetString("SectionMasterId", SectionMasterId.ToString());
            return RedirectToAction("GetTrendingVideosDetail");
        }



        [HttpPost]

        [Route("/HomePage/AddNewsLetter")]
        public JsonResult AddNewsLetter(string email)
        {
            NewsLetter newsLetter = new NewsLetter();
            newsLetter.Email = email;
            if (ModelState.IsValid)
            {
                string data = JsonConvert.SerializeObject(newsLetter);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpClient client = new HttpClient();
                client.BaseAddress = baseAddress;
                HttpResponseMessage response = client.PostAsync(client.BaseAddress + "api/v3/HomePage/AddNewsLetter", content).Result;
                //TempData["NewsLetter"] = "See you soon!!";
                return Json(true);
            }
            return Json(false);
        }
        [HttpGet]
        public JsonResult EmailsDoesNotExists(string email)
        {
            if (email != null)
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = baseAddress;
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + $"api/v3/HomePage/AddNewsLetter?email={email}").Result;
                var responseData = response.Content.ReadAsStringAsync().Result;
                var res = JsonConvert.DeserializeObject<ResponseVM>(responseData);
                var isEmailExist = JsonConvert.DeserializeObject<bool>(JsonConvert.SerializeObject(res.Data));
                return Json(isEmailExist);
            }
            else
            {
                return Json("wrong");
            }
        }
        [HttpGet]
        public ActionResult SubscribtionPlan()
        {
            return View();
        }
        [HttpGet]
        public IActionResult MyProfile(int id)
            {
            HttpClient client = new HttpClient();
            client.BaseAddress = baseAddress;
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + $"api/Register/GetUserById?id={id}&api-version=1").Result;
            string data = response.Content.ReadAsStringAsync().Result;
            var res = JsonConvert.DeserializeObject<ResponseVM>(data);
            UpdateUser user = JsonConvert.DeserializeObject<UpdateUser>(JsonConvert.SerializeObject(res.Data));
            int userIdfromSession = int.Parse(HttpContext.Session.GetString("UserId"));
            if(user.UserId != userIdfromSession)
            {
                return RedirectToAction( "GetBreakingNews", "HomePage", new { area = "" });
            }
            ViewBag.CountryIdselected = user.CountryId != null ? user.CountryId : 0;
            ViewBag.StateIdselected = user.StateId != null ? user.StateId : 0;
            ViewBag.CityIdselected = user.CityId != null ? user.CityId : 0;
            return View(user);
        }

        [HttpPost]
        public IActionResult MyProfile(UpdateUser updateUser)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = baseAddress;
            HttpResponseMessage responseget = client.GetAsync(client.BaseAddress + $"api/Register/GetUserById?id={updateUser.UserId}&api-version=1").Result;
            string dataget = responseget.Content.ReadAsStringAsync().Result;
            var res = JsonConvert.DeserializeObject<ResponseVM>(dataget);
            UpdateUser user = JsonConvert.DeserializeObject<UpdateUser>(JsonConvert.SerializeObject(res.Data));
            updateUser.PasswordHash = user.PasswordHash;
            updateUser.PasswordSalt = user.PasswordSalt;
            ModelState.Remove("PasswordSalt");
            ModelState.Remove("PasswordHash");
            if (ModelState.IsValid)
            {
                string data = JsonConvert.SerializeObject(updateUser);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(client.BaseAddress + "api/Register/UpdateUser", content).Result;
                TempData["updateUser"] = "Your Profile Updated!!";
                return RedirectToAction("MyProfile", new { id = updateUser.UserId });
            }
            return RedirectToAction("MyProfile", new { id = updateUser.UserId });
        }

        [HttpGet]
        public async Task<ActionResult> GetSectionByKeyword(string searchKeyword)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + $"api/v3/Search/GetSectionByKeyword?searchKeyword={searchKeyword}").Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseData = response.Content.ReadAsStringAsync().Result;
                    var res = JsonConvert.DeserializeObject<ResponseVM>(responseData);
                    List<GetSectionByKeywordDto> getSectionByKeywordDtos = JsonConvert.DeserializeObject<List<GetSectionByKeywordDto>>(JsonConvert.SerializeObject(res.Data));
                    return View(getSectionByKeywordDtos);
                }
                return RedirectToAction("GetBreakingNews", "HomePage", new { area = "" });  
            }
        }

        [HttpGet]
        public JsonResult GetNotifications(string i)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = baseAddress;
            if(i == "count")
            {
                HttpResponseMessage response1 = client.GetAsync(client.BaseAddress + $"api/v3/Notification/GetSectionAddedToday?i=count").Result;
                var responseData1 = response1.Content.ReadAsStringAsync().Result;
                return Json(responseData1);
            }
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + $"api/v3/Notification/GetSectionAddedToday?i=notification").Result;
            var responseData = response.Content.ReadAsStringAsync().Result;
            var res = JsonConvert.DeserializeObject<ResponseVM>(responseData);
            List<GetSectionByKeywordDto> notifications = JsonConvert.DeserializeObject<List<GetSectionByKeywordDto>>(JsonConvert.SerializeObject(res.Data));
            var result = notifications.OrderByDescending(x => x.CreatedDate);
            return Json(result);
        }
    }
}