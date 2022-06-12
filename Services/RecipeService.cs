using Data.Entities;
using Repository;
using Services.DTO;
using Services.Interfaces;
using System;
using System.Collections.Generic;

namespace Services
{
    public class RecipeService : IRecipeService
    {

        private readonly List<RecipeDTO> recipeDTO;
        private RecipeDTO newRecipe;
        private Recipe myRecipe;
        public RecipeService()
        {
            this.recipeDTO = new List<RecipeDTO>();
            this.newRecipe = new RecipeDTO();
            this.myRecipe = new Recipe();
        }

        public List<RecipeDTO> Get()
        {
            using (UnitOfWork unitOfWork = new())
            {
                foreach (var item in unitOfWork.RecipeRepository.Get())
                {

                    var recipes = new RecipeDTO
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Instructions = item.Instructions,
                        PreparationTime = item.PreparationTime,
                        CookingTime = item.CookingTime,
                        PortionsCount = item.PortionsCount,
                        CategoryId = item.CategoryId
                    };

                    recipeDTO.Add(recipes);
                }
            }
            return recipeDTO;
        }

        public RecipeDTO GetById(int id)
        {

            using (UnitOfWork unitOfWork = new())
            {
                Recipe recipe = unitOfWork.RecipeRepository.GetByID(id);
                if (recipe != null)
                {
                    newRecipe = new RecipeDTO
                    {
                        Id = recipe.Id,
                        Name = recipe.Name,
                        Instructions = recipe.Instructions,
                        PreparationTime = recipe.PreparationTime,
                        CookingTime = recipe.CookingTime,
                        PortionsCount = recipe.PortionsCount,
                        CategoryId = recipe.CategoryId
                    };
                }
            }

            return newRecipe;
        }

        public bool Save(RecipeDTO recipeDTO)
        {
            myRecipe = new Recipe()
            {
                Id = recipeDTO.Id,
                Name = recipeDTO.Name,
                Instructions = recipeDTO.Instructions,
                PreparationTime = recipeDTO.PreparationTime,
                CookingTime = recipeDTO.CookingTime,
                PortionsCount = recipeDTO.PortionsCount,
                CreatedOn = DateTime.UtcNow.Date,
                CategoryId = recipeDTO.CategoryId
            };

            try
            {
                using (UnitOfWork unitOfWork = new())
                {
                    if (recipeDTO.Id == 0)
                    {
                        unitOfWork.RecipeRepository.Insert(myRecipe);
                    }
                    else
                    {
                        unitOfWork.RecipeRepository.Update(myRecipe);
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

        public bool Update(RecipeDTO recipeDTO)
        {
            try
            {
                using (UnitOfWork unitOfWork = new())
                {
                    myRecipe = unitOfWork.RecipeRepository.GetByID(recipeDTO.Id);
                    myRecipe.Name = recipeDTO.Name;

                    myRecipe.Instructions = recipeDTO.Instructions;
                    myRecipe.PreparationTime = recipeDTO.PreparationTime;
                    myRecipe.CookingTime = recipeDTO.CookingTime;
                    myRecipe.PortionsCount = recipeDTO.PortionsCount;
                    myRecipe.CategoryId = recipeDTO.CategoryId;

                    unitOfWork.RecipeRepository.Update(myRecipe);
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
                    myRecipe = unitOfWork.RecipeRepository.GetByID(id);
                    unitOfWork.RecipeRepository.Delete(myRecipe);
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
