using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using TechNewsUI.Models;

namespace TechNewsUI.Controllers
{
    public class CaseStudiesController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:5000/");

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<ActionResult> GetAllCaseStudies()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "api/v3/CaseStudies/GetAllCaseStudies").Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseData = response.Content.ReadAsStringAsync().Result;
                    var res = JsonConvert.DeserializeObject<ResponseVM>(responseData);

                    List<CaseStudies> caseStudies = JsonConvert.DeserializeObject<List<CaseStudies>>(JsonConvert.SerializeObject(res.Data));


                    return View(caseStudies);
                }
                return View();
            }
        }
        //[HttpGet]
        //public async Task<ActionResult> CaseStudiesDetail(int SectionMasterId ,int? caseId)
        //{
        //    return View();
        //}

        //[HttpPost]
        //public async Task<ActionResult> CaseStudiesDetail(int SectionMasterId)
        //{
        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = baseAddress;
        //        client.DefaultRequestHeaders.Clear();
        //        HttpResponseMessage response = client.GetAsync(client.BaseAddress + $"api/v3/CaseStudies/GetCaseStudiesById?id={SectionMasterId}").Result;

        //        if (response.IsSuccessStatusCode)
        //        {
        //            var responseData = response.Content.ReadAsStringAsync().Result;
        //            var res = JsonConvert.DeserializeObject<ResponseVM>(responseData);
        //            CaseStudiesDetail caseStudies = JsonConvert.DeserializeObject<CaseStudiesDetail>(JsonConvert.SerializeObject(res.Data));

        //            return View(caseStudies);
        //        }
        //        return View();
        //    }
        //}

        [HttpPost]
        public async Task<ActionResult> CaseStudiesDetail([FromForm] int SectionMasterId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + $"api/v3/CaseStudies/GetCaseStudiesById?id={SectionMasterId}").Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseData = response.Content.ReadAsStringAsync().Result;
                    var res = JsonConvert.DeserializeObject<ResponseVM>(responseData);
                    CaseStudiesDetail caseStudies = JsonConvert.DeserializeObject<CaseStudiesDetail>(JsonConvert.SerializeObject(res.Data));

                    return View(caseStudies);
                }
                return View();
            }
        }



        [HttpGet]
        public async Task<ActionResult> GetCaseStudiesDetail()
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
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + $"api/v3/CaseStudies/GetCaseStudiesById?id={sectionMasterId}").Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseData = response.Content.ReadAsStringAsync().Result;
                    var res = JsonConvert.DeserializeObject<ResponseVM>(responseData);
                    CaseStudiesDetail caseStudies = JsonConvert.DeserializeObject<CaseStudiesDetail>(JsonConvert.SerializeObject(res.Data));

                    return View("CaseStudiesDetail", caseStudies);
                }
                return View("CaseStudiesDetail");
            }
        }
        [HttpPost]
        public async Task<ActionResult> PostCaseStudiesDetail(int SectionMasterId)
        {
            HttpContext.Session.SetString("SectionMasterId", SectionMasterId.ToString());
            return RedirectToAction("GetCaseStudiesDetail");
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

            return RedirectToAction("GetCaseStudiesDetail");


        }
    }
}
