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
    public class IngredientsController : ControllerBase
    {
        private readonly IngredientService service;
        private readonly Message message;
        public IngredientsController()
        {
            this.service = new IngredientService();
            this.message = new Message();
        }
        // GET: api/<IngredientsController>
        [HttpGet]
        public IEnumerable<IngredientDTO> Get()
        {
            return service.Get();
        }

        // GET api/<IngredientsController>/5
        [HttpGet("{id}")]
        public IngredientDTO Get(int id)
        {
            return service.GetById(id);
        }

        // POST api/<IngredientsController>
        [HttpPost]
        public string Post(IngredientDTO ingredientDTO)
        {
            if (service.Save(ingredientDTO))
            {
                message.Code = 200;
                message.Body = "Ingredient was saved.";
            }
            else
            {
                message.Code = 500;
                message.Body = "Ingredient was not saved.";
            }

            return $"{message.Code} {message.Body}";
        }

        // PUT api/<IngredientsController>/5
        [HttpPut("{id}")]
        public string Put(int id, [FromBody] IngredientDTO ingredientDTO)
        {
            if (!ingredientDTO.Validate())
                return "500 Data is not valid!";


            if (service.Update(ingredientDTO))
            {
                message.Code = 200;
                message.Body = "Ingredient was updated.";
            }
            else
            {
                message.Code = 500;
                message.Body = "Ingredient was not updated.";
            }

            return $"{message.Code} {message.Body}";
        }

        // DELETE api/<IngredientsController>/5
        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            if (service.Delete(id))
            {
                message.Code = 200;
                message.Body = "Ingredient was deleted.";
            }
            else
            {
                message.Code = 500;
                message.Body = "Ingredient was not deleted.";
            }

            return $"{message.Code} {message.Body}";
        }
    }
}
