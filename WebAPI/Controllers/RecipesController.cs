using Microsoft.AspNetCore.Mvc;
using Services;
using Services.DTO;
using System;
using System.Collections.Generic;
using WebAPI.ResponseHelper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly RecipeService service;
        private readonly Message message;
        public RecipesController()
        {
            this.service = new RecipeService();
            this.message = new Message();
        }
        // GET: api/<RecipesController>
        [HttpGet]
        public IEnumerable<RecipeDTO> Get()
        {
            return service.Get();
        }

        // GET api/<RecipesController>/5
        [HttpGet("{id}")]
        public RecipeDTO Get(int id)
        {
            return service.GetById(id);
        }

        // POST api/<RecipesController>
        [HttpPost]
        public string Post( RecipeDTO recipeDTO)
        {
            if (service.Save(recipeDTO))
            {
                message.Code = 200;
                message.Body = "Recipe was saved.";
            }
            else
            {
                message.Code = 500;
                message.Body = "Recipe was not saved.";
            }

            return $"{message.Code} {message.Body}";
        }

        // PUT api/<RecipesController>/5
        [HttpPut("{id}")]
        public string Put(int id, [FromBody] RecipeDTO recipeDTO)
        {
            if (!recipeDTO.Validate())
                return "500 Data is not valid!";


            if (service.Update(recipeDTO))
            {
                message.Code = 200;
                message.Body = "Recipe was updated.";
            }
            else
            {
                message.Code = 500;
                message.Body = "Recipe was not updated.";
            }

            return $"{message.Code} {message.Body}";
        }

        // DELETE api/<RecipesController>/5
        [HttpDelete("{id}")]
        public string Delete(int id)
        {

            if (service.Delete(id))
            {
                message.Code = 200;
                message.Body = "Recipe was deleted.";
            }
            else
            {
                message.Code = 500;
                message.Body = "Recipe was not deleted.";
            }

            return $"{message.Code} {message.Body}";
        }
    }
}
