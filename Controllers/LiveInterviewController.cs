using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using TechNewsUI.Models;
using TechNewsUI.Services;

namespace TechNewsUI.Controllers
{
    [UserSessionCheck]
    public class LiveInterviewController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:5000/");
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<ActionResult> GetAllLiveInterview()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "api/v3/LiveInterview/GetAllLiveInterview").Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseData = response.Content.ReadAsStringAsync().Result;
                    var res = JsonConvert.DeserializeObject<ResponseVM>(responseData);
                    List<GetLiveInterviewDto> getLiveInterviews = JsonConvert.DeserializeObject<List<GetLiveInterviewDto>>(JsonConvert.SerializeObject(res.Data));
                    return View(getLiveInterviews);
                }
                return RedirectToAction("HomePage", "GetBreakingNews", new { area = "" });
            }
        }

        //[HttpGet]
        //public async Task<ActionResult> GetLiveInteviewById(int? SectionMasterId)
        //{
        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = baseAddress;
        //        client.DefaultRequestHeaders.Clear();
        //        HttpResponseMessage response = client.GetAsync(client.BaseAddress + $"api/v3/LiveInterview/GetLiveInterviewById?id={SectionMasterId}").Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var responseData = response.Content.ReadAsStringAsync().Result;
        //            var res = JsonConvert.DeserializeObject<ResponseVM>(responseData);

        //            GetLiveInterviewByIdDto getLiveWebinarByIdDto = JsonConvert.DeserializeObject<GetLiveInterviewByIdDto>(JsonConvert.SerializeObject(res.Data));
        //            return View(getLiveWebinarByIdDto);
        //        }
        //        return RedirectToAction("LiveInterview", "GetAllLiveInterview", new { area = "" });
        //    }
        //}



        [HttpPost]
        public async Task<ActionResult> GetLiveInteviewById([FromForm]int? SectionMasterId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + $"api/v3/LiveInterview/GetLiveInterviewById?id={SectionMasterId}").Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseData = response.Content.ReadAsStringAsync().Result;
                    var res = JsonConvert.DeserializeObject<ResponseVM>(responseData);

                    GetLiveInterviewByIdDto getLiveWebinarByIdDto = JsonConvert.DeserializeObject<GetLiveInterviewByIdDto>(JsonConvert.SerializeObject(res.Data));
                    return View(getLiveWebinarByIdDto);
                }
                return RedirectToAction("LiveInterview", "GetAllLiveInterview", new { area = "" });
            }
        }
        [HttpGet]
        public async Task<ActionResult> GetInterviewDetail()
        {
            HttpContext.Session.GetString("SectionMasterId");
            int sectionMasterId = int.Parse(HttpContext.Session.GetString("SectionMasterId") ?? "0");
            if (sectionMasterId == 0)
            {
                return RedirectToAction("GetAllLiveInterview");
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + $"api/v3/LiveInterview/GetLiveInterviewById?id={sectionMasterId}").Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseData = response.Content.ReadAsStringAsync().Result;
                    var res = JsonConvert.DeserializeObject<ResponseVM>(responseData);

                    GetLiveInterviewByIdDto getLiveWebinarByIdDto = JsonConvert.DeserializeObject<GetLiveInterviewByIdDto>(JsonConvert.SerializeObject(res.Data));
                    return View("GetLiveInteviewById", getLiveWebinarByIdDto);
                }
                return RedirectToAction("GetLiveInteviewById");
            }
        }
        [HttpPost]
        public async Task<ActionResult> PostInterviewDetail(int SectionMasterId)
        {
            HttpContext.Session.SetString("SectionMasterId", SectionMasterId.ToString());
            return RedirectToAction("GetInterviewDetail");

        }



        public async Task<ActionResult> AddComment(string commentDescription, int id)
        {
            Comment comment = new Comment();
            comment.CommentDescription = commentDescription;
            comment.Email = HttpContext.Session.GetString("Email");
            comment.SectionMasterId = id;


            string data = JsonConvert.SerializeObject(comment);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpClient client = new HttpClient();
            client.BaseAddress = baseAddress;
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "api/v3/Comment/AddComment", content).Result;

            return RedirectToAction("GetInterviewDetail");


        }
    }
}
