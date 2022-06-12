using Microsoft.AspNetCore.Mvc;
using MVC_FE.UtilsData;
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
    public class RecipeController : Controller
    {
        private readonly Uri url = new("https://localhost:44395/api/Recipes");
        private readonly Uri url2 = new("https://localhost:44395/api/Categories");

        public async Task<ActionResult> Index(string searchString, string currentFilter, int? page)
        {
            using var client = new HttpClient();
            client.BaseAddress = url;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Add the Authorization header with the AccessToken. 
            client.DefaultRequestHeaders.Add("Authorization", "Bearer ");

            // make the request
            HttpResponseMessage response = await client.GetAsync("");

            // parse the response and return the data.
            string jsonString = await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<List<RecipeViewModel>>(jsonString);
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
            HttpResponseMessage response = await client.GetAsync("Recipes/" + id);

            // parse the response and return data
            string jsonString = await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<RecipeViewModel>(jsonString);
            return View(responseData);
        }

        // api/recipe/create
        public ActionResult Create()
        {
            ViewBag.Categories = LoadData.LoadCategoriesData();

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(RecipeViewModel recipeViewModel)
        {
            try
            {

                using (var client = new HttpClient())
                {

                    client.BaseAddress = url;
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var content = JsonConvert.SerializeObject(recipeViewModel);
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

        // api/recipe/edit/id
        public async Task<ActionResult> Edit(int id)
        {
            ViewBag.Categories = LoadData.LoadCategoriesData();
            using var client = new HttpClient();
            client.BaseAddress = url;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
           

            // make the request
            HttpResponseMessage response = await client.GetAsync("Recipes/" + id);

            // parse the response and return data
            string jsonString = await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<RecipeViewModel>(jsonString);
            return View(responseData);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(RecipeViewModel recipeViewModel)
        {
            try
            {

                using (var client = new HttpClient())
                {
                    client.BaseAddress = url;
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var content = JsonConvert.SerializeObject(recipeViewModel);
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

        // api/recipe/id
        public async Task<ActionResult> Delete(int id)
        {

            using var client = new HttpClient();
            client.BaseAddress = url;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // make the request
            HttpResponseMessage response = await client.DeleteAsync("Recipes/" + id);

            return RedirectToAction("Index");
        }
    }
}
