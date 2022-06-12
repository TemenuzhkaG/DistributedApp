using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_FE.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;

namespace MVC_FE.UtilsData
{
    public class LoadData
    {
        private static readonly Uri url = new("https://localhost:44395/api/Categories");

        public static SelectList LoadCategoriesData()
        {
            var client = new WebClient();
            string body = client.DownloadString(url);
            var responseData = JsonConvert.DeserializeObject<List<RecipeViewModel>>(body);
            return new SelectList(responseData, "Id", "Name");
        }

    }
}
