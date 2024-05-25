using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using TechNewsUI.Models;
using TechNewsUI.Services;

namespace TechNewsUI.Controllers
{
    [UserSessionCheck]
    public class MagazineController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:5000/");
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<ActionResult> GetAllMagzine()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "api/v3/Magazine/GetAllMagazine").Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseData = response.Content.ReadAsStringAsync().Result;
                    var res = JsonConvert.DeserializeObject<ResponseVM>(responseData);
                    List<MagazineListing> allMagazineList = JsonConvert.DeserializeObject<List<MagazineListing>>(JsonConvert.SerializeObject(res.Data));
                    return View(allMagazineList);
                }
                return RedirectToAction("HomePage", "GetBreakingNews", new { area = "" });


            }
        }
        //[HttpGet]
        //public async Task<ActionResult> GetMagazineById(int? SectionMasterId)
        //{

        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = baseAddress;
        //        client.DefaultRequestHeaders.Clear();
        //        HttpResponseMessage response = client.GetAsync(client.BaseAddress + $"api/v3/Magazine/GetAllMagazineById?id={SectionMasterId}").Result;

        //        if (response.IsSuccessStatusCode)
        //        {
        //            var responseData = response.Content.ReadAsStringAsync().Result;
        //            var res = JsonConvert.DeserializeObject<ResponseVM>(responseData);

        //            MagazineDetail magazine = JsonConvert.DeserializeObject<MagazineDetail>(JsonConvert.SerializeObject(res.Data));

        //            return View(magazine);
        //        }
        //        return RedirectToAction("Podcast", "GetAllPodcast", new { area = "" });


        //    }
        //}




        [HttpPost]
        public async Task<ActionResult> GetMagazineById([FromForm]int? SectionMasterId)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + $"api/v3/Magazine/GetAllMagazineById?id={SectionMasterId}").Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseData = response.Content.ReadAsStringAsync().Result;
                    var res = JsonConvert.DeserializeObject<ResponseVM>(responseData);

                    MagazineDetail magazine = JsonConvert.DeserializeObject<MagazineDetail>(JsonConvert.SerializeObject(res.Data));

                    return View(magazine);
                }
                return RedirectToAction("Podcast", "GetAllPodcast", new { area = "" });


            }
        }
        [HttpGet]
        public async Task<ActionResult> GetMagazineDetail()
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
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + $"api/v3/Magazine/GetAllMagazineById?id={sectionMasterId}").Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseData = response.Content.ReadAsStringAsync().Result;
                    var res = JsonConvert.DeserializeObject<ResponseVM>(responseData);

                    MagazineDetail magazine = JsonConvert.DeserializeObject<MagazineDetail>(JsonConvert.SerializeObject(res.Data));

                    return View("GetMagazineById",magazine);
                }
                return RedirectToAction("GetMagazineById");


            }
        }
        [HttpPost]
        public async Task<ActionResult> PostMagazineDetail(int SectionMasterId)
        {
            HttpContext.Session.SetString("SectionMasterId", SectionMasterId.ToString());
            return RedirectToAction("GetMagazineDetail");
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

            return RedirectToAction("GetMagazineDetail");


        }
    }
}
