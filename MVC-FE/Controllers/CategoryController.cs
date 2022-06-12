using Microsoft.AspNetCore.Mvc;
using MVC_FE.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using X.PagedList;

namespace MVC_FE.Controllers
{
    public class CategoryController : Controller
    {
        private readonly Uri url = new("https://localhost:44395/api/Categories");
        public async Task<ActionResult> Index(string searchString, string currentFilter, int? page)
        {

            using var client = new HttpClient();
            client.BaseAddress = url;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // make the request
            HttpResponseMessage response = await client.GetAsync("");

            // parse the response and return the data.
            string jsonString = await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<List<CategoryViewModel>>(jsonString);
            if (searchString != null)
                page = 1;

            else
                searchString = currentFilter;


            ViewBag.CurrentFilter = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                responseData = responseData.Where(c => c.Name.Contains(searchString)).ToList();
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            responseData = responseData.ToList();

            return View(responseData.ToPagedList(pageNumber, pageSize));
        }
        public async Task<ActionResult> Details(int id)
        {

            using var client = new HttpClient();
            client.BaseAddress = url;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // make the request
            HttpResponseMessage response = await client.GetAsync("Categories/" + id);

            // parse the response and return data
            string jsonString = await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<CategoryViewModel>(jsonString);
            return View(responseData);
        }

        // api/Categories/create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(CategoryViewModel categoryViewModel)
        {
            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = url;
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var content = JsonConvert.SerializeObject(categoryViewModel);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    // make the request
                    HttpResponseMessage response = await client.PostAsync("", byteContent);

                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // api/Categories/edit/id
        public async Task<ActionResult> Edit(int id)
        {

            using var client = new HttpClient();
            client.BaseAddress = url;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // make the request
            HttpResponseMessage response = await client.GetAsync("Categories/" + id);

            // parse the response and return data
            string jsonString = await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<CategoryViewModel>(jsonString);
            return View(responseData);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(CategoryViewModel categoryVM)
        {
            try
            {

                using (var client = new HttpClient())
                {
                    client.BaseAddress = url;
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var content = JsonConvert.SerializeObject(categoryVM);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    // make the request // Save or Update?
                    HttpResponseMessage response = await client.PostAsync("", byteContent);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // api/Categories/id
        public async Task<ActionResult> Delete(int id)
        {
            // string accessToken = await GetAccessToken();

            using var client = new HttpClient();
            client.BaseAddress = url;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // make the request
            HttpResponseMessage response = await client.DeleteAsync("Categories/" + id);

            // for testing purposes
            // string jsonString = await response.Content.ReadAsStringAsync();
            // var responseData = JsonConvert.DeserializeObject<NationalityViewModel>(jsonString);
            // return View(responseData);
            return RedirectToAction("Index");
        }
    }
}
