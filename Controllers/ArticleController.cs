using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using TechNewsUI.Models;
using TechNewsUI.Services;

namespace TechNewsUI.Controllers
{
    [UserSessionCheck]
    public class ArticleController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:5000/");
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> GetAllArticle()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "api/v3/Article/GetAllArticle").Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseData = response.Content.ReadAsStringAsync().Result;
                    var res = JsonConvert.DeserializeObject<ResponseVM>(responseData);

                    List<Article> articles = JsonConvert.DeserializeObject<List<Article>>(JsonConvert.SerializeObject(res.Data));
                   
                    
                    return View(articles);
                }
                return View();
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetAllArticleFilter()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "api/v3/Article/GetAllArticle").Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseData = response.Content.ReadAsStringAsync().Result;
                    var res = JsonConvert.DeserializeObject<ResponseVM>(responseData);

                    List<Article> articles = JsonConvert.DeserializeObject<List<Article>>(JsonConvert.SerializeObject(res.Data));


                    return View(articles);
                }
                return View(response);
            }
        }
        [HttpPost]
        public JsonResult GetAllArticleCategories()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = baseAddress;
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "api/v3/Article/GetAllArticleCategory").Result;
            string data = response.Content.ReadAsStringAsync().Result;
            List<ArticleCategory> articleCategories = JsonConvert.DeserializeObject<List<ArticleCategory>>(data);
            return Json(articleCategories);
        }
        [HttpPost]
        public JsonResult GetAllArticleSubCategories(int articleCategoryId)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = baseAddress;
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "api/v3/Article/GetAllArticleSubCategory?ArticleCategoryID=" + articleCategoryId.ToString()).Result;
            string data = response.Content.ReadAsStringAsync().Result;
            List<ArticleSubCategory> articleSubCategories = JsonConvert.DeserializeObject<List<ArticleSubCategory>>(data);
            return Json(articleSubCategories);
        }
        //[HttpGet]
        //public async Task<ActionResult> ArticleDetail(int SectionMasterId)
        //{
        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = baseAddress;
        //        client.DefaultRequestHeaders.Clear();
        //        HttpResponseMessage response = client.GetAsync(client.BaseAddress + $"api/v3/Article/GetArticleById?id={SectionMasterId}").Result;

        //        if (response.IsSuccessStatusCode)
        //        {
        //            var responseData = response.Content.ReadAsStringAsync().Result;
        //            var res = JsonConvert.DeserializeObject<ResponseVM>(responseData);
        //            ArticleDetail articles = JsonConvert.DeserializeObject<ArticleDetail>(JsonConvert.SerializeObject(res.Data));

        //            return View(articles);
        //        }
        //        return View();
        //    }
        //}


        [HttpPost]
        public async Task<ActionResult> ArticleDetail([FromForm]int SectionMasterId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + $"api/v3/Article/GetArticleById?id={SectionMasterId}").Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseData = response.Content.ReadAsStringAsync().Result;
                    var res = JsonConvert.DeserializeObject<ResponseVM>(responseData);
                    ArticleDetail articles = JsonConvert.DeserializeObject<ArticleDetail>(JsonConvert.SerializeObject(res.Data));

                    return View("ArticleDetail", articles);
                }
                return View("ArticleDetail");
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetArticleDetail()
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
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + $"api/v3/Article/GetArticleById?id={sectionMasterId}").Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseData = response.Content.ReadAsStringAsync().Result;
                    var res = JsonConvert.DeserializeObject<ResponseVM>(responseData);
                    ArticleDetail articles = JsonConvert.DeserializeObject<ArticleDetail>(JsonConvert.SerializeObject(res.Data));

                    return View("ArticleDetail", articles);
                }
                return View("ArticleDetail");
            }

        }
        [HttpPost]
        public async Task<ActionResult> PostArticleDetail(int SectionMasterId)
        {
            HttpContext.Session.SetString("SectionMasterId", SectionMasterId.ToString());
            return RedirectToAction("GetArticleDetail");
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

             return RedirectToAction("GetArticleDetail");


        }






    }
}
