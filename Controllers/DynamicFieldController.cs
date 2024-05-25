using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using TechNewsUI.Models;

namespace TechNewsUI.Controllers
{
    public class DynamicFieldController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:5000/");

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> DynamicFieldForm()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> DynamicFieldForm(DynamicField dynamicField)
        {

            if (ModelState.IsValid)
            {
                string data = JsonConvert.SerializeObject(dynamicField);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");


                HttpClient client = new HttpClient();

                client.BaseAddress = baseAddress;
                HttpResponseMessage response = client.PostAsync(client.BaseAddress + "api/DynamicField", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    string responseData = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine(responseData);

                    TempData["AddDynamic"] = "New Dynamically Field is Added";

                    return RedirectToAction("DynamicFieldForm");
                }
                return View("DynamicFieldForm", dynamicField);
            }
            return View("DynamicFieldForm", dynamicField);
        }

        [HttpGet]
        public async Task<ActionResult> GetAllDynamicField()
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "api/v3/DynamicField/GetAllDynamicField").Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseData = response.Content.ReadAsStringAsync().Result;
                    var res = JsonConvert.DeserializeObject<ResponseVM>(responseData);

                    List<DynamicField> dynamicFields = JsonConvert.DeserializeObject<List<DynamicField>>(JsonConvert.SerializeObject(res.Data));

                    ViewBag.Section = "dynamic";
                    return View("GetAllDynamicField", dynamicFields);
                }
                return View("GetAllDynamicField");

            }
        }
        [HttpGet]
        public JsonResult DeleteDynamicField(int id)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + "api/v3/DynamicField/DeleteDynamicField?DynamicFieldId="+id).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseData = response.Content.ReadAsStringAsync().Result;
                    var res = JsonConvert.DeserializeObject(responseData);


                    TempData["DeleteDynamic"] = "Dynamically Field is Deleted";


                    return Json(res);
                }
                return Json(false);

            }
        }
        [HttpGet]
        public async Task<ActionResult> UpdateDynamicField(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + $"api/v3/DynamicField/GetDynamicFieldById?DynamicFieldId={id}").Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseData = response.Content.ReadAsStringAsync().Result;
                    var res = JsonConvert.DeserializeObject<ResponseVM>(responseData);
                    DynamicField dynamicField = JsonConvert.DeserializeObject<DynamicField>(JsonConvert.SerializeObject(res.Data));

                    return View(dynamicField);
                }
                return View();
            }
        }

        [HttpPost]
        public async Task<ActionResult> UpdateDynamicField(DynamicField dynamicField)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    string data1 = JsonConvert.SerializeObject(dynamicField);
                    StringContent content = new StringContent(data1, Encoding.UTF8, "application/json");

                    client.BaseAddress = baseAddress;
                    client.DefaultRequestHeaders.Clear();
                    HttpResponseMessage response = client.PutAsync(client.BaseAddress + "api/v3/DynamicField/UpdateDynamicField", content).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var responseData = response.Content.ReadAsStringAsync().Result;
                        var res = JsonConvert.DeserializeObject(responseData);


                        TempData["UpdateDynamic"] = "Field is Updated";


                        return RedirectToAction("GetAllDynamicField");
                    }
                    return RedirectToAction("GetAllDynamicField");

                }

            }
            else
            {
                return View("UpdateDynamicField", dynamicField);
            }

           
        }
    }
}
