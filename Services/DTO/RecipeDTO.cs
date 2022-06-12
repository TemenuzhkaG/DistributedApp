using Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Services.DTO
{
  public  class RecipeDTO
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(500)]

        public string Instructions { get; set; }

        [Range(0, 1000)]
        public int PreparationTime { get; set; }

        [Range(0, 1000)]
        public int CookingTime { get; set; }

        [Range(1, 100)]
        public int PortionsCount { get; set; }
        public int CategoryId { get; set; }

      //  public virtual Category Category { get; set; }

      //  public List<IngredientDTO> Ingredients { get; set; }
     //   public List<CategoryDTO> Categories { get; set; }
        public bool Validate()
        {
            if (!String.IsNullOrEmpty(Name))
            {
                return true;
            }
            return false;

        }
    }

}
