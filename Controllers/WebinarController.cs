using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol.Plugins;
using System.Text;
using TechNewsUI.Models;

namespace TechNewsUI.Controllers
{
    public class WebinarController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:5000/");

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> GetAllWebinar()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "api/v3/Webinar/GetAllWebinar").Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseData = response.Content.ReadAsStringAsync().Result;
                    var res = JsonConvert.DeserializeObject<ResponseVM>(responseData);

                    List<WebinarDto> caseStudies = JsonConvert.DeserializeObject<List<WebinarDto>>(JsonConvert.SerializeObject(res.Data));


                    return View(caseStudies);
                }
                return View();
            }
        }
        

        [HttpPost]
        public async Task<ActionResult> WebinarDetail([FromForm]int SectionMasterId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;
                   client.DefaultRequestHeaders.Clear();
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + $"api/v3/Webinar/GetWebinarById?id={SectionMasterId}").Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseData = response.Content.ReadAsStringAsync().Result;
                    var res = JsonConvert.DeserializeObject<ResponseVM>(responseData);
                    WebinarDetail webinarDtoDetail = JsonConvert.DeserializeObject<WebinarDetail>(JsonConvert.SerializeObject(res.Data));
                    if (webinarDtoDetail.StartDate > DateTime.Now || webinarDtoDetail.EndDate < DateTime.Now)
                    {
                        webinarDtoDetail.YoutubeVideoLink = null;
                    }
                    response = client.GetAsync(client.BaseAddress + $"api/v3/Webinar/GetAllEnrolledUser?WebinarId={webinarDtoDetail.WebinarId}").Result;
                    string data = response.Content.ReadAsStringAsync().Result;
                    List<Enrollment> enrolledUsers = JsonConvert.DeserializeObject<List<Enrollment>>(data);
                    ViewBag.EnrolledUsers = enrolledUsers;

                    if (HttpContext.Session.GetString("Token") != null)
                    {
                        int userId = int.Parse(HttpContext.Session.GetString("UserId"));
                        response = client.GetAsync(client.BaseAddress + $"api/v3/Webinar/EnrollmentExist?userId={userId}&webinarId={webinarDtoDetail.WebinarId}").Result;
                        responseData = response.Content.ReadAsStringAsync().Result;
                        bool enrolled = JsonConvert.DeserializeObject<bool>(responseData);
                        if (!enrolled)
                        {
                            webinarDtoDetail.YoutubeVideoLink = null;
                        }
                        ViewBag.IsEnrolled = enrolled;
                        ViewBag.IsGuest = false;
                    }
                    else
                    {
                        webinarDtoDetail.YoutubeVideoLink = null;
                        ViewBag.IsEnrolled = false;
                        ViewBag.IsGuest = true;
                        return View(webinarDtoDetail);
                    }
                    return View(webinarDtoDetail);
                }
                return View();
            }

        }
        [HttpGet]
        public async Task<ActionResult> GetWebinarDetail()
        {
            HttpContext.Session.GetString("SectionMasterId");
            int sectionMasterId = int.Parse(HttpContext.Session.GetString("SectionMasterId") ?? "0");
            if (sectionMasterId == 0)
            {
                return RedirectToAction("GetAllWebinar");
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + $"api/v3/Webinar/GetWebinarById?id={sectionMasterId}").Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseData = response.Content.ReadAsStringAsync().Result;
                    var res = JsonConvert.DeserializeObject<ResponseVM>(responseData);
                    WebinarDetail webinarDtoDetail = JsonConvert.DeserializeObject<WebinarDetail>(JsonConvert.SerializeObject(res.Data));
                    if (webinarDtoDetail.StartDate > DateTime.Now || webinarDtoDetail.EndDate < DateTime.Now)
                    {
                        webinarDtoDetail.YoutubeVideoLink = null;
                    }
                    response = client.GetAsync(client.BaseAddress + $"api/v3/Webinar/GetAllEnrolledUser?WebinarId={webinarDtoDetail.WebinarId}").Result;
                    string data = response.Content.ReadAsStringAsync().Result;
                    List<Enrollment> enrolledUsers = JsonConvert.DeserializeObject<List<Enrollment>>(data);
                    ViewBag.EnrolledUsers = enrolledUsers;

                    if (HttpContext.Session.GetString("Token") != null)
                    {
                        int userId = int.Parse(HttpContext.Session.GetString("UserId"));
                        response = client.GetAsync(client.BaseAddress + $"api/v3/Webinar/EnrollmentExist?userId={userId}&webinarId={webinarDtoDetail.WebinarId}").Result;
                        responseData = response.Content.ReadAsStringAsync().Result;
                        bool enrolled = JsonConvert.DeserializeObject<bool>(responseData);
                        if (!enrolled)
                        {
                            webinarDtoDetail.YoutubeVideoLink = null;
                        }
                        ViewBag.IsEnrolled = enrolled;
                        ViewBag.IsGuest = false;
                        return View("WebinarDetail", webinarDtoDetail);

                    }
                    else
                    {
                        webinarDtoDetail.YoutubeVideoLink = null;
                        ViewBag.IsEnrolled = false;
                        ViewBag.IsGuest = true;
                        return View("WebinarDetail", webinarDtoDetail);
                    }
                    return View("WebinarDetail");
                }
                return View();
            }
        }
        [HttpPost]
        public async Task<ActionResult> PostWebinarDetail(int SectionMasterId)
        {
            HttpContext.Session.SetString("SectionMasterId", SectionMasterId.ToString());
            return RedirectToAction("GetWebinarDetail");
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

            return RedirectToAction("GetWebinarDetail");


        }
        [HttpPost]
        public async Task<IActionResult> AddEnrollment(Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                string data = JsonConvert.SerializeObject(enrollment);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");


                HttpClient client = new HttpClient();

                client.BaseAddress = baseAddress;
                HttpResponseMessage response = client.PostAsync(client.BaseAddress + "api/v3/Webinar/AddEnrollment", content).Result;
            }
            return RedirectToAction("GetWebinarDetail");

        }

        [HttpPost]
        public JsonResult Enroll(Enrollment enrollment)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = baseAddress;
            string data = JsonConvert.SerializeObject(enrollment);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "api/v3/Webinar/AddEnrollment", content).Result;
            data = response.Content.ReadAsStringAsync().Result;
            ResponseVM enrolled = JsonConvert.DeserializeObject<ResponseVM>(data);
            Console.WriteLine(enrolled.Message);
            TempData["EnrollDone"] = $"Enrollment has been done for this webinar";
            return Json(enrolled.Data);
        }
                    
    }
}
