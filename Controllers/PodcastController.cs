using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection;
using System.Text;
using TechNewsUI.Models;
using TechNewsUI.Services;

namespace TechNewsUI.Controllers
{
    [UserSessionCheck]
    public class PodcastController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:5000/");
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> GetAllPodcast()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "api/v3/Podcast/GetAllPodcast").Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseData = response.Content.ReadAsStringAsync().Result;
                    var res = JsonConvert.DeserializeObject<ResponseVM>(responseData);
                    List<Podcast> podcasts = JsonConvert.DeserializeObject<List<Podcast>>(JsonConvert.SerializeObject(res.Data));
                    return View(podcasts);
                }
                return RedirectToAction( "GetBreakingNews", "HomePage", new { area = "" });


            }
        }

        [HttpPost]
        //[Route("{SectionMasterId}")]
        public async Task<ActionResult> GetPodcastById(int? SectionMasterId)
            {

            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + $"api/v3/Podcast/GetPodcastById?id={SectionMasterId}").Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseData = response.Content.ReadAsStringAsync().Result;
                    var res = JsonConvert.DeserializeObject<ResponseVM>(responseData);
     
                    Podcast podcasts = JsonConvert.DeserializeObject<Podcast>(JsonConvert.SerializeObject(res.Data));
                   
                    return View(podcasts);
                }
                return RedirectToAction( "GetAllPodcast", "Podcast", new { area = "" });


            }
        }
        [HttpGet]
        public async Task<ActionResult> GetPodacastDetail()
        {
            HttpContext.Session.GetString("SectionMasterId");
            int sectionMasterId = int.Parse(HttpContext.Session.GetString("SectionMasterId") ?? "0");
            if (sectionMasterId == 0)
            {
                return RedirectToAction("GetAllPodcast");
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + $"api/v3/Podcast/GetPodcastById?id={sectionMasterId}").Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseData = response.Content.ReadAsStringAsync().Result;
                    var res = JsonConvert.DeserializeObject<ResponseVM>(responseData);

                    Podcast podcasts = JsonConvert.DeserializeObject<Podcast>(JsonConvert.SerializeObject(res.Data));

                    return View("GetPodcastById",podcasts);
                }
                return RedirectToAction("GetPodcastById");


            }
        }
        [HttpPost]
        public async Task<ActionResult> PostPodacastDetail(int SectionMasterId)
        {
            HttpContext.Session.SetString("SectionMasterId", SectionMasterId.ToString());
            return RedirectToAction("GetPodacastDetail");

        }


        public async Task<ActionResult> GetChapterById(int id)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "api/v3/Podcast/GetChapterById?id=" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseData = response.Content.ReadAsStringAsync().Result;
                    var res = JsonConvert.DeserializeObject<ResponseVM>(responseData);
       
                    PodcastChapter podcasts = JsonConvert.DeserializeObject<PodcastChapter>(JsonConvert.SerializeObject(res.Data));
                    
                    
                    return View(podcasts);
                }
                return RedirectToAction( "GetBreakingNews", "HomePage", new { area = "" });
            }
        }
        public async Task<ActionResult> AddComment(string commentdis, int id)
        {
            Comment comment = new Comment();
            comment.CommentDescription = commentdis;
            comment.Email = HttpContext.Session.GetString("Email");
            comment.SectionMasterId = id;
            
            
                string data = JsonConvert.SerializeObject(comment);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpClient client = new HttpClient();
                client.BaseAddress = baseAddress;
                HttpResponseMessage response = client.PostAsync(client.BaseAddress + "api/v3/Comment/AddComment", content).Result;
              
                return RedirectToAction("GetPodacastDetail");

           
        }
        [HttpPost]
        public JsonResult GetChapterByIdJson(int id)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "api/v3/Podcast/GetChapterById?id=" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseData = response.Content.ReadAsStringAsync().Result;
                    var res = JsonConvert.DeserializeObject<ResponseVM>(responseData);

                    PodcastChapter podcasts = JsonConvert.DeserializeObject<PodcastChapter>(JsonConvert.SerializeObject(res.Data));

                    return Json(podcasts);
                }
                return null;
            }
        }
    }
}
