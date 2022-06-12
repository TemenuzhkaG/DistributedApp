using Data.Context;
using Data.Entities;
using System;

namespace Repository
{
   public class UnitOfWork : IDisposable
    {
        private readonly RecipesDbContext context;

        private GenericRepository<Category> categoryRepository;

        private GenericRepository<Ingredient> ingredientRepository;

        private GenericRepository<Recipe> recipeRepository;

        public UnitOfWork()
        {
            this.context = new RecipesDbContext();
        }
        public GenericRepository<Category> CategoryRepository
        {
            get
            {

                if (this.categoryRepository == null)
                {
                    this.categoryRepository = new GenericRepository<Category>(context);
                }
                return categoryRepository;
            }
        }

        public GenericRepository<Ingredient> IngredientRepository
        {
            get
            {
                if (this.ingredientRepository == null)
                {
                    this.ingredientRepository = new GenericRepository<Ingredient>(context);
                }
                return ingredientRepository;
            }
        }

        public GenericRepository<Recipe> RecipeRepository
        {
            get
            {
                if (this.recipeRepository == null)
                {
                    this.recipeRepository = new GenericRepository<Recipe>(context);
                }
                return recipeRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
