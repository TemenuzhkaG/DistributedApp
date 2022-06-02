using Services.DTO;
using System.Collections.Generic;

namespace Services.Interfaces
{
  public  interface IRecipeService
    {
        List<RecipeDTO> Get();
        RecipeDTO GetById(int id);
        bool Save(RecipeDTO recipeDTO);
        bool Update(RecipeDTO recipeDTO);
        bool Delete(int id);
    }
}
