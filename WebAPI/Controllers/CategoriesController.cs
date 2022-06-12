using Microsoft.AspNetCore.Mvc;
using Services;
using Services.DTO;
using System.Collections.Generic;
using WebAPI.ResponseHelper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {

        private readonly CategoryService service;
        private readonly Message message;

        public CategoriesController()
        {
            this.service = new CategoryService();
            this.message = new Message();
        }

        // GET: api/<CategoriesController>
        [HttpGet]
        public IEnumerable<CategoryDTO> Get()
        {
            return service.Get();
        }

        // GET api/<CategoriesController>/5
        [HttpGet("{id}")]
        public CategoryDTO Get(int id)
        {
            return service.GetById(id);
        }

        // POST api/<CategoriesController>
        [HttpPost]
        public string Post(CategoryDTO categoryDTO)
        {
            if (service.Save(categoryDTO))
            {
                message.Code = 200;
                message.Body = "Category was saved.";
            }
            else
            {
                message.Code = 500;
                message.Body = "Category was not saved.";
            }

            return $"{message.Code} {message.Body}";
        }

        // PUT api/<CategoriesController>/5
        [HttpPut("{id}")]
        public string Put([FromBody] CategoryDTO categoryDTO)
        {
            if (!categoryDTO.Validate())
                return "500 Data is not valid!";


            if (service.Update(categoryDTO))
            {
                message.Code = 200;
                message.Body = "Category was updated.";
            }
            else
            {
                message.Code = 500;
                message.Body = "Category was not updated.";
            }

            return $"{message.Code} {message.Body}";
        }

        // DELETE api/<CategoriesController>/5
        [HttpDelete("{id}")]
        public string Delete(int id)
        {

            if (service.Delete(id))
            {
                message.Code = 200;
                message.Body = "Category was deleted.";
            }
            else
            {
                message.Code = 500;
                message.Body = "Category was not deleted.";
            }

            return $"{message.Code} {message.Body}";
        }
    }
}
