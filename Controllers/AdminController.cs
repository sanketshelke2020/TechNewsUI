using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using TechNewsUI.Models;
using TechNewsUI.Services;
using static System.Collections.Specialized.BitVector32;

namespace TechNewsUI.Controllers
{
    [AdminSessionCheck]
    public class AdminController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:5000/");
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateSectionMasterCommand sectionMaster, IFormFile? StoredFile)
        {
            //JObject json = JObject.Parse(sectionMaster.DynamicData);
            //string terminal = json["string"];
            //sectionMaster.KeyWords = sectionMaster.KeyWords.Replace(",", " ");

            //if (sectionMaster.CategoryType == "Article")
            //{
            //    var radioValue = sectionMaster.flexRadioDefault;
            //    if (radioValue == "link")
            //    { 
            //        sectionMaster.StoredFile = null;
            //    }
            //    else
            //    {

            //        sectionMaster.YoutubeVideoLink = null;
            //    }
            //}

            //if (!ModelState.IsValid)
            //{
            //    return View();
            //}

            //var file = Request.Form.Files[0];
            //sectionMaster.FileName = file.FileName;
            //using (var ms = new MemoryStream())
            //{
            //    file.CopyTo(ms);
            //    sectionMaster.Image = ms.ToArray();
            //}
            //if (StoredFile != null)
            //{
            //    sectionMaster.StoredFileName = StoredFile.FileName;
            //    using (var ms = new MemoryStream())
            //    {
            //        StoredFile.CopyTo(ms);
            //        sectionMaster.File = ms.ToArray();
            //    }
            //}
            //using (var client = new HttpClient())
            //{
            //    var accessToken = HttpContext.Session.GetString("Token");
            //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            //    client.BaseAddress = new Uri("https://localhost:5000/api/v3/");
            //    var postTask = client.PostAsJsonAsync<CreateSectionMasterCommand>("Admin", sectionMaster);
            //    try
            //    {
            //        postTask.Wait();
            //    }
            //    catch (Exception)
            //    {
            //        TempData["Error"] = "Something went Wrong";
            //        return View();
            //    }
            //    var result = postTask.Result;
            //    if (result == null)
            //    {
            //        TempData["Error"] = "Something went Wrong";
            //        return View();
            //    }
            //    if (result.IsSuccessStatusCode)
            //    {
            //        TempData["MySuccess"] = "Record Added Successfully!";
            //        ModelState.Clear();
            //        return View();
            //    }

            //}
            
            string data1 = JsonConvert.SerializeObject(sectionMaster);
            StringContent content = new StringContent(data1, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            var accessToken = HttpContext.Session.GetString("Token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            client.BaseAddress = new Uri("https://localhost:5000/api/v3/");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "Admin/AddSectionmaster", content).Result;


            if (response.IsSuccessStatusCode)
            {
                TempData["MySuccess"] = "Record Added Successfully!";
                ModelState.Clear();
                return View();
            }
            else
            {

                TempData["Error"] = "Something went Wrong";
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

        [HttpGet]
        public async Task<IActionResult> GetSectionMasterList(string section)
        {
            using (
                var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "api/v3/Admin/GetSectionMasterByCategory?category=" + section).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseData = response.Content.ReadAsStringAsync().Result;
                    var res = JsonConvert.DeserializeObject<ResponseVM>(responseData);
                    List<SectionMasterView> sections = JsonConvert.DeserializeObject<List<SectionMasterView>>(JsonConvert.SerializeObject(res.Data));
                    string[] split = Regex.Split(section, @"(?<!^)(?=[A-Z])");
                    var sectionJoin = String.Join(" ", split);
                    var youtubeFilter = sectionJoin == "Youtube" ? sectionJoin + " Video" : sectionJoin;
                    ViewData["Section"] = youtubeFilter.EndsWith('s') ? youtubeFilter : youtubeFilter + "s";
                    ViewBag.Section = section;
                    return View(sections);
                }
                return RedirectToAction("Login", "Login");
            }
            return View();
        }
        [HttpGet]
        public JsonResult DeleteSectionMaster(int sectionMasterId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + "api/v3/Admin/DeleteSectionMaster?sectionMasterId=" + sectionMasterId).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseData = response.Content.ReadAsStringAsync().Result;
                    var res = JsonConvert.DeserializeObject<ResponseVM>(responseData);
                    if (res.Data)
                    {
                        //work
                        TempData["DeleteSuccess"] = "Item Removed Successfully!";
                        return Json(res.Data);
                    }
                }
                return Json(false);
            }
            
        }

        [HttpGet]
        public async Task<IActionResult> GetSectionMasterById(int sectionMasterId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "api/v3/Admin/GetSectionMasterById?sectionMasterId=" + sectionMasterId).Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseData = response.Content.ReadAsStringAsync().Result;
                    var res = JsonConvert.DeserializeObject<ResponseVM>(responseData);
                    UpdateSectionMasterCommandDto sections = JsonConvert.DeserializeObject<UpdateSectionMasterCommandDto>(JsonConvert.SerializeObject(res.Data));

                    ViewBag.CountryId = sections.CountryId != null ? sections.CountryId : 0;
                    ViewBag.ArticleCategory = sections.ArticleCategoryId != null ? sections.ArticleCategoryId : 0;
                    ViewBag.ArticleSubCategory = sections.ArticleSubCategoryId != null ? sections.ArticleSubCategoryId : 0;
                    return View(sections);
                }
                return RedirectToAction("Login", "Login");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GetSectionMasterById(UpdateSectionMasterCommandDto sectionMaster, IFormFile? StoredFile, IFormFile? FormImage, string? flexRadioDefault)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CountryId = sectionMaster.CountryId != null ? sectionMaster.CountryId : 0;
                ViewBag.ArticleCategory = sectionMaster.ArticleCategoryId != null ? sectionMaster.ArticleCategoryId : 0;
                ViewBag.ArticleSubCategory = sectionMaster.ArticleSubCategoryId != null ? sectionMaster.ArticleSubCategoryId : 0;
                return View();
            }
            if (sectionMaster.CategoryType == "Article")
            {
                var radioValue = flexRadioDefault;
                if (radioValue == "link")
                {
                    sectionMaster.StoredFileName = null;
                }
                else
                {
                    sectionMaster.YoutubeVideoLink = null;
                }
            }
            var file = FormImage;
            if (file != null)
            {
                sectionMaster.FileName = file.FileName;
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    sectionMaster.Image = ms.ToArray();
                }
            }
            if (StoredFile != null)
            {
                sectionMaster.StoredFileName = StoredFile.FileName;
                using (var ms = new MemoryStream())
                {
                    StoredFile.CopyTo(ms);
                    sectionMaster.File = ms.ToArray();
                }
            }
            using (var client = new HttpClient())
            {
                var accessToken = HttpContext.Session.GetString("Token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                client.BaseAddress = new Uri("https://localhost:5000/api/v3/");
                var postTask = client.PostAsJsonAsync<UpdateSectionMasterCommandDto>("Admin/UpdateSectionMaster", sectionMaster);
                try
                {
                    postTask.Wait();
                }
                catch (Exception)
                {
                    TempData["Error"] = "Something went Wrong";
                    return View();
                }
                var result = postTask.Result;
                if (result == null)
                {
                    TempData["Error"] = "Something went Wrong";
                    return View();
                }
                if (result.IsSuccessStatusCode)
                {
                    TempData["UpdateSuccess"] = "Record Updated Successfully!";
                    ModelState.Clear();
                    return RedirectToAction("GetSectionMasterById", "Admin", new { sectionMasterId = sectionMaster.SectionMasterId });
                }
            }

            return View();
        }
        public async Task<ActionResult> GetChapterByPodcastId(int id)
        {
            int i = 0;
            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage response1 = client.GetAsync(client.BaseAddress + "api/v3/Admin/GetSectionMasterById?sectionMasterId=" + id).Result;
                if (response1.IsSuccessStatusCode)
                {
                    var responseData = response1.Content.ReadAsStringAsync().Result;
                    var res = JsonConvert.DeserializeObject<ResponseVM>(responseData);
                    PodcastIdModel Podcast = JsonConvert.DeserializeObject<PodcastIdModel>(JsonConvert.SerializeObject(res.Data));
                    i = Podcast.PodcastId;
                }
                HttpResponseMessage response2 = client.GetAsync(client.BaseAddress + "api/v3/Chapter/GetChapterByPodcastId?id=" + i).Result;

                if (response2.IsSuccessStatusCode)
                {
                    var responseData = response2.Content.ReadAsStringAsync().Result;
                    var res = JsonConvert.DeserializeObject<ResponseVM>(responseData);

                    List<ChaptersDto> podcasts = JsonConvert.DeserializeObject<List<ChaptersDto>>(JsonConvert.SerializeObject(res.Data));
                    ViewBag.podcastId = i;
                    ViewBag.sectionmasterId = id;
                    return View(podcasts);


                }
                return RedirectToAction("HomePage", "GetBreakingNews", new { area = "" });
            }
        }
        [HttpPost]
        public ActionResult AddChapter(ChaptersDto data, int id)
        {

            var file = data.ChapterFile;
            data.FileName = file.FileName;
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                data.Audio = ms.ToArray();
            }
            using (var client = new HttpClient())
            {
                string data1 = JsonConvert.SerializeObject(data);
                StringContent content = new StringContent(data1, Encoding.UTF8, "application/json");


                client.BaseAddress = baseAddress;
                HttpResponseMessage response = client.PostAsync(client.BaseAddress + "api/v3/Chapter/AddChapter", content).Result;

            }
            return RedirectToAction("GetChapterByPodcastId", "Admin", new { id = id });

        }
        [HttpGet]
        public JsonResult GetChapterById(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "api/v3/Podcast/GetChapterById?id=" + id).Result;


                var responseData = response.Content.ReadAsStringAsync().Result;
                var res = JsonConvert.DeserializeObject<ResponseVM>(responseData);

                ChaptersDto chapter = JsonConvert.DeserializeObject<ChaptersDto>(JsonConvert.SerializeObject(res.Data));


                return Json(chapter);

            }

        }
        [HttpGet]


        public JsonResult DeleteChapter(int id)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "api/v3/Chapter/DeleteChapter?id=" + id).Result;
                return Json(true);

            }


        }
        [HttpPost]
        public ActionResult UpdateChapter(ChaptersDto data, int id, Byte[] Audiobyte)
        {
            if (data.ChapterFile != null)
            {
                var file = data.ChapterFile;
                data.FileName = file.FileName;
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    data.Audio = ms.ToArray();
                }
            }
            else
            {
                data.Audio = Audiobyte;
            } 

            using (var client = new HttpClient())
            {
                string data1 = JsonConvert.SerializeObject(data);
                StringContent content = new StringContent(data1, Encoding.UTF8, "application/json");


                client.BaseAddress = baseAddress;
                HttpResponseMessage response = client.PostAsync(client.BaseAddress + "api/v3/Chapter/UpdateChapter", content).Result;

            }
            return RedirectToAction("GetChapterByPodcastId", "Admin", new { id = id });

        }
        [HttpGet]
        public JsonResult ChapterNumberExist(int number, int podcastId)
        {
            if (number != null)
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = baseAddress;
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + $"api/v3/Chapter/ChapterNumberExist?no={number}&podcastId={podcastId}").Result;
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
        public async Task<ActionResult> GetAllUserQueries()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "api/v3/Admin/GetAllUserQuery").Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseData = response.Content.ReadAsStringAsync().Result;
                    var res = JsonConvert.DeserializeObject<ResponseVM>(responseData);
                    List<UserQuery> userQueries = JsonConvert.DeserializeObject<List<UserQuery>>(JsonConvert.SerializeObject(res.Data));
                    ViewBag.Section = "UserQuery";
                    return View(userQueries);
                }
                return RedirectToAction("GetSectionMasterList", "Admin", new { section = "Articles" });



            }
        }
        [HttpGet]
        public ActionResult GetUserQueryById(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "api/v3/Admin/GetUserQueryById?id=" + id).Result;


                var responseData = response.Content.ReadAsStringAsync().Result;
                var res = JsonConvert.DeserializeObject<ResponseVM>(responseData);

                UserQuery userQueries = JsonConvert.DeserializeObject<UserQuery>(JsonConvert.SerializeObject(res.Data));


                return View(userQueries);

            }

        }
        [HttpPost]
        public JsonResult GetDynamicFieldByCategory(string category)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = baseAddress;
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + $"api/v3/Admin/GetDynamicFieldByCategory?category={category}").Result;
            var responseData = response.Content.ReadAsStringAsync().Result;

            List<DynamicFields> dynamicFields = JsonConvert.DeserializeObject<List<DynamicFields>>(responseData);
            ViewBag.Category = dynamicFields;
            return Json(dynamicFields);
        }

       

    }
}
