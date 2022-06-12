using Data.Entities;
using Repository;
using Services.DTO;
using Services.Interfaces;
using System.Collections.Generic;

namespace Services
{
    public class IngredientService : IIngredientService
    {
        private readonly List<IngredientDTO> ingredientDTO;
        private IngredientDTO newIngredient;
        private Ingredient myIngredient;
        public IngredientService()
        {
            this.ingredientDTO = new List<IngredientDTO>();
            this.newIngredient = new IngredientDTO();
            this.myIngredient = new Ingredient();
        }

        public List<IngredientDTO> Get()
        {
            using (UnitOfWork unitOfWork = new())
            {
                foreach (var item in unitOfWork.IngredientRepository.Get())
                {

                    var ingredients = new IngredientDTO
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Description = item.Description
                    };

                    ingredientDTO.Add(ingredients);
                }
            }
            return ingredientDTO;
        }

        public IngredientDTO GetById(int id)
        {
            using (UnitOfWork unitOfWork = new())
            {
                Ingredient ingredient = unitOfWork.IngredientRepository.GetByID(id);
                if (ingredient != null)
                {
                    newIngredient = new IngredientDTO
                    {
                        Id = ingredient.Id,
                        Name = ingredient.Name,
                        Description = ingredient.Description
                    };
                }
            }

            return newIngredient;
        }

        public bool Save(IngredientDTO ingredientDTO)
        {
            myIngredient = new Ingredient()
            {
                Id = ingredientDTO.Id,
                Name = ingredientDTO.Name,
                Description = ingredientDTO.Description
            };

            try
            {
                using (UnitOfWork unitOfWork = new())
                {
                    if (ingredientDTO.Id == 0)
                    {
                        unitOfWork.IngredientRepository.Insert(myIngredient);
                    }
                    else
                    {
                        unitOfWork.IngredientRepository.Update(myIngredient);
                    }
                    unitOfWork.Save();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(IngredientDTO ingredientDTO)
        {
            try
            {
                using (UnitOfWork unitOfWork = new())
                {
                    myIngredient = unitOfWork.IngredientRepository.GetByID(ingredientDTO.Id);
                    myIngredient.Name = ingredientDTO.Name;

                    myIngredient.Description = ingredientDTO.Description;
                    unitOfWork.IngredientRepository.Update(myIngredient);
                    unitOfWork.Save();
                }

                return true;
            }
            catch
            {
                return false;
            }

        }

        public bool Delete(int id)
        {
            try
            {
                using (UnitOfWork unitOfWork = new())
                {
                    myIngredient = unitOfWork.IngredientRepository.GetByID(id);
                    unitOfWork.IngredientRepository.Delete(myIngredient);
                    unitOfWork.Save();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
