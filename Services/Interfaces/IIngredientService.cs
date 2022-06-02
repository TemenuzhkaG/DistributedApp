using Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IIngredientService
    {
        List<IngredientDTO> Get();
        IngredientDTO GetById(int id);
        bool Save(IngredientDTO ingredientDTO);
        bool Update(IngredientDTO ingredientDTO);
        bool Delete(int id);
    }
}
