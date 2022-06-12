using Services.DTO;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace MVC_FE.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [DisplayName("Description")]
        [StringLength(500, MinimumLength = 5)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public CategoryViewModel() { }

        public CategoryViewModel(RecipeDTO recipeDTO)
        {
            Id = recipeDTO.Id;
            Name = recipeDTO.Name;
        }
    }
}
