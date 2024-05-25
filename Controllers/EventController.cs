using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using TechNewsUI.Models;
using TechNewsUI.Services;

namespace TechNewsUI.Controllers
{
    [UserSessionCheck]
    public class EventController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:5000/");

        public ActionResult GetAllEvents()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "api/v3/Event/GetAllEventSchedule").Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseData = response.Content.ReadAsStringAsync().Result;
                    var res = JsonConvert.DeserializeObject<ResponseVM>(responseData);

                    List<EventSchedule> eventSchedule = JsonConvert.DeserializeObject<List<EventSchedule>>(JsonConvert.SerializeObject(res.Data));

                    return View(eventSchedule);
                }
                return View();
            }
        }

        //public async Task<ActionResult> EventDetailsPage()
        //{
        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = baseAddress;
        //        client.DefaultRequestHeaders.Clear();
        //        HttpResponseMessage response = client.GetAsync(client.BaseAddress + "api/v3/Event/GetAllEventSchedule").Result;

        //        if (response.IsSuccessStatusCode)
        //        {
        //            var responseData = response.Content.ReadAsStringAsync().Result;
        //            var res = JsonConvert.DeserializeObject<Response>(responseData);

        //            List<EventScheduleDetails> eventScheduleDetail = JsonConvert.DeserializeObject<List<EventScheduleDetails>>(JsonConvert.SerializeObject(res.Data));

        //            return View(eventScheduleDetail);
        //        }
        //        return View();
        //    }
        //    //return View();
        //}

        //[Route("GetEventById/{id}")]
        //public ActionResult GetEventById(int id)
        //{
        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = baseAddress;
        //        client.DefaultRequestHeaders.Clear();
        //        HttpResponseMessage response = client.GetAsync(client.BaseAddress + $"api/v3/Event/GetAllEventScheduleById?id={id}").Result;

        //        if (response.IsSuccessStatusCode)
        //        {
        //            var responseData = response.Content.ReadAsStringAsync().Result;                    
        //            var res = JsonConvert.DeserializeObject<ResponseVM>(responseData);                   
        //            EventScheduleDetails eventScheduleDetail = JsonConvert.DeserializeObject<EventScheduleDetails>(JsonConvert.SerializeObject(res.Data));
        //            var videoData = res.Data.Video;
        //            //if(res.Data.EventDate > DateTime.Now)
        //            //{

        //            //    res.Data. = null;
        //            //}
        //            return View(eventScheduleDetail);
        //        }
        //        return View();
        //    }

        //}
       
        [HttpPost]
        public ActionResult GetEventById([FromForm] int sectionMasterId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + $"api/v3/Event/GetAllEventScheduleById?id={sectionMasterId}").Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseData = response.Content.ReadAsStringAsync().Result;
                    var res = JsonConvert.DeserializeObject<ResponseVM>(responseData);
                    EventScheduleDetails eventScheduleDetail = JsonConvert.DeserializeObject<EventScheduleDetails>(JsonConvert.SerializeObject(res.Data));
                    var videoData = res.Data.Video;
                    //if(res.Data.EventDate > DateTime.Now)
                    //{

                    //    res.Data. = null;
                    //}
                    return View(eventScheduleDetail);
                }
                return View();
            }

        }
        [HttpGet]
        public ActionResult GetEventDetail()
        {
            HttpContext.Session.GetString("SectionMasterId");
            int sectionMasterId = int.Parse(HttpContext.Session.GetString("SectionMasterId") ?? "0");
            if (sectionMasterId == 0)
            {
                return RedirectToAction("GetAllEvents");
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + $"api/v3/Event/GetAllEventScheduleById?id={sectionMasterId}").Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseData = response.Content.ReadAsStringAsync().Result;
                    var res = JsonConvert.DeserializeObject<ResponseVM>(responseData);
                    EventScheduleDetails eventScheduleDetail = JsonConvert.DeserializeObject<EventScheduleDetails>(JsonConvert.SerializeObject(res.Data));
                    var videoData = res.Data.Video;
                    
                    return View("GetEventById", eventScheduleDetail);
                }
                return View("GetEventById");
            }
        }
        [HttpPost]
        public ActionResult PostEventDetail(int SectionMasterId)
        {
            HttpContext.Session.SetString("SectionMasterId", SectionMasterId.ToString());
            return RedirectToAction("GetEventDetail");

        }

        public async Task<ActionResult> AddComment(string commentdis, int id)
        {
            Comment comment = new Comment();
            comment.CommentDescription = commentdis;
            //comment.Email = HttpContext.Session.GetString("Email");
            comment.Email = HttpContext.Session.GetString("Email");
            comment.SectionMasterId = id;


            string data = JsonConvert.SerializeObject(comment);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpClient client = new HttpClient();
            client.BaseAddress = baseAddress;
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "api/v3/Comment/AddComment", content).Result;

            return RedirectToAction("GetEventDetail");


        }
    }
}
