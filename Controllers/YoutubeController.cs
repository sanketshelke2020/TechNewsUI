using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using TechNewsUI.Models;

namespace TechNewsUI.Controllers
{
    public class YoutubeController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:5000/");
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> GetAllYoutube()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "api/v3/Youtube/GetAllYoutube").Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseData = response.Content.ReadAsStringAsync().Result;
                    var res = JsonConvert.DeserializeObject<ResponseVM>(responseData);

                    List<YoutubeDto> youtubes = JsonConvert.DeserializeObject<List<YoutubeDto>>(JsonConvert.SerializeObject(res.Data));


                    return View(youtubes);
                }
                return View();
            }
        }
        //[HttpGet]
        //public async Task<ActionResult> YoutubeDetail(int SectionMasterId)
        //{
        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = baseAddress;
        //        client.DefaultRequestHeaders.Clear();
        //        HttpResponseMessage response = client.GetAsync(client.BaseAddress + $"api/v3/Youtube/GetYoutubeById?id={SectionMasterId}").Result;

        //        if (response.IsSuccessStatusCode)
        //        {
        //            var responseData = response.Content.ReadAsStringAsync().Result;
        //            var res = JsonConvert.DeserializeObject<ResponseVM>(responseData);
        //            YoutubeDtoDetail youtubeDtoDetail = JsonConvert.DeserializeObject<YoutubeDtoDetail>(JsonConvert.SerializeObject(res.Data));

        //            return View(youtubeDtoDetail);
        //        }
        //        return View();
        //    }
        //}
        [HttpPost]
        
        public async Task<ActionResult> YoutubeDetail(int SectionMasterId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + $"api/v3/Youtube/GetYoutubeById?id={SectionMasterId}").Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseData = response.Content.ReadAsStringAsync().Result;
                    var res = JsonConvert.DeserializeObject<ResponseVM>(responseData);
                    YoutubeDtoDetail youtubeDtoDetail = JsonConvert.DeserializeObject<YoutubeDtoDetail>(JsonConvert.SerializeObject(res.Data));

                    return View(youtubeDtoDetail);
                }
                return View();
            }
        }
        [HttpGet]
        public async Task<ActionResult> GetYoutubeDetail()
        {
            HttpContext.Session.GetString("SectionMasterId");
            int sectionMasterId = int.Parse(HttpContext.Session.GetString("SectionMasterId")?? "0");
            if (sectionMasterId == 0)
            {
                return RedirectToAction("GetAllYoutube");
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + $"api/v3/Youtube/GetYoutubeById?id={sectionMasterId}").Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseData = response.Content.ReadAsStringAsync().Result;
                    var res = JsonConvert.DeserializeObject<ResponseVM>(responseData);
                    YoutubeDtoDetail youtubeDtoDetail = JsonConvert.DeserializeObject<YoutubeDtoDetail>(JsonConvert.SerializeObject(res.Data));

                    return View("YoutubeDetail", youtubeDtoDetail);
                }
                return View("YoutubeDetail");
            }

        }
        [HttpPost]
        public async Task<ActionResult> PostYoutubeDetail(int SectionMasterId)
        {
            HttpContext.Session.SetString("SectionMasterId", SectionMasterId.ToString());
            return RedirectToAction("GetYoutubeDetail");

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

            return RedirectToAction("GetYoutubeDetail" );


        }
    }
}
