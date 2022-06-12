using Services.DTO;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVC_FE.ViewModels
{
    public class IngredientViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [DisplayName("Description")]
        [StringLength(500, MinimumLength = 5)]
        [DataType(DataType.MultilineText)]
        [Required]
        public string Description { get; set; }

        public IngredientViewModel() { }

        public IngredientViewModel(RecipeDTO recipeDTO)
        {
            Id = recipeDTO.Id;
            Name = recipeDTO.Name;
        }


  
    }
}
